using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController cC;
    private Transform cT;

    public float visualRotation;
    public float speed;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float jumpLimit;

    public Transform feetCheck;
    public Transform handsCheck;
    public float groundDistance = 0.4f;
    public float wallDistance = 1f;
    public LayerMask groundMask;
    public LayerMask outBoundsMask;
    public LayerMask wallMask;

    Vector3 velocity;
    bool isGrounded;
    bool isHugging;
    [HideInInspector] public bool isOutBounds = false;

    private void Start()
    {
        cT = GameObject.Find("Main Camera").GetComponent<Transform>();
        cC = GetComponent<CharacterController>();
    }

    void Update()
    {
        Movement();
        Gravity();
        Jump();
        CheckFeet();
        RotateVisual();
    }

    /// <summary>
    /// This allows the player to move with A/W/S/D & set its speed
    /// </summary>
    public void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        cC.Move(move * speed * Time.deltaTime);
    }

    /// <summary>
    /// This rotates the camera when the player touches the wall
    /// </summary>
    public void RotateVisual()
    {
        if(isHugging && cT.localEulerAngles.x != visualRotation)
        {
            cT.Rotate(visualRotation, cT.rotation.y, cT.rotation.z);
        }
    }

    /// <summary>
    /// This checks if the player touches the ground/wall/out of bounds
    /// </summary>
    public void CheckFeet()
    {
        isGrounded = Physics.CheckSphere(feetCheck.position, groundDistance
            , groundMask);

        isHugging = Physics.CheckSphere(handsCheck.position, wallDistance
            , wallMask);

        isOutBounds = Physics.CheckSphere(feetCheck.position, groundDistance
            , outBoundsMask);
    }

    /// <summary>
    /// This is how the player falls down
    /// </summary>
    public void Gravity()
    {
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        cC.Move(velocity * Time.deltaTime);
    }

    /// <summary>
    /// This is a simple jump system & rewards/removes the players jumps when touches a wall
    /// </summary>
    public void Jump()
    {
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (Input.GetButtonDown("Jump") && isHugging && jumpLimit >= 0)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jumpLimit--;
        }

        if(isHugging)
        {
            jumpLimit++;
        }
    }
}
