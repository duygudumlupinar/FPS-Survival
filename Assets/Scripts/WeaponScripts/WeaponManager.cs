using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private WeaponHandler[] weapons;
    [SerializeField] private GameObject crosshair;
    
    private Animator _animator;
    private int currentWeapon;

    void Start()
    {
        _animator = GetComponent<Animator>();
        currentWeapon = 0;
        weapons[currentWeapon].gameObject.SetActive(true);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (currentWeapon == 0)
            {
                ChangeWeapon(1);
                _animator.SetTrigger("TakeOut");
                _animator.SetTrigger("GunIdle");
                crosshair.SetActive(true);
            }
            else
            {
                ChangeWeapon(0);
                _animator.SetTrigger("SwordIdle");
                crosshair.SetActive(false);
            }
        }
    }

    void ChangeWeapon(int weaponCode)
    {
        //put down the current weapon
        weapons[currentWeapon].gameObject.SetActive(false);
        //pick up the other weapon
        weapons[weaponCode].gameObject.SetActive(true);
        currentWeapon = weaponCode;
    }

    public int GetCurrentWeapon()
    {
        return currentWeapon;
    }
}
