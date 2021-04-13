using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine;

public class NextScene : MonoBehaviour
{
    public void NextScreen(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
