using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_StickerPopUp : MonoBehaviour
{
    public Image stickerBookIcone;
    public Image unlockedIcone;
    public Sprite sprite;

    private Vector3 unlockedIconeInitialPosition;
    private float lerpFactor = 0f;
    private bool isAnimating;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        stickerBookIcone = GetComponent<Image>();
        unlockedIcone.gameObject.SetActive(false);
        unlockedIconeInitialPosition = unlockedIcone.rectTransform.localPosition;
    }

    private void Update()
    {
        if (isAnimating && lerpFactor != 1f)
        {
            lerpFactor += 0.0025f;
            unlockedIcone.rectTransform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, lerpFactor);
            unlockedIcone.rectTransform.localPosition = Vector3.Lerp(unlockedIconeInitialPosition, stickerBookIcone.rectTransform.localPosition, lerpFactor);

            if (lerpFactor >= 1f)
            {
                lerpFactor = 0f;
                isAnimating = false;
                animator.SetTrigger("PopUpMove");
                unlockedIcone.rectTransform.localPosition = unlockedIconeInitialPosition;
                unlockedIcone.rectTransform.localScale = Vector3.one;
                unlockedIcone.gameObject.SetActive(false);
            }
        }
    }

    public IEnumerator PopUpSticker(Sprite stickerIcone)
    {
        unlockedIcone.gameObject.SetActive(true);
        unlockedIcone.sprite = stickerIcone;
        yield return new WaitForSeconds(1);
        isAnimating = true;
    }
}