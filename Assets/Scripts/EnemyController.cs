using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum EnemyType { Patrolling, PlaceHolder, Attackking, }
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private float speed;
    [SerializeField] private int health;
    [SerializeField] GameObject[] waypoints;
    int currentWaypointIndex = 0;
    public PlayerController playerController;

    private void Update()
    {
        if (!GameManager.instance.canProgress )
        {
            return;
        }
        else
        {
            if(enemyType == EnemyType.Patrolling)
            if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].transform.position) < .1f)
            {
                currentWaypointIndex++;
                if (currentWaypointIndex >= waypoints.Length)
                {
                    currentWaypointIndex = 0;
                }
            }

            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, speed * Time.deltaTime);
        }
    
    }

    //if player collides with enemy palyer will die
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            TakeDamage(30);
            playerController.KillPlayer();         
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
