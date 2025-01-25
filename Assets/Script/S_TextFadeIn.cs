using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_TextFadeIn : MonoBehaviour
{
    public bool startFadeIn;
    private Animator animator;
    public Animator nextText;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (startFadeIn)
            animator.SetTrigger("FadeIn");
    }

    public void NextText()
    {
        if (nextText == null)
        {
            StartCoroutine(BackToMainScreen());
            return;
        }

        nextText.SetTrigger("FadeIn");
    }

    public IEnumerator BackToMainScreen()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(0);
    }
}