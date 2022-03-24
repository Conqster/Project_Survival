using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public int maxHealth;

    public int currentHealth;
    public NavMeshAgent agent;

    public Transform  player;

    public LayerMask whatIsGround, whatIsPlayer;
    
    public int health;

    // Patrolling 
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // Attacking 
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    // States 
    public float sightRange, attackRange;
    public bool playerInsightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        currentHealth = maxHealth;
    }
    
    private void Update()
    {
        // check for sight and attack range
        playerInsightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
       
    if (!playerInsightRange && !playerInAttackRange) Patroling();
    if (playerInsightRange && !playerInAttackRange) ChasePlayer();
    if (playerInAttackRange && playerInsightRange) AttackPlayer();
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if(walkPointSet)
        agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // Walkpoint reach 
        if (distanceToWalkPoint.magnitude < 1f)
        walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        // calculate random point in range 
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z +  randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        
        walkPointSet = true;
    }
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
    private void AttackPlayer()
    {
        // make sure enemy doesn't move 
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if(!alreadyAttacked)
        {
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) Invoke(nameof(DestroyEnemy), .5f);
    }

    /*public void MaxHealth(int maxHealth)
    {
        maxHealth = 100;
    }*/

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
