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
    private TextMeshProUGUI text;
    [SerializeField]
    private RawImage back;
    private string levelName;


    public void StartFade(bool fadeIn, string name)
    {
        levelName = name;
        StartCoroutine(Fade(fadeIn));
    }
    private IEnumerator Fade(bool fadeIn)
    {
        if (fadeIn)
        {
            text.text = levelName;
            yield return new WaitForSeconds(2);
            for (int i = 0; i <= 10; i++)
            {

                text.alpha -= 0.1f;
                back.CrossFadeAlpha(0, 1, false);
                yield return new WaitForEndOfFrame();

            }
        }
        else
        {
            for (int i = 0; i <= 10; i++)
            {
                text.text = levelName;
                text.alpha += 0.1f;
                back.CrossFadeAlpha(1, 1, false);
                yield return new WaitForEndOfFrame();

            }
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(levelName);
        }
    }
}
