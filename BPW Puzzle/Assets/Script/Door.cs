using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private List<Button> buttons = new List<Button>();
    private int amountOn;


    void Update()
    {
        foreach(Button button in buttons) {

            if (button.GetActive())
            {

                amountOn++;
            }
            if(amountOn == buttons.Count)
            {
                Debug.Log("Open");
            }
        }
        amountOn = 0;
    }

    private IEnumerator Open()
    {

        yield return new WaitForSecondsRealtime(5);
    }
}
