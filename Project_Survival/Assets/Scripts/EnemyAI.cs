using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform  player;
    public LayerMask whatIsGround, whatIsPlayer;
    private Animator animator;
    public float wanderSpeed = 4f;
    public float chaseSpeed = 7f;


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

    public void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Awake()
    {
        //player = GameObject.Find("Dean_Rigged").transform;
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
        animator.SetBool("SkeletonAware", false);
        agent.speed =  wanderSpeed;
    }
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        animator.SetBool("SkeletonAware", true);
        agent.speed =  chaseSpeed;
    }
    private void AttackPlayer()
    {
        // make sure enemy doesn't move 
        agent.SetDestination(transform.position);

        //transform.LookAt(player);

        if(!alreadyAttacked)
        {
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 12f, ForceMode.Impulse);
            rb.AddForce(transform.up * 3f, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
            FindObjectOfType<AudioManager>().AudioTrigger(AudioManager.SoundFXCat.SkeletonAttack, transform.position, 1f);
            animator.SetBool("SkeletonAttack", true);
        }
        
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
