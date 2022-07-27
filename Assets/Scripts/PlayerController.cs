using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region
    private Rigidbody rb;
    public float speed;
    float horizontal;
    float vertical;
    [SerializeField] private float groundVelocity;
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float jumpFallVelocity;
    [SerializeField] private float jump;
    public bool isforwardClicked = false;
    public bool isReverseClicked = false;
    public bool isFacingRight = true;
    private Animator animator;

    #endregion
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        isforwardClicked = false;
        isReverseClicked = false;
        isFacingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxisRaw("Jump");
        //Debug.Log("UPDATE"+horizontal);
           
        
//#if UNITY_EDITOR
//        if (Input.GetKey(KeyCode.D))
//        {
//            rb.velocity = new Vector3(0, 0, 1 * speed * Time.deltaTime);
//        }
//#endif
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(horizontal * speed * Time.fixedDeltaTime, rb.velocity.y, 0);
        Debug.Log(horizontal);
        animator.SetFloat("Horizontal", Mathf.Abs(horizontal));
        MoveWithUIButtons();

        if (horizontal > 0 && !isFacingRight)
        {
            Flip();
        }
        else if(horizontal<0 && isFacingRight)
        {
            Flip();
        }
        else if (isforwardClicked && !isFacingRight)
        {
            Flip();
        }
        else if (isReverseClicked && isFacingRight)
        {
            Flip();
        }

        // MoveCharacter(horizontal, vertical);
        //PlayerJumpAnimation(vertical);
        //Move();
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.z *= -1;
        transform.localScale = scale;
    }

    private void PlayerJumpAnimation(float vertical)
    {
        if (Input.GetKey(KeyCode.Space))// && isGrounded)
        {
            animator.SetBool("Jump", true);
            rb.velocity = new Vector3(0, jumpVelocity, 0);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetBool("Jump", false);
            rb.velocity = new Vector3(0, jumpFallVelocity, 0);
        }
    }

    #region UI Button Movement
    public void OnForwardButton()
    {
        isforwardClicked = true;
        horizontal = 1f;
       

    }
    public void OnForwardButtonUp()
    {
        isforwardClicked = false;
        horizontal = 0;
    }

    public void OnReverseButton()
    {
        isReverseClicked = true;
        horizontal = -1;
    }
    public void OnReverseButtonUp()
    {
        isReverseClicked = false;
        horizontal = 0;
    }

    public void MoveWithUIButtons()
    {
        if (isforwardClicked)
        {
            rb.velocity = new Vector3(speed*Time.fixedDeltaTime, rb.velocity.y, 0);
            animator.SetFloat("Horizontal", Mathf.Abs(speed));
        }
        else if (isReverseClicked)
        {
            rb.velocity -= new Vector3(speed * Time.fixedDeltaTime, rb.velocity.y, 0);
            animator.SetFloat("Horizontal", Mathf.Abs(speed));
        }

    }

    #endregion

    public void MoveCharacter(float horizontal, float verticle)
    {
        // Move character horizontally
        if (horizontal > 0)
        {
            Vector3 position = transform.position;
            position.z += horizontal * speed * Time.fixedDeltaTime;
            transform.position = position;
            animator.SetFloat("Horizontal", horizontal);
        }
        if (horizontal == 0)
        {
            animator.SetFloat("Horizontal", horizontal);
        }

        // Move character vertically
        if (verticle > 0)
        {
            animator.SetBool("Jump", true);
            rb.AddForce(new Vector3(0f, jump,0f), ForceMode.Impulse);
        }
    }
}
