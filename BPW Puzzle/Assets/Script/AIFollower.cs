using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
//Generated by ChatGPT
//Reformatted by me (It didn't work on first generation)

public class AIFollower : MonoBehaviour
{
    private List<PlayerData> playerData;
    private int counter = 0;
    private int iDCounter;
    private int start;
    private int length;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private SpriteRenderer diamond;
    [SerializeField] private SpriteRenderer circle;
    [SerializeField]private BoxCollider2D box;

    void Start()
    {
        sprite = this.gameObject.GetComponent<SpriteRenderer>();
        box = this.gameObject.GetComponent<BoxCollider2D>();
    }
    void ReceiveData(PlayerData data)
    {
        playerData = data.pD;

      
        length = playerData.Count;
    }
    void Setstart(int startAt)
    {
        start = startAt;
        counter = start;

    }
    void Reset()
    {
        counter = start;
        iDCounter = 0;
        sprite.enabled = true;
        box.enabled = true;
        diamond.enabled = true;
        circle.enabled = true;
    }

    void Update()
    {
        if (counter <= length)
        {

            PlayerData currentData = playerData[counter];
            transform.position = currentData.position;
            transform.eulerAngles = new Vector3(0, 0, currentData.rotation);


            // Do something here to simulate the interaction event
            // Debug.Log("Interacted with object at position " + currentData.position + " and rotation " + currentData.rotation);


            counter++;

        }
        if (counter >= length)
        {
            sprite.enabled = false;
            box.enabled = false;
            diamond.enabled = false;
            circle.enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
