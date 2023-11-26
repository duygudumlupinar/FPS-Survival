using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    PATROL,
    CHASE,
    ATTACK
}

public class EnemyController : MonoBehaviour
{
    private EnemyAnimator enemyAnimator;
    private NavMeshAgent navMeshAgent;

    private EnemyState enemyState;

    public float walkSpeed = 0.5f;
    public float runSpeed = 4f;
    public float chaseDistance = 7f;
    public float currentChaseDistance;
    public float attackDistance = 1.8f;
    public float chaseAfterHitDistance = 2f;

    public float patrolRadius = 50f;
    public float attackWait = 3f;
    public float attackTimer;
    public float patrolTime = 15f;
    public float patrolTimer;

    private Transform target;

    void Awake()
    {
        enemyAnimator = GetComponent<EnemyAnimator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("Player").transform;
    }

    private void Start()
    {
        enemyState = EnemyState.PATROL;
        currentChaseDistance = chaseDistance;
        patrolTimer = patrolTime;
    }
    void Update()
    {
        if(enemyState == EnemyState.PATROL)
        {
            Patrol();
        }
        if (enemyState == EnemyState.CHASE)
        {
            Chase();
        }
        if (enemyState == EnemyState.ATTACK)
        {
            Attack();
        }
    }
   
    void Patrol()
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = walkSpeed;
        patrolTimer += Time.deltaTime;

        //the enemy will patrol in the random route for as long as patrol time
        if(patrolTimer > patrolTime)
        {
            //after the patrol time a new route will be generated
            SetRandomDestination();
            patrolTimer = 0;
        }

        if(navMeshAgent.velocity.sqrMagnitude > 0)
        {
            enemyAnimator.Walk();
        }

        if(Vector3.Distance(transform.position, target.position) <= chaseDistance)
        {
            enemyAnimator.StopWalk();
            enemyState = EnemyState.CHASE;
        }
        
    }

    void Chase()
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = runSpeed;

        navMeshAgent.SetDestination(target.position);

        if (navMeshAgent.velocity.sqrMagnitude > 0)
        {
            enemyAnimator.Run();
        }

        if (Vector3.Distance(transform.position, target.position) <= attackDistance)
        {
            enemyAnimator.StopRun();
            enemyState = EnemyState.ATTACK;
        }

        if (chaseDistance != currentChaseDistance)
        {
            chaseDistance = currentChaseDistance;
        }
        else if (Vector3.Distance(transform.position, target.position) > chaseDistance)
        {
            //in this case player is running away from  the enemy after being noticed

            enemyAnimator.StopRun();
            enemyState = EnemyState.PATROL;
        }
    }

    void Attack()
    {
        navMeshAgent.velocity = Vector3.zero;
        navMeshAgent.isStopped = true;
        attackTimer += Time.deltaTime;
        if(attackTimer > 2f)
        {
            enemyAnimator.Attack();
            attackTimer = 0;
        }

        if (Vector3.Distance(transform.position,target.position) > attackDistance + chaseAfterHitDistance)
        {
            //in this case player is running away in the middle of attack

            enemyState = EnemyState.CHASE;
        }
        

        
    }

    void SetRandomDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
        randomDirection += transform.position;
        NavMeshHit navMeshHit;
        NavMesh.SamplePosition(randomDirection, out navMeshHit, patrolRadius, -1);
        navMeshAgent.SetDestination(navMeshHit.position);
    }
}
