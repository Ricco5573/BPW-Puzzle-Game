using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

//Written by chatGPT
//I've had to rewrite it somewhat, and i have to still go back and refactor it to make it look more proffesional
public class CharacterController2D : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.1f;
    [SerializeField]
    private GameObject spawnObject;
    [SerializeField]
    private List<PlayerData> playerData = new List<PlayerData>();
    [SerializeField]
    private CanvasManager canva;
    private Rigidbody2D rigidbody2D;
    private Animator anim;
    private Quaternion toMouse;
    private List<GameObject> Clones = new List<GameObject>();
    private int startAt = 60;
    private int frameCounter = 0;
    private int dir;
    private AudioSource aS;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        aS= GetComponent<AudioSource>();
    }

    void Update()
    {
        TrackPlayer();
        Move();
        if (Input.GetKeyDown(KeyCode.F))
        {
            SpawnObject();
            aS.PlayOneShot(aS.clip);
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            startAt = playerData.Count;
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }


    }

    void Move()
    {
        // https://answers.unity.com/questions/607618/unity2d-make-object-face-mouse.html Source
        Vector3 mouseScreen = Input.mousePosition;
        Vector3 mouse = Camera.main.ScreenToWorldPoint(mouseScreen);
        toMouse = Quaternion.Euler(0, 0, Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x) * Mathf.Rad2Deg - 90);
        bool moving;
        //this.gameObject.transform.rotation = toMouse;
        Vector3 directionToMouse = (mouse - transform.position).normalized;

        float angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;
        angle = (angle + 360) % 360; // convert angle to 0-360 range
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        if (angle >= 301 || angle <= 60)
        {
            anim.SetInteger("Dir", 1);
            this.gameObject.transform.rotation = new Quaternion(0, 180, 0, 0);
            dir = 1;
        }
        else if (angle >= 61 && angle <= 120)
        {
            anim.SetInteger("Dir", 0);
            this.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
            dir = 0;
        }
        else if (angle >= 121 && angle <= 240)
        {
            anim.SetInteger("Dir", 3);
            this.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
            dir = 3;
        }
        else if (angle >= 241 && angle <= 300)
        {
            anim.SetInteger("Dir", 2);
            this.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
            dir = 2;
        }

        if (vertical == 0 && horizontal == 0)
        {
            moving = false;
        }
        else
        {
            moving = true; 
        }


        anim.SetBool("Move", moving);
        if (Input.GetKey(KeyCode.W))
        {
            rigidbody2D.velocity = directionToMouse * speed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rigidbody2D.velocity = -directionToMouse * speed;
        }
        else
        {
            rigidbody2D.velocity = Vector2.zero;
        }
    }

    void TrackPlayer()
    {
        //Creates a new Data point for the playerData struct, that will later be send to the Clones.
        PlayerData data = new PlayerData(transform.position, toMouse.eulerAngles.z, dir);
        playerData.Add(data);

    }

    void SpawnObject()
    {
        //so turns out, This method runs the code BEFORE START(). Which is interesting, but by god its annoying
        //Creates a clone, and sends all the accumulated playerData to it, as well as an int for it to know where in the struct to start
        canva.StartFlash();
        GameObject newObject = Instantiate(spawnObject, new Vector2(0,-12), Quaternion.identity);
        newObject.SendMessage("ReceiveData", new PlayerData(playerData));
        newObject.SendMessage("SetStart", startAt); 
        Clones.Add(newObject);
        foreach(GameObject clone in Clones)
        {
           clone.SendMessage("Reset", null, SendMessageOptions.DontRequireReceiver);
        }
        //Sets start int so that the next clone will start when you summoned the last one.
        startAt = playerData.Count;
        
    }
}


//This struct keeps track of the player position and rotation, and inside of it has a list of itself to keep track of the data
//of every frame.
public struct PlayerData
{
    public Vector2 position;
    public float rotation;
    public int direction;
    public List<PlayerData> pD;

    public PlayerData(Vector2 position, float rotation, int direction)
    {
        this.position = position;
        this.rotation = rotation;
        this.direction = direction;
        this.pD = new List<PlayerData>();

    }

    public PlayerData(List<PlayerData> playerData)
    {
        this.position = Vector2.zero;
        this.rotation = 0.0f;
        this.direction = 0;
        this.pD = playerData;

    }
}