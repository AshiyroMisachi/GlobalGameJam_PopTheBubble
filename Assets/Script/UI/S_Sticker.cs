using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class S_Sticker : MonoBehaviour
{
    public Image sticker;
    public Sprite lockImage;
    public Sprite unlockImage;

    public string stickerName;
    public TextMeshProUGUI gameText;


    private void Start()
    { /*
        //Setup
        sticker.sprite = lockImage;
        gameText.text = "???";*/
    }

    public void Unlock()
    {
        sticker.sprite = unlockImage;
        gameText.text = stickerName;

        //POP UP Event ?
    }
}
