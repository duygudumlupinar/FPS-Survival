using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public float damage = 2f;
    public float damageFieldRadius = 1f;
    public LayerMask layerMask;
    
    void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, damageFieldRadius, layerMask);

        if (hits.Length > 0)
        {
            hits[0].gameObject.GetComponent<HealthScript>().ApplyDamage(damage);
        }
    }
}
