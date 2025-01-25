using System.Collections;
using System.Collections.Generic;
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




    public Transform orientation;
    public float floatingSpeed;

    public bool playerInMenu = false;
    public Transform playerStart;
    public bool playerpoped;


    void Start()
    {
        //Get Reference from Component
        controller = GetComponent<CharacterController>();
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


        float y = Input.GetAxis("Jump");
        if (y != 0)
        {
            velocity.y = y * floatingSpeed;
        }
    }

    private void PlayerGravity()
    {
        //Apply the gravity to the player  multiplied by deltaTime to be frame independant 
        float y = Input.GetAxis("Jump");
        if (y == 0)
            velocity.y += gravity * Time.deltaTime;


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

    public void RespawnAfterPop()
    {
        //Animation Pop

        //Fin animation Pop
        playerpoped = true;
        transform.SetPositionAndRotation(playerStart.position, playerStart.rotation);
    }

    public IEnumerator RespawnPlayer()
    {
        //Début clignotement Bulle
        yield return new WaitForSeconds(1f);
        //Stop clignotement bulle
        playerpoped = false;
    }

}
