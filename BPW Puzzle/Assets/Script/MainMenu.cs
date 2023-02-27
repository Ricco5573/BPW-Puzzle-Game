using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MainMenu : MonoBehaviour
{

    [SerializeField]
    private string levelName;
    [SerializeField]
    private CanvasManager canva;
    public void OnClickStart()
    {
        StartCoroutine(ClickStart());
    }

    private IEnumerator ClickStart()
    {
        yield return new WaitForSeconds(1);
        canva.StartFade(false);
    }

    public void OnClickQuit()
    {   
        StartCoroutine(ClickQuit());
    }
    private IEnumerator ClickQuit()
    {
        yield return new WaitForSeconds(0.2f);
        canva.StartFade(false);
        yield return new WaitForSeconds(0.5f);
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
