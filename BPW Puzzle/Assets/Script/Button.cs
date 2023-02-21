using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    private bool isActive;
    private SpriteRenderer sR;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        sR = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
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
        animator.SetBool("On", true);
        yield return new WaitForSeconds(2.5f);
        isActive = false;
        animator.SetBool("On", false);
    }

}
