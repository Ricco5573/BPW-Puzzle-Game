using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI buttons;
    [SerializeField]
    private TextMeshProUGUI pressF;
    [SerializeField]
    private TextMeshProUGUI pressR;
    bool on;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Tutorials());
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !on)
        {
            pressF.enabled = false;
            pressR.enabled = true;
            on = true;

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            buttons.enabled = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(!on)
            pressF.enabled = true;
    }
    private IEnumerator Tutorials()
    {
        yield return new WaitForSeconds(5);
        buttons.enabled = true;

        yield return new WaitForSeconds(1);
        
    }
}
