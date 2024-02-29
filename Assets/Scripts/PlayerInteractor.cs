using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] bool debug;

    Collider[] colliders = new Collider[20];
    private void Interact()
    {
        int size = Physics.OverlapSphereNonAlloc(transform.position, range, colliders);
        for (int i = 0; i < size; i++)
        {
            IInteractable interactable = colliders[i].GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact(this);
                break;
            }
        }
    }

    private void OnInteract(InputValue value)
    {
        Interact();
    }

    private void OnDrawGizmosSelected()
    {
        if (!debug)
            return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    private void OnControllerColliderHit(ControllerColliderHit other)
    {
        IPushable button = other.gameObject.GetComponent<IPushable>();
        if (button != null)
            button.Push(this);
    }
}
