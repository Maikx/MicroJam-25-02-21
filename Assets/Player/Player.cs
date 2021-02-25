using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Loader loader;
    private CharacterController cC;
    private Vector3 velocity;
    private Transform fT;
    private Transform hT;
    private Transform cT;

    [Header("Settings")]
    public float mouseSensitivity = 100f;
    public float visualRotation = 20f;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    [Header("CheckDistance")]
    public float groundDistance = 0.4f;
    public float wallDistance = 1f;

    [Header("LayerMasks")]
    public LayerMask groundMask;
    public LayerMask outBoundsMask;
    public LayerMask wallMask;

    [HideInInspector] public bool isGrounded;
    [HideInInspector] public bool isHugging;
    [HideInInspector] public bool isOutBounds;

    private void Start()
    {
        cT = GameObject.Find("Main Camera").GetComponent<Transform>();
        cC = GetComponent<CharacterController>();
        fT = GameObject.Find("GroundCheck").GetComponent<Transform>();
        hT = GameObject.Find("WallCheck").GetComponent<Transform>();
        loader = GameObject.Find("SceneManager").GetComponent<Loader>();
    }

    void Update()
    {
        Movement();
        Gravity();
        Jump();
        CheckFeet();
        RotateVisual();
        LoseCondition();
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
        if (isHugging && cT.localEulerAngles.x != visualRotation)
        {
            cT.localEulerAngles = new Vector3(visualRotation, cT.localEulerAngles.y, cT.localEulerAngles.z);
            //perchè nella build non funziona?? ;(
        }
    }

    /// <summary>
    /// This checks if the player touches the ground/wall/out of bounds
    /// </summary>
    public void CheckFeet()
    {
        isGrounded = Physics.CheckSphere(fT.position, groundDistance
            , groundMask);

        isHugging = Physics.CheckSphere(hT.position, wallDistance
            , wallMask);

        isOutBounds = Physics.CheckSphere(fT.position, groundDistance
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
    /// This is a simple jump system
    /// </summary>
    public void Jump()
    {
        if(Input.GetButtonDown("Jump") && isGrounded || Input.GetButtonDown("Jump") && isHugging)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    public void LoseCondition()
    {
        if (isOutBounds)
            loader.ReloadScene();
    }
}
