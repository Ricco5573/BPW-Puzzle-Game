using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    private bool isActive;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Clone")
        {
            StartCoroutine(Timer());
        }
    }

    public bool GetActive()
    {
        return isActive;
    }
    private IEnumerator Timer()
    {
        isActive = true;
        yield return new WaitForSeconds(2.5f);
        isActive = false;
    }

}
