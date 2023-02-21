using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

//Written by chatGPT
//I've had to rewrite it somewhat, and i have to still go back and refactor it to make it look more proffesional
public class CharacterController2D : MonoBehaviour
{
    [SerializeField]private float speed = 0.1f;
    [SerializeField]private GameObject spawnObject;

    private Rigidbody2D rigidbody2D;
   [SerializeField] private List<PlayerData> playerData = new List<PlayerData>();
    private List<GameObject> Clones = new List<GameObject>();
    private int startAt = 0;
    private int frameCounter = 0;



    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        TrackPlayer();
        Move();
        if (Input.GetKeyDown(KeyCode.F))
        {
            SpawnObject();
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


    }

    void Move()
    {
        // https://answers.unity.com/questions/607618/unity2d-make-object-face-mouse.html Source
        Vector3 mouseScreen = Input.mousePosition;
        Vector3 mouse = Camera.main.ScreenToWorldPoint(mouseScreen);
        this.gameObject.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x) * Mathf.Rad2Deg - 90);

        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal"); ;


        Vector3 movement = new Vector3(horizontal, 0, vertical);
        // Move the player towards or away from the mouse
        movement = transform.up * vertical;

        // Normalize the movement vector and make it proportional to the speed per second
        movement = movement.normalized * (speed * Time.deltaTime);

        // Move the player
        rigidbody2D.MovePosition(transform.position + movement);
    }

    void TrackPlayer()
    {
        PlayerData data = new PlayerData(transform.position, transform.eulerAngles.z, Time.time);
        playerData.Add(data);

    }

    void SpawnObject()
    {
        //so turns out, This method runs the code BEFORE START(). Which is interesting, but by god its annoying
        GameObject newObject = Instantiate(spawnObject, new Vector2(0,0), Quaternion.identity);
        newObject.SendMessage("ReceiveData", new PlayerData(playerData));
        newObject.SendMessage("Setstart", startAt);
        Debug.Log("Send message");
        Clones.Add(newObject);
        foreach(GameObject clone in Clones)
        {
           clone.SendMessage("Reset", null, SendMessageOptions.DontRequireReceiver);
        }
        startAt = playerData.Count;
        
    }
}



public struct PlayerData
{
    public Vector2 position;
    public float rotation;
    public float time;

    public List<PlayerData> pD;

    public PlayerData(Vector2 position, float rotation, float time)
    {
        this.position = position;
        this.rotation = rotation;
        this.time = time;
        this.pD = new List<PlayerData>();

    }

    public PlayerData(List<PlayerData> playerData)
    {
        this.position = Vector2.zero;
        this.rotation = 0.0f;
        this.time = 0.0f;
        this.pD = playerData;

    }
}