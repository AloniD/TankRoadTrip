using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
/* gameController.cs
 * Pedro Bento - 300843658
 * EEnemy spawner script
 * Throws enemies at the player
 * 
 * Last modified: 03/10/2016
 * Revision:
 * 2-instantiation all done at Start(), object destruction removed
 * 1-default from tutorial
 * 
 */
public class gameController : MonoBehaviour
{

    public GameObject rock;
    public GameObject cactus;
    public Transform enemyBullet;

    //this controllers's empty game object position
    private Vector3 enemyPos;

    public float Road_Edge = 300.0f;

    [Header("Wave Properties")]
    // We want to delay our code at certain times
    public float timeBeforeSpawning = 3.5f;
    public float timeBetweenEnemies = 1.5f;
    public float timeBeforeWaves = 3.0f;
    public int enemiesPerWave = 10;
    private int currentNumberOfEnemies = 0;

    [Header("User Interface")]
    //values
    private int score = 0;
    private int wave = 0;
    private int lives = 5;

    [Header("Pile Trackers")]
    //these keep tabs on the next object to use
    private int enemyToUse;
    //text objects
    public Text scoreText;
    public Text waveText;
    public Text LivesText;
    public Text GameOver;
    public Text FinalScore;
    public Button Restart;

    //enemy pile object
    public List<GameObject> enemyPile = new List<GameObject>();
    //has to be pretty large, upper waves get pretty xboxhueg
    //200 enemies last until wave 19, where 190 enemies spawn, that should be pretty much unbeatable.
    private int enemyPileNumber = 200;



    // Use this for initialization
    void Start()
    {
        StartCoroutine(SpawnRocks());
        StartCoroutine(SpawnCacti());
        scoreText.text = "Score: " + score;
        waveText.text = "Wave: " + wave;
        LivesText.text = "Lives: " + lives;

        enemyPos = this.transform.position;

        //instantiate a big ol' pile of rocks


        for (int i = 0; i < enemyPileNumber; i++)
        {
            GameObject obj = (GameObject)Instantiate(rock, enemyPos, Quaternion.identity);
            obj.SetActive(false);
            enemyPile.Add(obj);
        }

        //we always start off from the beginning of the pile
        enemyToUse = 0;

        InvokeRepeating("TimeIncrease", 1, 1);


    }

    // Update is called once per frame
    void Update()
    {

    }

    // Coroutine used to spawn enemies
    IEnumerator SpawnRocks()
    {
        // Give the player time before we start the game
        yield return new WaitForSeconds(timeBeforeSpawning);
        // After timeBeforeSpawning has elapsed, we will enter
        // this loop
        while (true)
        {
            // Don't spawn anything new until all of the previous
            // wave's enemies are dead
            if (currentNumberOfEnemies <= 0)
            {
                wave++;
                enemiesPerWave += wave;
                waveText.text = "Wave: " + wave;

                int activatedEnemies = 0;

                //activate a wave of enemies
                while (activatedEnemies < enemiesPerWave)
                {
                    if (!enemyPile[enemyToUse].activeInHierarchy)
                    {
                        float randDistance = Random.Range(-1*Road_Edge, Road_Edge);

                        enemyPile[enemyToUse].gameObject.SetActive(true);

                        activatedEnemies++;

                        enemyPile[enemyToUse].transform.position = new Vector3(enemyPos.x + randDistance, enemyPos.y, 0);

                        enemyToUse++;

                        if (enemyToUse == enemyPile.Count)
                        {
                            enemyToUse = 0;
                        }

                        currentNumberOfEnemies++;
                    }
                    // We want the enemies to come from the top of the screen
                    // we randomize the spawn point above it
                    

                    // Using the distance and direction we set the
                    // position

                    
                    yield return new WaitForSeconds(timeBetweenEnemies);
                }
            }
            // How much time to wait before checking if we need to
            // spawn another wave
            yield return new WaitForSeconds(timeBeforeWaves);
        }
    }

    IEnumerator SpawnCacti()
    {
        // Give the player time before we start the game
        yield return new WaitForSeconds(timeBeforeSpawning);
        // After timeBeforeSpawning has elapsed, we will enter
        // this loop
        while (true)
        {
            // Don't spawn anything new until all of the previous
            // wave's enemies are dead
            if (currentNumberOfEnemies <= 0)
            {
                wave++;
                enemiesPerWave += wave;
                waveText.text = "Wave: " + wave;

                int activatedEnemies = 0;

                //activate a wave of enemies
                while (activatedEnemies < enemiesPerWave)
                {
                    if (!enemyPile[enemyToUse].activeInHierarchy)
                    {
                        float randDistance = Random.Range(-1*Road_Edge, Road_Edge);

                        enemyPile[enemyToUse].gameObject.SetActive(true);

                        activatedEnemies++;

                        enemyPile[enemyToUse].transform.position = new Vector3(enemyPos.x + randDistance, enemyPos.y, 0);

                        enemyToUse++;

                        if (enemyToUse == enemyPile.Count)
                        {
                            enemyToUse = 0;
                        }

                        currentNumberOfEnemies++;
                    }
                    // We want the enemies to come from the top of the screen
                    // we randomize the spawn point above it
                    

                    // Using the distance and direction we set the
                    // position

                    
                    yield return new WaitForSeconds(timeBetweenEnemies);
                }
            }
            // How much time to wait before checking if we need to
            // spawn another wave
            yield return new WaitForSeconds(timeBeforeWaves);
        }
    }

    // Allows classes outside of GameController to say when we
    // killed an enemy. Adds the enemy back into the pile of unused
    public void KilledEnemy()
    {
        currentNumberOfEnemies--;
    }

    public void IncreaseScore(int increase)
    {
        score += increase;
        scoreText.text = "Score: " + score;
    }
    public void TimeIncrease()
    {
        IncreaseScore(1);
    }

    public void LoseLife()
    {
        lives--;
        if (lives < 0)
        {
            endGame();
        }
        LivesText.text = "Lives: " + lives;
    }

    public void endGame()
    {
        Time.timeScale = 0;

    }
}