using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public string tagToDestroy;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagToDestroy))
        {
            //Human's rigidbody is under the parent human object
            Destroy(other.transform.parent.gameObject);
        }
        else
        {
            Debug.Log("Something else");
        }
    }

}
