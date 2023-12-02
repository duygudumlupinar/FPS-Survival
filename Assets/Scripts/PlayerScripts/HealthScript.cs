using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealthScript : MonoBehaviour
{
    private EnemyAnimator enemyAnimator;
    private NavMeshAgent agent;
    private EnemyController enemyController;
    public float health = 100f;
    private bool isDead;
    public bool isEnemy;
    public bool isPlayer;

    void Awake()
    {
        if (isEnemy)
        {
            enemyAnimator = GetComponent<EnemyAnimator>();
            enemyController = GetComponent<EnemyController>();
            agent = GetComponent<NavMeshAgent>();

        }
        if(isPlayer)
        {

        }
    }


    public void ApplyDamage(float damage)
    {
        if(isDead) 
            return;

        health -= damage;

        if(isPlayer)
        {
            if (isDead)
            {
                PlayerDied();
            }
        }
        
        if (isEnemy)
        {
            if(enemyController.Enemy_State == EnemyState.PATROL)
            {
                enemyController.chaseDistance = 50f;
            }

            if (isDead)
            {
                EnemyDied();
            }
        }

    }

    void PlayerDied()
    {
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerAttack>().enabled = false;

        Invoke("RestartGame", 3f);
    }

    void EnemyDied()
    {
        enemyController.enabled = false;
        agent.velocity = Vector3.zero;
        agent.isStopped = true;
        enemyAnimator.Die();

        Invoke("TurnOffGameObject", 3f);
    }

    void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }
    void TurnOffGameObject()
    {
        gameObject.SetActive(false);
    }
}
