using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reader : MonoBehaviour
{
    private void Start()
    {
        Debug.Log((int)CSVReader.Read("Item")[1]["attack"]);
    }
}
