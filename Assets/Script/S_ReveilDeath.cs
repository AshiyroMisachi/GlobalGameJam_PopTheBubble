using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ReveilDeath : MonoBehaviour
{
    public int relatedSticker;


    public S_StickerBook stickerbook;
    public S_PlayerMovement2 player;
    private S_StickerPopUp stickerPopUp;


    public AudioSource audioReveil;
    public Collider triggerCollider;

    private void Start()
    {
        stickerPopUp = FindObjectOfType<S_StickerPopUp>();
        stickerbook = FindObjectOfType<S_StickerBook>();
        player = FindObjectOfType<S_PlayerMovement2>();

        triggerCollider.enabled = false;
    }


    /*
    private void OnTriggerEnter(Collider other)
    {
        if (!stickerbook.CheckStickerState(relatedSticker))
        {
            stickerbook.UnlockSticker(relatedSticker);
            StartCoroutine(stickerPopUp.PopUpSticker(stickerbook.stickerList[relatedSticker].unlockImage));
        }
        player.RespawnAfterPop();
    }
    */

    public void ActivateClock()
    {
        triggerCollider.enabled = true;
        audioReveil.Play();
        StartCoroutine(StopClock());
        if (!stickerbook.CheckStickerState(relatedSticker))
        {
            stickerbook.UnlockSticker(relatedSticker);
            StartCoroutine(stickerPopUp.PopUpSticker(stickerbook.stickerList[relatedSticker].unlockImage));
        }
        player.RespawnAfterPop();
    }

    public IEnumerator StopClock()
    {
        yield return new WaitForSeconds(audioReveil.time);
        triggerCollider.enabled = false;
    }
}
