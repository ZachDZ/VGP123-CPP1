using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    // Component References
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;

    // Movement Variables
    public float speed = 5.0f;
    public float jumpForce = 300.0f;
    public bool isFlipped = false;

    // GroundCheck Stuff
    public bool isGrounded;
    public Transform groundCheck;
    public LayerMask isGroundLayer;
    public float groundCheckRadius = 0.02f;

    // Attack Stuff
    public bool attack = false;

    // Start is called before the first frame update
    void Start()
    {
        // Getting Our Component References
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        // Checking Variables For Dirty Data
        if (rb == null)
            Debug.Log("No Rigidbody Reference");
        if (sr == null)
            Debug.Log("No Sprite Renderer Reference");
        if (anim == null)
            Debug.Log("No Animator Reference");

        if (groundCheckRadius <= 0)
        {
            groundCheckRadius = 0.02f;
            Debug.Log("GroundCheck set to default value");
        }

        if (speed <= 0)
        {
            speed = 5.0f;
            Debug.Log("Speed set to default value");
        }

        if (jumpForce <= 0)
        {
            jumpForce = 3.00f;
            Debug.Log("jumpForce set to default value");
        }

        if (groundCheck == null)
        {
            GameObject obj = new GameObject();
            obj.transform.SetParent(gameObject.transform);
            obj.transform.localPosition = Vector3.zero;
            obj.name = "GroundCheck";
            groundCheck = obj.transform;
        }

    }

    // Update is called once per frame
    void Update()
    {
        float hInput = Input.GetAxisRaw("Horizontal");

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);

        // Jump Event
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector2.up * jumpForce);
        }

        // Attack Events
        if (Input.GetButton("Fire1"))
        {
            attack = true;
            Debug.Log("attack is set to true");
        }
        else
        {
            attack = false;
            Debug.Log("attack is set to false");
        }

        // Flip Events
        if (hInput == -1)
        {
            isFlipped = true;
        }
        else if (hInput == 1)
        {
            isFlipped = false;
        }

        if (isFlipped == true)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }

        Vector2 moveDirection = new Vector2(hInput * speed, rb.velocity.y);
        rb.velocity = moveDirection;

        anim.SetFloat("hInput", Mathf.Abs(hInput));
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("attack", attack);

    }
}
