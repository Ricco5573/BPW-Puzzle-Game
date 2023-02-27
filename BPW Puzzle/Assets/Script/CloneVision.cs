using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneVision : MonoBehaviour
{
    [SerializeField]
    private AIFollower clone;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
            if (collision.gameObject.tag == "Player")
            {
                clone.CaughtPlayer();
            }
        
    }

}
