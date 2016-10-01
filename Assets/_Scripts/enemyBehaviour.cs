using UnityEngine;
using System.Collections;
/* enemyBehaviour.cs
 * Pedro Bento - 300843658
 * Enemy behaviour script
 * Contains movement functionality
 * 
 * Last modified: 03/10/2016
 * Revision:
 * 2-movement has target
 * 1-default from tutorial
 * 
 */

public class enemyBehaviour : MonoBehaviour
{
    
    private Vector3 camerapos;
    public Transform explosion;
    public AudioClip hitSound;
    public AudioSource hitSource;
    public float speed = 2.0f;
    // How many times should I be hit before I die
    public int health = 2;

    //the controller object that spawned this - could also be used to make enemies that spawn other enemies because of weak binding.
    private gameController controller;



    // Use this for initialization
    void Start()
    {
        
        hitSource = GetComponent<AudioSource>();
        camerapos = GameObject.Find("camera").transform.position;
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<gameController>();

    }

    // Update is called once per frame
    void Update()
    {

        Movement();
       

    }

    //movement
    //must obtain some way to make it cooler
    void Movement()
    {
        float moveSpeed = speed * Time.deltaTime;
        Vector3 movector = new Vector3(0, -1 * moveSpeed, 0);
        this.transform.Translate(movector);
        if(this.transform.position.y < -600)
        {
            Disable();
        }
    }

    void Disable()
    {
        gameObject.SetActive(false);
        controller.KilledEnemy();
        controller.IncreaseScore(10);
    }
    
    void OnCollisionEnter2D(Collision2D theCollision)
    {
        // Uncomment this line to check for collision
        //Debug.Log("Hit"+ theCollision.gameObject.name);
        // this line looks for "laser" in the names of
        // anything collided.
        if (theCollision.gameObject.name.Contains("Laser"))
        {
            LaserBehaviour laser =
            theCollision.gameObject.GetComponent("LaserBehaviour") as LaserBehaviour;
            health -= laser.damage;
            Destroy(theCollision.gameObject);
            AudioSource.PlayClipAtPoint(hitSound, camerapos);

        }
        if (health <= 0)
        {

            this.gameObject.SetActive(false);
            

            //check if explosion particle is set
            if (explosion)
            {
                GameObject exploder = ((Transform)Instantiate(explosion, this.transform.position, this.transform.rotation)).gameObject;
                Destroy(exploder, 2.0f);
            }
        }
    }
}
