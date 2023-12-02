using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class enemyAIPatrol : MonoBehaviour
{
    GameObject player;
    NavMeshAgent agent;
    [SerializeField] LayerMask groundLayer, playerLayer;
    Animator animator;
    Vector3 destPoint;
    bool walkpointSet;
    [SerializeField] float range;
    [SerializeField] float sightRange, attackRange;
    bool playerInSight, playerInAttackRange;
    BoxCollider boxCollider;
    private bool canAttack = false;
    private bool isDead = false;
    private int hitCount = 0;
    private int maxHitCount = 10;
    public Fight fp;
    public int maxHealth = 100;
	public int currentHealth;

	public HealthBar healthBar;
    public int sceneNumber;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player"); 
        animator = GetComponent<Animator>();
        boxCollider = GetComponentInChildren<BoxCollider>();
        currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        playerInSight = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if (!playerInSight && !playerInAttackRange)
        {
            Patrol();
        }
        if (playerInSight && !playerInAttackRange)
        {
            Chase();
        }
        if (playerInSight && playerInAttackRange)
        {
            Attack();
        }
    }

    void Chase()
    {
        animator.SetTrigger("Chase");
        agent.SetDestination(player.transform.position);
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        agent.SetDestination(player.transform.position);
    }

    void Patrol()
    {
        if (!walkpointSet)
        {
            SearchForDest();
        }
        if (walkpointSet)
        {
            agent.SetDestination(destPoint);
        }
        if (Vector3.Distance(transform.position, destPoint) < 1)
        {
            walkpointSet = false;
        }
    }
    public void TakeHit(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if(currentHealth <= 0 )
        {
            TriggerLoseAnimation();
        }
    }
     public void TriggerLoseAnimation()
    {
        // Trigger the lose animation
        animator.SetTrigger("Lose");

        // Call a function to load the next scene after the animation finishes
        StartCoroutine(LoadNextSceneAfterAnimation());
        isDead = true;
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }
    IEnumerator LoadNextSceneAfterAnimation()
    {
        yield return new WaitForSeconds(2f); // Adjust the time according to the length of the lose animation
        int sceneName = sceneNumber;
        SceneManager.LoadScene(sceneName);
    }

    void SearchForDest()
    {
        float z = Random.Range(-range, range);
        float x = Random.Range(-range, range);

        destPoint = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);

        if (Physics.Raycast(destPoint, Vector3.down, groundLayer))
        {
            walkpointSet = true;
        }
    }

    void EnableAttack()
    {
        boxCollider.enabled = true;

        // Find the player and apply damage
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            Player player = playerObject.GetComponent<Player>();
            if (player != null && fp.isBlocking)
            {
                player.TakeDamage(0);
            }
            else if (player != null)
            {
                player.TakeDamage(4);
            }

        }
    }
    void DisableAttack()
    {
        boxCollider.enabled = false;
    }
    
}
