using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2D;

    [Header("Movement Settings")]

    private float horizontalMovement = 0f;

    [SerializeField] private float moveSpeed;

    [Range (0, 0.3f)][SerializeField] private float speedSmoothening;

    private Vector3 speed = Vector3.zero;

    private bool lookingRight = true;

    [Header("Jump Settings")]

    [SerializeField] private float jumpForce;

    [SerializeField] private LayerMask groundCheck;

    [SerializeField] private Transform floorControler;

    [SerializeField] private Vector3 boxDimentions;

    [SerializeField] private bool onFloor;

    private bool jump = false;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal") * moveSpeed;

        if(Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        onFloor = Physics2D.OverlapBox(floorControler.position, boxDimentions, 0f, groundCheck);

        Move(horizontalMovement * Time.fixedDeltaTime, jump);

        jump = false;
    }

    private void Move(float  move, bool jump)
    {
        Vector3 speedObjective = new Vector2(move, rb2D.linearVelocity.y);
        rb2D.linearVelocity = Vector3.SmoothDamp(rb2D.linearVelocity, speedObjective, ref speed, speedSmoothening);

        if (move>0 && !lookingRight)
        {
            Flip();
        }
        else if (move < 0 && lookingRight)
        {
            Flip();
        }

        if(onFloor && jump)
        {
            onFloor = false;
            rb2D.AddForce(new Vector2(0f, jumpForce));
        }

    }

    private void Flip()
    {
        lookingRight = !lookingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(floorControler.position, boxDimentions);
    }
}
