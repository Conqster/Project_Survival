using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIHitting : MonoBehaviour
{

    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public float wanderSpeed = 4f;
    public float chaseSpeed = 7f;
    public GameObject rightFist;
    private Animator animator;

    
    // Patrolling 
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // Attacking 
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    // States 
    public float sightRange, attackRange;
    public bool playerInsightRange, playerInAttackRange;

    public void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Awake()
    {
        //player = GameObject.Find("Dean_Rigged").transform;
        //player = GetComponent<Transform>();
        //player = GameObject.Find("Player_Body").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // check for sight and attack range
        playerInsightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInsightRange && !playerInAttackRange) Patrolling();
        if (playerInsightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInsightRange) AttackPlayer();

    }

    private void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
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

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))

            walkPointSet = true;
        animator.SetBool("SkeletonAware", false);
        agent.speed = wanderSpeed;
    }
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        animator.SetBool("SkeletonAware", true);
        agent.speed = chaseSpeed;
    }


    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        //transform.LookAt(player);

        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
            FindObjectOfType<AudioManager>().AudioTrigger(AudioManager.SoundFXCat.EnemyShoot, transform.position, 1f);
            animator.SetBool("SkeletonAttack", true);
        }

    }

    private void OnDrawGizmos()
    {
        Vector3 _player = player.transform.position;
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(_player, 1);
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void activateFist()
    {
        rightFist.GetComponent<Collider>().enabled = true;
    }

    public void deactivateFist()
    {
        rightFist.GetComponent<Collider>().enabled = false;
    }
}
