using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_StickerPopUp : MonoBehaviour
{
    public Image unlockedIcone;
    public Sprite sprite;

    private void Start()
    {
        unlockedIcone.gameObject.SetActive(false);

        StartCoroutine(PopUpSticker(sprite));
    }


    public IEnumerator PopUpSticker(Sprite stickerIcone)
    {
        unlockedIcone.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        float lerpFactor = 0f;
        while (lerpFactor != 1f)
        {
            lerpFactor += 0.0005f;
            Debug.Log(lerpFactor);

            unlockedIcone.rectTransform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, lerpFactor);
        }
    }
}