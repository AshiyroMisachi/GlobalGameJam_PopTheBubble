using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_MainScreenManager : MonoBehaviour
{
    private AudioSource m_AudioSource;


    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();   
    }

    public void StartLevel()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}