using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelTransition : MonoBehaviour
{
    [SerializeField]
    private string levelName;
    [SerializeField]
    private string nextLevelName;
    [SerializeField]
    private CanvasManager canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas.StartFade(true, levelName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            canvas.StartFade(false, nextLevelName);
        }
    }
}

