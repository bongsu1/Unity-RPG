using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] LayerMask layerMask;
    [SerializeField] float range;
    [SerializeField] float angle;
    [SerializeField] int damage;

    private float cosRange;

    private void Awake()
    {
        cosRange = Mathf.Cos(angle * Mathf.Deg2Rad);
    }

    private void Attack()
    {
        int rand = Random.Range(0, 2);
        if (rand == 0)
        {
            animator.SetTrigger("Attack");
        }
        else
        {
            animator.SetTrigger("Attack2");
        }
    }

    Collider[] colliders = new Collider[20];
    private void AttackTiming()
    {
        int size = Physics.OverlapSphereNonAlloc(transform.position, range, colliders, layerMask);
        for (int i = 0; i < size; i++)
        {
            Vector3 dirToTarget = (colliders[i].transform.position - transform.position).normalized;
            // if (Vector3.Angle(transform.forward, dirToTarget) > angle) // 연산이 느림
            if (Vector3.Dot(transform.position, dirToTarget) < cosRange) // 최적화
                continue;

            IDamagable damageable = colliders[i].GetComponent<IDamagable>();
            damageable?.TakeDamage(damage);
        }
    }

    private void OnAttack(InputValue value)
    {
        Attack();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
