using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class S_DeathInteraction : MonoBehaviour
{
    public S_StickerBook stickerbook;
    public S_PlayerMovement2 player;
    public int relatedSticker;

    private void Start()
    {
        stickerbook = FindObjectOfType<S_StickerBook>();
        player = FindObjectOfType<S_PlayerMovement2>();
    }

    private void OnTriggerEnter(Collider other)
    {
        stickerbook.UnlockSticker(0);
        player.RespawnAfterPop();
    }
}
