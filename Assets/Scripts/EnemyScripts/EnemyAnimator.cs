using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public void Walk()
    {
        _animator.SetTrigger("ZombieWalk");
    }
    public void StopWalk()
    {
        _animator.ResetTrigger("ZombieWalk");
    }
    public void Run()
    {
        _animator.SetTrigger("ZombieRun");
    }
    public void StopRun()
    {
        _animator.ResetTrigger("ZombieRun");
    }
    public void Attack()
    {
        _animator.SetTrigger("ZombieAttack");
    }
    public void Die()
    {
        _animator.SetTrigger("ZombieDie");
    }
}
