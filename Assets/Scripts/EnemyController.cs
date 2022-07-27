using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum EnemyType { Patrolling,PlaceHolder,Attackking,}
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private Transform originPosition;
    [SerializeField] private Transform originPositionForVerticalRayCast;
    [SerializeField] private float range;
    [SerializeField] private float speed;
    [SerializeField] private int health;
    private Animator animator;

    Rigidbody2D rb;
    public PlayerController playerController;
    Vector2 dir = new Vector2(0, -1);
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        //RayCasting();
        if (enemyType == EnemyType.Attackking)
        {
            animator.SetBool("EnemyAttack", true);
        }
    }
    //Enemy flip from one direction to other
    void FlipDirection()
    {
        //Flip();
        speed *= -1;
    }

    //if player collides with enemy palyer will die
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            //SoundManager.Instance.Play(Sounds.PlayerDeath);
            playerController.KillPlayer();
        }
    }

    //function for ray casting for patrolling enemy
    //void RayCasting()
    //{
    //    RaycastHit hit;

    //    if (Physics.Raycast(originPosition.position, dir, range))
    //    {

    //    }
    //    RaycastHit2D rayCast = Physics2D.Raycast(originPositionForVerticalRayCast.position, dir, range);
    //    if (!hit)
    //    {
    //        FlipDirection();
    //    }
    //    if (rayCast)
    //    {
    //        FlipDirection();
    //    }
    //}
    ////patrolling enemy will flip his direction
    //private void Flip()
    //{
    //    Vector2 scale = transform.localScale;
    //    scale.x = -(scale.x);
    //    transform.localScale = scale;
    //}
    //private void FixedUpdate()
    //{
    //    rb.velocity = new Vector2(speed, rb.velocity.y);
    //}

    //public void TakeDamage(int damage)
    //{
    //    health -= damage;
    //    if (health < 0)
    //    {
    //        Die();
    //    }
    //}

    //void Die()
    //{
    //    SoundManager.Instance.Play(Sounds.EnemyDeath);
    //    Destroy(gameObject);
    //}
}
