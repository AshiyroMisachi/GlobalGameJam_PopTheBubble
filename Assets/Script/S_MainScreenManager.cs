using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class S_MainScreenManager : MonoBehaviour
{
    private S_DataManager dataManager;
    private AudioSource m_AudioSource;
    public GameObject stickerbook;

    public GameObject[] stickerList;

    public Animator blackImage;

    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        dataManager = FindObjectOfType<S_DataManager>();
        Cursor.lockState = CursorLockMode.None;
        Debug.Log("Test");
    }

    public void StartLevel()
    {
        StartCoroutine(LoadLevel());
    }


    public IEnumerator LoadLevel()
    {
        blackImage.SetTrigger("FadeIn");
        yield return new WaitUntil(() => blackImage.GetComponent<Image>().color.a == 1);
        SceneManager.LoadScene("Level_1");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenStickerbook()
    {
        stickerbook.SetActive(!stickerbook.activeSelf);

        if (dataManager.gameFinished)
        {
            for (int i = 0; i < stickerList.Length; i++)
            {
                stickerList[i].SetActive(true);
            } 
        }
    }
}