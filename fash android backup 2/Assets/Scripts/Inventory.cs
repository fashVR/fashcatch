using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool[] isFull;
    public GameObject[] slots;

    private void Update()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if(slots[i].transform.childCount == 2)
            {
                isFull[i] = false;
            }
        }
    }
}
