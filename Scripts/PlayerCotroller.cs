using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCotroller : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed = 5f;
    public float jumpForce = 8f;
    public float slideSpeed = 10f;
    private bool isSliding = false;
    private bool isGrounded = false;
    private bool wasGrounded = false;
    public int healthbar = 100;
    private int jumpCount = 0;
    public int maxJumpSteps = 2;

    private Animator animator;
    public Transform groundCheck;
    public LayerMask groundLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        bool groundedNow = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        if (groundedNow && !wasGrounded)
        {
            // แตะพื้นรอบใหม่ รีเซ็ต jumpCount
            jumpCount = 0;
        }

        isGrounded = groundedNow;
        wasGrounded = groundedNow;

        // ตัวละครเคลื่อนที่ไปเรื่อย ๆ ทางขวา (หยุดเมื่อสไลด์)
        if (!isSliding)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }

        // กระโดด
        if (Input.GetButtonDown("Jump") && jumpCount < maxJumpSteps)
        {
            Jump();
        }

        // สไลด์
        if (Input.GetKeyDown(KeyCode.A) && isGrounded && !isSliding)
        {
            StartCoroutine(SlideAction());
        }

        animator.SetBool("isJumping", !isGrounded);
    }

    public void Jump()
    {
        if (!isSliding && jumpCount < maxJumpSteps)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;
            animator.SetBool("isJumping", true);
        }
    }

    public void PerformSlide()
    {
        if (isGrounded && !isSliding)
        {
            StartCoroutine(SlideAction());
        }
    }
    private System.Collections.IEnumerator SlideAction()
    {
        isSliding = true;
        animator.SetBool("isSliding", true);

        rb.velocity = new Vector2(moveSpeed * 1.5f, rb.velocity.y);
        yield return new WaitForSeconds(0.5f);

        isSliding = false;
        animator.SetBool("isSliding", false);
    }

}
