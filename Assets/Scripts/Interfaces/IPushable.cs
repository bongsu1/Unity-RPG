using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPushable
{
    public void Push(PlayerInteractor player);

    public void Pop();

    IEnumerator PushRoutine();
}
