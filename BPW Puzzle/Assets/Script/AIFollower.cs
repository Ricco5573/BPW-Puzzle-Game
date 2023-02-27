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
        //On spawn, the clone will receive the playerdata containing movement and rotation, along with the start location
        playerData = data.pD;

        //Because a struct is a reference, we need to define the end of this clones playtime as well. Which we do here
        //by getting the current length of the list, meaning that it will end when the player pressed the button to reset time.
        length = playerData.Count;
    }

    public void SetStart(int listNum)
    {
        start = listNum;
    }

    void Reset()
    {
        //This function will reset the clone back to its starting position, restarting its movement
        counter = start;
        iDCounter = 0;
        sprite.enabled = true;
        box.enabled = true;
        diamond.enabled = true;
        circle.enabled = true;
    }

    void Update()
    {
        //Here, it runs through one piece of the playerdata list each frame. Updating its position and rotation to be the same
        //as that of the data. Simulating movement.
        if (counter <= length)
        {

            PlayerData currentData = playerData[counter];
            transform.position = currentData.position;
            transform.eulerAngles = new Vector3(0, 0, currentData.rotation);
            counter++;

        }
        if (counter >= length)
        {
            //when it reaches the end of the list, it will disable itself, to simulate resetting time.
            sprite.enabled = false;
            box.enabled = false;
            diamond.enabled = false;
            circle.enabled = false;
        }
    }

    public void CaughtPlayer()
    {
        //If the clone spots the player, that will create a time paradox, and restart the level
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
 
}
