using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;
using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net.Http;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;

public class ChatManager : ChatManagerBehavior
{
	public Transform contentTransform;
	public GameObject messageLabel;
	public GameObject eventSystem;

	public InputField messageInput;

	private List<Text> messageLabels = new List<Text>();
	public int maxMessages = 100;

    public string username;
    IDbConnection dbcon;

    private string prevAction;
    private string currAction;
    private string doctor_speech;
    private string patient_speech;
    private string prevPosition;
    private string currPosition;

    private string transcriptPath;

    public string action="none";
    public string position= "standing";

    private void Awake()
	{
		Instantiate(eventSystem);
        try
        {
            string connection = "URI=file:" + Application.persistentDataPath + "/" + "data_DB";
            // Open connection
            dbcon = new SqliteConnection(connection);
            dbcon.Open();
            // Create table
            IDbCommand dbcmd;
            dbcmd = dbcon.CreateCommand();
            string q_createTable = "CREATE TABLE IF NOT EXISTS transcript (doctor_speech TEXT, patient_speech TEXT, patient_action_prev TEXT, patient_action_after TEXT, patient_position_prev TEXT, patient_position_after TEXT)";
            dbcmd.CommandText = q_createTable;
            dbcmd.ExecuteReader();
        } catch (Exception e)
        {
            print(e);
        }
        

        // Set initial values
        prevAction = "none";
        currAction = "none";
        doctor_speech = "";
        patient_speech = "";
        prevPosition = "standing";
        currPosition = "standing";

        // Create transcript file if not alread exists
        transcriptPath = Application.persistentDataPath + "/transcript.txt";
        // Clear transcript
        File.WriteAllText(transcriptPath, "");

        /**
        if (CrossSceneInformation.role == "patient")
            username = "Patient";
        else
            username = "Doctor";
    **/
    }
    
    protected override void NetworkStart()
    {
        base.NetworkStart();
    }

    public void UpdateActionPosition(string action, string position)
    {
        networkObject.SendRpc(RPC_CHANGE_ACTION_POSITION, Receivers.All, action, position);
    }

    public override void ChangeActionPosition(RpcArgs args)
    {
        string arg_action = args.GetNext<string>();
        string arg_position = args.GetNext<string>();
        action = arg_action;
        position = arg_position;
        
        currAction = action;
        currPosition = position;
    }

    public override void SendMessage(RpcArgs args)
	{
		string username = args.GetNext<string>();
		string message = args.GetNext<string>();
        string action = args.GetNext<string>();
        string position = args.GetNext<string>();

        Text label = null;
		if (messageLabels.Count == maxMessages)
		{
			label = messageLabels[0];
			messageLabels.RemoveAt(0);
			label.transform.SetAsLastSibling();
		}
		else
			label = (Instantiate(messageLabel, contentTransform) as GameObject).GetComponent<Text>();

		messageLabels.Add(label);
		label.text = username + ": " + message;

        // record transcript on all local clients
        var textFile = File.ReadAllText(transcriptPath);
        File.WriteAllText(transcriptPath, textFile + "\n" + username + ": " + message);

        if (networkObject.IsServer)
        {            
            if (username == "Patient")
            {
                if (patient_speech != "") patient_speech += ". ";
                patient_speech += message.Trim();
            }
            else if (username == "Doctor")
            {
                // Doctor (IMG)
                if (patient_speech != "")
                {
                    // Call to insert conversation in DB
                    StoreTranscript();

                    // reset        
                    doctor_speech = message.Trim();
                    patient_speech = "";
                    prevAction = currAction;
                    prevPosition = currPosition;                    
                }
                else
                {
                    if (doctor_speech != "") doctor_speech += ". ";
                    doctor_speech += message.Trim();
                }
            }        
        }
        
    }

    public void SendMessage()
    {
        string message = messageInput.text.Trim();
        if (string.IsNullOrEmpty(message))
            return;

        networkObject.SendRpc(RPC_SEND_MESSAGE, Receivers.All, username, message, action, position);

        messageInput.text = "";
        messageInput.Select();
    }

    public void RecordVoice(string msg)
    {
        string message = msg.Trim();
        if (string.IsNullOrEmpty(message))
            return;

        networkObject.SendRpc(RPC_SEND_MESSAGE, Receivers.All, username, message, action, position);
    }

    public void StoreTranscript()
    {
        String finalDoctorSpeech = doctor_speech.Replace("'", "''");
        String finalPatientSpeech = patient_speech.Replace("'", "''");

        // Insert values in table
        IDbCommand cmnd = dbcon.CreateCommand();
        cmnd.CommandText = "INSERT INTO transcript (doctor_speech, patient_speech, patient_action_prev, patient_action_after, patient_position_prev, patient_position_after) VALUES ('" + finalDoctorSpeech + "', '" + finalPatientSpeech
            + "', '" + prevAction + "', '" + currAction + "', '" + prevPosition + "', '" + currPosition + "')";
        cmnd.ExecuteNonQuery();
    }

    public void DownloadTranscript()
    {
        String result = UploadTranscriptAsync();
        Application.OpenURL(result);
    }

    public String UploadTranscriptAsync()
    {
        using (var httpClient = new HttpClient())
        {
            using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://file.io/?expires=1w"))
            {
                var multipartContent = new MultipartFormDataContent();
                multipartContent.Add(new ByteArrayContent(File.ReadAllBytes(transcriptPath)), "file", Path.GetFileName(transcriptPath));
                request.Content = multipartContent;

                var response = httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                string url = response.Split(new string[] { "," }, StringSplitOptions.None)[5].Split(new string[] { "\"" }, StringSplitOptions.None)[3];
                return url;
            }
        }
    }

    public override void RestartTranscript(RpcArgs args)
    {
        if (networkObject.IsServer)
        {
            File.WriteAllText(transcriptPath, "");
        }
    }

    public void Restart()
    {
        // Clear transcript
        File.WriteAllText(transcriptPath, "");
        networkObject.SendRpc(RPC_RESTART_TRANSCRIPT, Receivers.All);
    }    
}