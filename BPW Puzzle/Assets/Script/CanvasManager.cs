using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private RawImage back;
    [SerializeField] private RawImage flash;
    [SerializeField]
    private string levelName;

    public void StartFade(bool fadeIn)
    {

        StartCoroutine(Fade(fadeIn));
    }
    public void StartFlash()
    {
        StartCoroutine(Flash());
    }
    private IEnumerator Fade(bool fadeIn)
    {
        back.enabled = true;
        if (fadeIn)
        {

            yield return new WaitForSeconds(2);
            for (int i = 0; i <= 10; i++)
            {

                back.CrossFadeAlpha(0, 1, false);
                yield return new WaitForEndOfFrame();

            }
            back.enabled = false;
        }
        else
        {

            for (int i = 0; i <= 10; i++)
            {

                back.CrossFadeAlpha(1, 1, false);
                yield return new WaitForEndOfFrame();

            }
           
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(levelName);
        }
    }
    private IEnumerator Flash()
    {   //the rawimage alpha thing i got from here. (why cant it just work normally?)
        //https://answers.unity.com/questions/1420434/change-image-alpha-of-a-raw-image-component-stored.html
        float alpha = 1f;
        Color currentcolour = flash.color;
        currentcolour.a = alpha;
        flash.color = currentcolour;
        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i <= 10; i++)
        {
            Debug.Log(flash.color.a); 
            currentcolour = flash.color;
            currentcolour.a = alpha;
            flash.color = currentcolour;
            alpha -= 0.1f;
            yield return new WaitForEndOfFrame();


        }
    }
}
