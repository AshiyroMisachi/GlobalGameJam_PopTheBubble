using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_StickerBook : MonoBehaviour
{


    public GameObject stickerBook;
    public KeyCode openKey = KeyCode.I;


    public S_PlayerMovement2 player;
    public CinemachineFreeLook playerCamera;


    public S_Sticker[] stickerList;
    public bool[] stickersUnlock;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(openKey))
        {
            stickerBook.SetActive(!stickerBook.activeSelf);
            player.playerInMenu = !player.playerInMenu;
            playerCamera.enabled = !playerCamera.enabled;
            SetupStickerBook();
        }
    }

    private void SetupStickerBook()
    {
        for (int i = 0; i < stickerList.Length; i++)
        {
            if (stickersUnlock[i])
            {
                stickerList[i].Unlock();
            }
        }
    }
}