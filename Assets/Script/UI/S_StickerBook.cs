using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.Build;

public class S_StickerBook : MonoBehaviour
{
    private S_LevelManager levelManager;

    public GameObject stickerBook;
    public KeyCode openKey = KeyCode.I;


    public S_PlayerMovement2 player;
    public CinemachineFreeLook playerCamera;


    public S_Sticker[] stickerList;
    [SerializeField]
    private bool[] stickersUnlock;

    public Animator blackImage;

    private void Start()
    {
        stickersUnlock = new bool[stickerList.Length];   
        levelManager = FindObjectOfType<S_LevelManager>();
        stickerBook.SetActive(false);
    }


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


    public bool CheckStickerState(int sticker)
    {
        return stickersUnlock[sticker];
    }

    public void UnlockSticker(int stickerNumber)
    {
        stickersUnlock[stickerNumber] = true;
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


    public void CheckWinCondition()
    {
        for (int i = 0;i < stickerList.Length; i++)
        {
            if (!stickersUnlock[i])
            {
                return;
            }
        }
        StartCoroutine(levelManager.LoadEndScreen());
    }
}