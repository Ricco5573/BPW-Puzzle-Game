using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelTransition : MonoBehaviour
{
    [SerializeField]
    private CanvasManager canvas;
    void Start()
    {
        canvas.StartFade(true); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            canvas.StartFade(false);
        }
    }
}

