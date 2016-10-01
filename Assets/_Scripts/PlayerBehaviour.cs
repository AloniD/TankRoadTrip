using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerBehaviour : MonoBehaviour
{

    // Movement modifier applied to directional movement.
    public float playerSpeed = 400.0f;

    private float actualSpeed;

    public float rotationspeed;

    //symmetrical edge of road, going outside this slows down car (deal damage?)
    public float Road_Edge = 300.0f;
    public float Map_Edge = 335.0f;

    public GameObject scrollingBG;

    // Use this for initialization
    void Start()
    {
        actualSpeed = playerSpeed;
        scrollingBG = GameObject.Find("BG");
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate player to face mouse
        //not used for shmup
        //Rotation();

        // Move the player's body
        Movement();

    }


    // Will move the player based off of keys pressed
    void Movement()
    {
        // The movement that needs to occur this frame
        Vector3 movement = new Vector3();
        // Check for input
        movement.x += Input.GetAxisRaw("Horizontal");
        movement.y += Input.GetAxisRaw("Vertical");
        /*
        * If we pressed multiple buttons, make sure we're only
        * moving the same length.
        */
        movement.Normalize();
        // Check if we pressed anything
        //we want precision movement, so no dragging motion.
        if (movement.magnitude > 0)
        {
            // If we did, move in that direction
            this.transform.Translate(movement * Time.deltaTime * actualSpeed, Space.World);
            //space boundaries. This isn't really any slower than using 
            if (this.transform.position.x > Map_Edge)
            {
                this.transform.position = new Vector3(Map_Edge, this.transform.position.y, this.transform.position.z);
            }
            if (this.transform.position.x < -1 * Map_Edge)
            {
                this.transform.position = new Vector3(-1*Map_Edge, this.transform.position.y, this.transform.position.z);
            }
            if (this.transform.position.y > 535)
            {
                this.transform.position = new Vector3(this.transform.position.x, 535, this.transform.position.z);
            }
            if (this.transform.position.y < -535)
            {
                this.transform.position = new Vector3(this.transform.position.x, -535, this.transform.position.z);
            }

            if (this.transform.position.x > Road_Edge || this.transform.position.x < -1 * Road_Edge)
            {
                this.actualSpeed = playerSpeed/2;
                //shake();

            }
            else
            {
                this.actualSpeed = playerSpeed;
            }

        }

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name.Contains("Enemy"))
        {

        }

    }

    void shake()
    {
        float angle = 10.0f;
        float rotationspeed = 10.0f;

        Quaternion rot = Quaternion.Euler(new Vector3(0, 0, (angle +
        90) * Time.deltaTime * rotationspeed));
        // Assign the ship's rotation
        this.transform.rotation = rot;
        if(this.transform.rotation.z >= 10 || this.transform.rotation.z <= -10)
        {
            angle *= -1;
        }
    }

    // Will rotate the ship to face the mouse.
    // Not used in shmup
    /*
    void Rotation()
    {
        // We need to tell where the mouse is relative to the
        // player
        Vector3 worldPos = Input.mousePosition;
        worldPos = Camera.main.ScreenToWorldPoint(worldPos);
        /*
        * Get the differences from each axis (stands for
        * deltaX and deltaY)
        *
        float dx = this.transform.position.x - worldPos.x;
        float dy = this.transform.position.y - worldPos.y;
        // Get the angle between the two objects
        float angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        /*
        * The transform's rotation property uses a Quaternion,
        * so we need to convert the angle in a Vector
        * (The Z axis is for rotation for 2D).
        *
        Quaternion rot = Quaternion.Euler(new Vector3(0, 0, angle +
        90));
        // Assign the ship's rotation
        this.transform.rotation = rot;
    }
    */
}
