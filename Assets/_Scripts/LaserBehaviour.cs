using UnityEngine;
using System.Collections;
/* LaserBehaviour.cs
 * Pedro Bento - 300843658
 * Laser behaviour script
 * Contains laser functionality
 * 
 * Last modified: 03/10/2016
 * Revision:
 * 2-angle dispersion - random
 * 1-default from tutorial
 * 
 */
public class LaserBehaviour : MonoBehaviour
{

    // How long the laser will live
    public float lifetime = 2.0f;
    // How fast will the laser move
    public float speed = 5.0f;
    // How much damage will this laser do if we hit an enemy
    public int damage = 1;


    // Use this for initialization
    void Start()
    {

        // The game object that contains this component will be
        // destroyed after lifetime seconds have passed
       //Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector3.up * Time.deltaTime * speed);

    }
}
