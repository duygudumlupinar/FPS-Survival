using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponAim
{
    NONE,
    AIM
}
public enum WeaponBulletType
{
    NONE,
    BULLET
}

public class WeaponHandler : MonoBehaviour
{   
    private Animator _animator;
    public WeaponAim weaponAim;

    [SerializeField] private GameObject muzzleFlash;
    [SerializeField] private AudioSource shootSound;
    [SerializeField] private AudioSource reloadSound;
    public WeaponBulletType bulletType;
    public GameObject attackPoint;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Shoot()
    {
        _animator.SetTrigger("SwordAttack");
        shootSound.Play();
    }

    void Aim(bool canAim)
    {
        _animator.SetTrigger("Aim");
    }

    void MuzzleFlashOn()
    {
        muzzleFlash.SetActive(true);
    }
    void MuzzleFlashOff()
    {
        muzzleFlash.SetActive(false);
    }

    void Reload()
    {
        reloadSound.Play();
    }

    void TurnOnAttackPoint()
    {
        attackPoint.SetActive(true);
    }
    
    void TurnOffAttackPoint()
    {
        if(attackPoint.activeInHierarchy)
        {
            attackPoint.SetActive(false);
        }
    }
}
