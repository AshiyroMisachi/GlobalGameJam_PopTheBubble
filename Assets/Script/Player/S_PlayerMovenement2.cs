using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

//This Script need a CharacterController to work, CharacterController work like a Collider
[RequireComponent(typeof(CharacterController))]
public class S_PlayerMovement2 : MonoBehaviour
{
    [Header("Movement")]
    //speed value of the player
    [SerializeField]
    private float speed = 12f;

    //Reference of the CharacterController
    private CharacterController controller;

    [Header("Gravity")]
    //CharacterController and Rigidbody are not compatible so we need  to create a self gravity for the player
    //Stock the velocity of the player
    private Vector3 velocity;
    //The Gravity who gonna be apply to the player 
    [SerializeField]
    private float gravity = -9.81f;


    [Header("GroundCheck")]
    //All the variable needed for checking if the player is touching the ground
    //The transform of a empty gameobject
    [SerializeField]
    private Transform groundCheck;
    //Radius of the checking sphere
    [SerializeField]
    private float groundDistance = 0.04f;
    //Mask for the ground
    [SerializeField]
    private LayerMask groundMask;
    //Stock if the player touch the ground or not
    private bool isGrounded;



    public MeshRenderer playerObj;

    public Transform orientation;
    public float floatingSpeed;

    public bool playerInMenu = false;
    public Transform playerStart;
    public bool playerpoped;


    public float scaleSpeed;
    private float actualScaleSpeed;
    public float minScalePop;
    public float maxScalePop;

    private Vector3 currentScale = Vector3.one;
    private float reScaleTime;

    private S_StickerBook stickerBook;
    private S_StickerPopUp stickerPopUp;
    private S_FallingCubeManager fallingCubeManager;


    private AudioSource popSound;
    public GameObject popVFX;

    void Start()
    {
        //Get Reference from Component
        controller = GetComponent<CharacterController>();

        stickerBook = FindObjectOfType<S_StickerBook>();
        stickerPopUp = FindObjectOfType<S_StickerPopUp>();
        fallingCubeManager = FindObjectOfType<S_FallingCubeManager>();
        popSound = GetComponent<AudioSource>();


    }

    void Update()
    {
        if (playerInMenu || playerpoped)
        {
            return;
        }

        //Check if the player touch the ground or not
        IsGrounded();
        //Chek if the player need to move each frame
        Movement();
        //Apply the gravity to the player
        PlayerGravity();

        ScaleDeath();
    }

    private void Movement()
    {

        //Stock Axis value
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");


        //Make a vector 3 from Axis value before moving
        Vector3 move = orientation.transform.right * x + orientation.transform.forward * z;

        //Move the Player    Multiplied by the speed    And deltaTime to work with all frameRate
        controller.Move(move * speed * Time.deltaTime);

    }

    private void PlayerGravity()
    {
        float y = Input.GetAxis("Jump");

        if (y != 0)
        {
            if (y > 0)
            {
                actualScaleSpeed = scaleSpeed;
            }
            else
            {
                actualScaleSpeed = scaleSpeed/3;
            }

            velocity.y = y * floatingSpeed;

            transform.localScale = transform.localScale + Vector3.one * y * actualScaleSpeed;

            currentScale = transform.localScale;
            reScaleTime = 0f;
        }
        else
        {
            //Apply the gravity to the player  multiplied by deltaTime to be frame independant 
            velocity.y = gravity;

            reScaleTime += 0.008f;
            transform.localScale = Vector3.Lerp(currentScale, Vector3.one, reScaleTime);
        }


        //Move the player with the gravity velocity  and multiplied again with deltaTime because
        controller.Move(velocity * Time.deltaTime);
    }

    private void IsGrounded()
    {
        //Spawn a sphere at the bottom of the player, if this sphere touch a object at the ground layout, say true
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //If the player touch the ground, reset the gravity velocity
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }


    //Self Death Condition
    private void ScaleDeath()
    {
        if (transform.localScale.x < minScalePop)
        {
            if (!stickerBook.CheckStickerState(1))
            {
                stickerBook.UnlockSticker(1);
                StartCoroutine(stickerPopUp.PopUpSticker(stickerBook.stickerList[1].unlockImage));
            }
            RespawnAfterPop();
        }
        else if (transform.localScale.x > maxScalePop)
        {
            if (!stickerBook.CheckStickerState(0))
            {
                stickerBook.UnlockSticker(0);
                StartCoroutine(stickerPopUp.PopUpSticker(stickerBook.stickerList[0].unlockImage));
            }
            RespawnAfterPop();
        }
    }


    //Respawn
    public void RespawnAfterPop()
    {
        playerpoped = true;
        popSound.Play();
        StartCoroutine(RespawnPlayer());
        if (!fallingCubeManager.playerGotKilled)
        {
            fallingCubeManager.ResetCubesPositions();
        }
    }

    public IEnumerator RespawnPlayer()
    {
        playerObj.enabled = false;
        GameObject activePopVFX = Instantiate(popVFX, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Destroy(activePopVFX);
        playerObj.enabled = true;
        transform.SetPositionAndRotation(playerStart.position, playerStart.rotation);
        transform.localScale = Vector3.one;
        currentScale = Vector3.one;
        stickerBook.CheckWinCondition();
        yield return new WaitForSeconds(0.2f);
        playerpoped = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<S_Cube>())
        {
            if (collision.gameObject.GetComponent<Rigidbody>().velocity.y < 0 && collision.transform.position.y > transform.position.y)
            {
                StartCoroutine(stickerPopUp.PopUpSticker(stickerBook.stickerList[4].unlockImage));
                stickerBook.UnlockSticker(4);
                fallingCubeManager.playerGotKilled = true;
                RespawnAfterPop();
            }
        }
    }
}