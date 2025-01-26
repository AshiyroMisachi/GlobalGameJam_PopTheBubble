using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_LampDeath : MonoBehaviour
{
    public int relatedSticker;


    public S_StickerBook stickerbook;
    public S_PlayerMovement2 player;
    private S_StickerPopUp stickerPopUp;


    public GameObject lampLight;
    public Material activeMaterial;
    public Collider triggerCollider;

    private bool canPlayerDie;

    [SerializeField]
    private float playerHeat;
    private void Start()
    {
        lampLight.SetActive(false);
        stickerPopUp = FindObjectOfType<S_StickerPopUp>();
        stickerbook = FindObjectOfType<S_StickerBook>();
        player = FindObjectOfType<S_PlayerMovement2>();

        triggerCollider.enabled = false;
    }

    private void OnTriggerStay(Collider other)
    {
        playerHeat += 1f;
        if (playerHeat >= 100 && canPlayerDie)
        {
            canPlayerDie = false;
            stickerbook.UnlockSticker(relatedSticker);
            StartCoroutine(stickerPopUp.PopUpSticker(stickerbook.stickerList[relatedSticker].unlockImage));
            player.RespawnAfterPop();
        }   

    }

    private void OnTriggerEnter(Collider other)
    {
        canPlayerDie = true;
    }

    private void OnTriggerExit(Collider other)
    {
        playerHeat = 0f;
        canPlayerDie = false;
    }


    public void ActivateLight()
    {
        triggerCollider.enabled = true;
        GetComponent<MeshRenderer>().material = activeMaterial;
        lampLight.SetActive(true);
    }
}
