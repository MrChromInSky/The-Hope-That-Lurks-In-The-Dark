using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffMapKiller : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.transform.position = new Vector3(0, 2, 0);
        }
    }
}
