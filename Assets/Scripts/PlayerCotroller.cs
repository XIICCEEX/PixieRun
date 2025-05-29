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

    
    public AudioClip jumpSound;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>(); 
    }

    void Update()
    {
        bool groundedNow = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        if (groundedNow && !wasGrounded)
        {
            jumpCount = 0;
        }

        isGrounded = groundedNow;
        wasGrounded = groundedNow;

        if (!isSliding)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }

        if (Input.GetButtonDown("Jump") && jumpCount < maxJumpSteps)
        {
            Jump();
        }

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

            // 🔊 เล่นเสียงกระโดด
            if (jumpSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(jumpSound);
            }
        }
    }

    public void PerformSlide()
    {
        if (isGrounded && !isSliding)
        {
            StartCoroutine(SlideAction());
        }
    }

    private IEnumerator SlideAction()
    {
        isSliding = true;
        animator.SetBool("isSliding", true);

        rb.velocity = new Vector2(moveSpeed * 1.5f, rb.velocity.y);
        yield return new WaitForSeconds(0.5f);

        isSliding = false;
        animator.SetBool("isSliding", false);
    }
}
