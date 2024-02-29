using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public void Get()
    {
        Debug.Log("Item Get");
        Destroy(gameObject);
    }
}
