using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PushableAdapter : MonoBehaviour, IPushable
{
    public UnityEvent<PlayerInteractor> OnPushed;
    public UnityEvent OnPoped;


    public void Push(PlayerInteractor player)
    {
        OnPushed?.Invoke(player);
        StartCoroutine(PushRoutine());
    }

    public void Pop()
    {
        OnPoped?.Invoke();
    }

    public IEnumerator PushRoutine()
    {
        yield return new WaitForSeconds(2f); ;
        Pop();
    }
}
