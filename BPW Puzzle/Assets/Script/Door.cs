using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Door : MonoBehaviour
{
    [SerializeField]
    private List<Button> buttons = new List<Button>();
    private int amountOn;
    private SpriteRenderer sR;
    private Animator anim;
    private BoxCollider2D bC;

    private void Start()
    {
        bC = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sR = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        foreach(Button button in buttons) {

            if (button.GetActive())
            {

                amountOn++;
            }
            if(amountOn == buttons.Count)
            {
                StartCoroutine(Open());
            }
        }
        amountOn = 0;
    }

    private IEnumerator Open()
    {
        bC.enabled = false;
        anim.SetBool("Open", true);
        yield return new WaitForSecondsRealtime(3);
        anim.SetBool("Open", false);
        bC.enabled = true;
    }
}
