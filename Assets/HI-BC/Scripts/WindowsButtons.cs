using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WindowsButtons : MonoBehaviour
{
    public GameObject minimize;
    public GameObject exit;
    public GameObject home;

    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);
    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    // Start is called before the first frame update
    void Start()
    {
        minimize.GetComponent<Button>().onClick.AddListener(MinimizeWindow);
        exit.GetComponent<Button>().onClick.AddListener(ExitWindow);
        home.GetComponent<Button>().onClick.AddListener(GoToHome);
    }

    private void MinimizeWindow()
    {
        ShowWindow(GetActiveWindow(), 2);
    }

    private void ExitWindow()
    {
        Application.Quit();
    }

    private void GoToHome()
    {
        SceneManager.LoadScene(0);
    }
}
