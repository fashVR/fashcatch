using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fashDetection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fash"))
        {
            
        }
    }
}
