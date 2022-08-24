using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallManeger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("human"))
        {
            Debug.Log("sssssss");

        }
    }
}
