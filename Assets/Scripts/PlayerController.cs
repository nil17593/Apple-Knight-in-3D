using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region
    private Rigidbody rb;
    public float speed;
    [SerializeField] private float groundVelocity;
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float jumpFallVelocity;
    [SerializeField] private float jump;
    #endregion
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Jump");
        PlayerMovementAnimation(horizontal);
        PlayerJumpAnimation(vertical);
        MoveCharacter(horizontal, vertical);
//#if UNITY_EDITOR
//        if (Input.GetKey(KeyCode.D))
//        {
//            rb.velocity = new Vector3(0, 0, 1 * speed * Time.deltaTime);
//        }
//#endif
    }

    private void PlayerMovementAnimation(float horizontal)
    {
        
      
    }
    private void PlayerJumpAnimation(float vertical)
    {
        if (Input.GetKey(KeyCode.Space))// && isGrounded)
        {
            // SoundManager.Instance.PlayMusic(Sounds.PlayerJump);
            //animator.SetBool("Jump", true);
            rb.velocity = new Vector3(0, jumpVelocity, 0);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            //SoundManager.Instance.PlayMusic(Sounds.PlayerLand);
            //animator.SetBool("Jump", false);
            rb.velocity = new Vector3(0, jumpFallVelocity, 0);
        }
    }

    private void MoveCharacter(float horizontal, float verticle)
    {
        // Move character horizontally
        Vector3 position = transform.position;
        position.z += horizontal * speed * Time.deltaTime;
        transform.position = position;

        // Move character vertically
        if (verticle > 0)
        {
            rb.AddForce(new Vector3(0f, jump,0f), ForceMode.Impulse);
        }
    }
}
