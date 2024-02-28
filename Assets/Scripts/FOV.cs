using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FOV : MonoBehaviour
{
    [SerializeField] Transform eyePoint;
    [SerializeField] float range;
    [SerializeField] float angle;
    [SerializeField] LayerMask playerMask;
    [SerializeField] LayerMask obstacleMask;

    // Test
    public bool isLockOn = false;

    private float cosRange;

    private void Awake()
    {
        cosRange = Mathf.Cos(angle * 0.5f * Mathf.Deg2Rad);
    }

    private void Update()
    {
        PlayerCheck();
    }

    Collider[] colliders = new Collider[20];
    private void PlayerCheck()
    {
        int size = Physics.OverlapSphereNonAlloc(eyePoint.position, range, colliders, playerMask);
        if (size == 0) isLockOn = false;
        for (int i = 0; i < size; i++)
        {
            Vector3 toTargetDir = (colliders[i].transform.position - eyePoint.position).normalized;
            if (Vector3.Dot(toTargetDir, eyePoint.forward) < cosRange)
                continue;

            float toTargetDistance = Vector3.Distance(colliders[i].transform.position, eyePoint.position);
            if (Physics.Raycast(eyePoint.position, toTargetDir, toTargetDistance, obstacleMask))
                continue;

            isLockOn = true;
            Debug.DrawRay(eyePoint.position, toTargetDir * toTargetDistance, Color.red);
            Quaternion lookDir = Quaternion.LookRotation(new Vector3(toTargetDir.x, 0, toTargetDir.z));
            transform.rotation = Quaternion.Lerp(transform.rotation, lookDir, Time.deltaTime * 10f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(eyePoint.position, range);

        Gizmos.color = Color.yellow;
        Vector3 drawAngle = eyePoint.TransformDirection(Mathf.Sin(angle * 0.5f * Mathf.Deg2Rad), 0, Mathf.Cos(angle * 0.5f * Mathf.Deg2Rad));
        Vector3 drawAngle2 = eyePoint.TransformDirection(Mathf.Sin(-angle * 0.5f * Mathf.Deg2Rad), 0, Mathf.Cos(-angle * 0.5f * Mathf.Deg2Rad));
        Gizmos.DrawRay(eyePoint.position, drawAngle * range);
        Gizmos.DrawRay(eyePoint.position, drawAngle2 * range);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(eyePoint.position, eyePoint.forward * range);
    }
}
