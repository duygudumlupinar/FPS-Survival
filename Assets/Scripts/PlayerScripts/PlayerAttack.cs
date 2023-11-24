using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject muzzleFlash;
    [SerializeField] private AudioSource shootSound;
    [SerializeField] private Camera mainCamera;

    private Animator _animator;
    private WeaponManager _weaponManager;
    void Start()
    {
        _weaponManager = GetComponent<WeaponManager>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(_weaponManager.GetCurrentWeapon() == 1)
            {
                _animator.SetTrigger("GunAttack");
                shootSound.Play();
                muzzleFlash.SetActive(true);
                StartCoroutine(MuzzleFlashOff());
                FireBullet();
            }
            else
            {
                _animator.SetTrigger("SwordAttack");
            }
        }
    }

    IEnumerator MuzzleFlashOff()
    {
        yield return new WaitForSeconds(0.1f);
        muzzleFlash.SetActive(false);
    }

    void FireBullet()
    {
        RaycastHit hit;

        if(Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit))
        {
            print("we hit" + hit.transform.gameObject.name);
        }

    }
}
