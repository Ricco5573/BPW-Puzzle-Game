using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    private bool isActive;
    private SpriteRenderer sR;
    private Animator animator;
    private CanvasManager cM;
    private AudioSource aS;
    [SerializeField]
    private AudioClip on;
    [SerializeField]
    private AudioClip off;
    private IEnumerator routine;
    private bool player;
    private bool last;
    // Start is called before the first frame update
    void Start()
    {
        sR = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        cM = FindObjectOfType<CanvasManager>();
        aS = GetComponent<AudioSource>();
        routine = Timer();

    }
    private void Update()
    {

        if(!isActive && !player && last)
        {

            aS.PlayOneShot(off);
            animator.SetBool("On", false);
        }
        last = isActive;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Clone")
        {
            StopCoroutine(routine);
            routine = Timer();
            StartCoroutine(routine);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Clone")
        {
            if (!isActive)
                aS.PlayOneShot(on);

            player = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Clone")
        {
            Debug.Log("Trigger exit");
            player = false;
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
        yield return new WaitForSeconds(1);
        isActive = false;
       
    }

}
