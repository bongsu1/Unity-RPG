using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BottomButton : MonoBehaviour
{
    private Vector3 oldPos;

    private void Start()
    {
        oldPos = transform.position;
    }

    public void Press()
    {
        transform.position = new Vector3(oldPos.x, oldPos.y - 0.5f, oldPos.z);
    }

    public void Pop()
    {
        transform.position = oldPos;
    }
}
