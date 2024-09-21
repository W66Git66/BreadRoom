using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJump : MonoBehaviour
{
    public Transform groundCheck;
    private Rigidbody2D rb;

    public float checkRadius;
    bool isTouchingFront;
    public Transform frontCheck;
    public LayerMask WhatIsGround;
    bool wallSliding;
    public float WallSlidingSpeed;
    private bool isGrounded;

    bool wallJumping;
    public float xWallForce;
    public float yWallForce;
    public float wallJumpTime;

    private Vector2 originalScale;          // 记录原始大小

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale; // 存储角色的原始大小

    }

    // Update is called once per frame
    void Update()
    {
        float input = Input.GetAxisRaw("Horizontal");

        Debug.Log("isTouchingFront:" + isTouchingFront);
        Debug.Log("isGrounded:" + isGrounded);
        //Debug.Log(rb.velocity);
        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, WhatIsGround);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, WhatIsGround);
        if (isTouchingFront && !isGrounded && input != 0)
        {
            wallSliding = true;
        }
        else
        {
            wallSliding = false;
        }

        if(wallSliding) 
        {
            rb.velocity = new Vector2(rb.velocity.x,Mathf.Clamp(rb.velocity.y,-WallSlidingSpeed,float.MaxValue));
        }
        //if(isTouchingFront)
        //{
        //    transform.localScale = new Vector2(Mathf.Sign(input) * originalScale.x * 0.9f, originalScale.x * 1.25f);  // 根据移动方向调整朝向
        //}

        if(Input.GetKeyDown(KeyCode.W) && wallSliding)
        {
            wallJumping = true;
            Invoke("SetWallJumpingToFalse", wallJumpTime);
        }

        if(wallJumping)
        {
            rb.velocity = new Vector2(xWallForce * -input, yWallForce);
        }
    }

    void SetWallJumpingToFalse()
    {
        wallJumping = false;
    }
}
