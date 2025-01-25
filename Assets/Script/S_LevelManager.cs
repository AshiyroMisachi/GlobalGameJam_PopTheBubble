using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class S_LevelManager : MonoBehaviour
{
    public Animator blackImage;

    private void Start()
    {
        blackImage.gameObject.SetActive(true);
        blackImage.SetTrigger("FadeOut");
    }


    public IEnumerator LoadEndScreen()
    {
        blackImage.SetTrigger("FadeIn");
        yield return new WaitUntil(() => blackImage.GetComponent<Image>().color.a == 1);
        SceneManager.LoadScene("EndScreen");
    }
}