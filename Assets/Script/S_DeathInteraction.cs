using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class S_DeathInteraction : MonoBehaviour
{
    public int relatedSticker;


    public S_StickerBook stickerbook;
    public S_PlayerMovement2 player;
    private S_StickerPopUp stickerPopUp;

    private void Start()
    {
        stickerPopUp = FindObjectOfType<S_StickerPopUp>();
        stickerbook = FindObjectOfType<S_StickerBook>();
        player = FindObjectOfType<S_PlayerMovement2>();
    }

    private void OnTriggerEnter(Collider other)
    {
        stickerbook.UnlockSticker(relatedSticker);
        player.RespawnAfterPop();
        StartCoroutine(stickerPopUp.PopUpSticker(stickerbook.stickerList[relatedSticker].unlockImage));
    }
}