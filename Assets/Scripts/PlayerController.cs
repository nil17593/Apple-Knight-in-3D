using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    #region SerializedFields
    [SerializeField] private float speed;
    [SerializeField] private float groundVelocity;
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float jumpFallVelocity;
    [SerializeField] private float jump;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float jumpPower;
    #endregion

    #region bools
    public bool isforwardClicked = false;
    public bool isReverseClicked = false;
    public bool isFacingRight = true;
    public bool isGrounded = false;
    public bool jumping = false;
    public bool secretArea = false;
    private bool gameOver;
    public bool ableToMakeADoubleJump = false;
    #endregion

    #region private fields
    private Rigidbody rb;
    private Animator animator;
    Collider[] groundColliders;
    float horizontal;
    float vertical;
    private GameManager gameManager;
    #endregion

    [Header("Health UI")]
    [SerializeField] private Image[] hearts;
    [SerializeField] private int livesRemain = 3;
    [SerializeField] private int score;

    [Header("Achievement requirments")]
    [HideInInspector] public int enemyKillCount;
    public int highScore;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        isforwardClicked = false;
        isReverseClicked = false;
        isFacingRight = true;
        jumping = false;
        gameManager = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Jump");

        if (isGrounded)
        {
            animator.SetFloat("Horizontal", Mathf.Abs(horizontal));
        }
        if (isGrounded && isforwardClicked)
        {
            animator.SetFloat("Horizontal", Mathf.Abs(speed));
        }
        if (isGrounded && isReverseClicked)
        {
            animator.SetFloat("Horizontal", Mathf.Abs(speed));
        }

        if (horizontal > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (horizontal < 0 && isFacingRight)
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
        AttackAnimation();
    }

    private void FixedUpdate()
    {
        if (!isGrounded)
        {
            animator.SetBool("Jumping", true);
            rb.AddForce(new Vector3(0, -jumpFallVelocity, 0));
        }

        //if (ableToMakeADoubleJump && vertical > 0)
        //{
        //    DoubleJump();
        //}

        if(isGrounded && vertical > 0)
        {
            //horizontal = 0;
            isGrounded = false;
            ableToMakeADoubleJump = true;
            animator.SetBool("Jumping", true);
            rb.AddForce(new Vector3(0, jumpPower, 0));
        }
        else
        {
            animator.SetBool("Jumping", false);
        }

        groundColliders = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundLayer);
        if (groundColliders.Length > 0)
        {
            isGrounded = true;
            ableToMakeADoubleJump = false;
        }
        else
        {
            isGrounded = false;
        }

        Movement();
        MoveWithUIButtons();
    }

    private void DoubleJump()
    {
        //Double Jump
        animator.SetBool("Jumping", true);

        rb.AddForce(new Vector3(0, jumpPower, 0));
        ableToMakeADoubleJump = false;
    }

    public void KillPlayer()
    {
        if (gameOver)
        {
            GameManager.instance.EnableRestartPanel();
            return;
        }
        else
        {
            livesRemain--;
            UpdateLifeUI();
        }
       
    }
    private void UpdateLifeUI()
    {
        hearts[livesRemain].gameObject.SetActive(false);

        if (livesRemain == 0)
        {
            hearts[livesRemain].gameObject.SetActive(false);
            gameOver = true;
        }
    }
    void AttackAnimation()
    {
        if (Input.GetKey(KeyCode.S))
        {
            animator.SetBool("Attack", true);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            animator.SetBool("Attack", false);
        }
    }

    void Movement()
    {
        rb.velocity = new Vector3(horizontal * speed * Time.fixedDeltaTime, rb.velocity.y, 0);      
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
            animator.SetBool("Jumping", true);
            rb.velocity = new Vector3(0, jumpVelocity, 0);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetBool("Jumping", false);
            rb.velocity = new Vector3(0, jumpFallVelocity, 0);
        }
    }

    #region UI Button Movement

    public void OnAttackButtonClick()
    {
        animator.SetBool("Attack", true);
    }
    public void OnAttackButtonUP()
    {
        animator.SetBool("Attack", false);
    }


    public void OnJumpButtonClick()
    {
        if (isGrounded)
        {
            isGrounded = false;
            animator.SetBool("Jumping", true);
            rb.AddForce(new Vector3(0, jumpPower*4, 0));
        }
    }

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
            rb.velocity = new Vector3(speed * Time.fixedDeltaTime, rb.velocity.y, 0);
            if (isGrounded)
            {
                animator.SetFloat("Horizontal", Mathf.Abs(speed));
            }
        }
        else if (isReverseClicked)
        {
            rb.velocity -= new Vector3(speed * Time.fixedDeltaTime, rb.velocity.y, 0);
            if (isGrounded)
            {
                animator.SetFloat("Horizontal", Mathf.Abs(speed));
            }
        }

    }

    #endregion


    #region collision

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            //score += 1;
            ScoreController.instance.IncreaseScore(1);
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("SecretArea"))
        {
            secretArea = true;
            if (secretArea)
            {
                other.gameObject.GetComponent<MeshRenderer>().enabled = false;
            }
           
        }
        if (other.gameObject.CompareTag("Level1Complete"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            KillPlayer();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("SecretArea"))
        {
            secretArea = false;
            if (!secretArea)
            {
                other.gameObject.GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }

    #endregion
}
