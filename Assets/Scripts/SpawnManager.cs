using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    // spawn timers
    public float spawnintervalEnemy;
    public float spawnintervalCoin;
    public float spawnintervalPickup;
    public float spawnintervalObstacle;

    public float timerEnemy;
    public float timerCoin;
    public float timerPickup;
    public float timerObstacle;

    // adjust spawner location of ff. items
    public Vector3 spawnareaEnemy;
    public float width_saEnemy;
    public float height_saEnemy;

    public Vector3 spawnareaCoin;
    public float width_saCoin;
    public float height_saCoin;

    public Vector3 spawnareaPickup;
    public float width_saPickup;
    public float height_saPickup;

    public Vector3 spawnareaObstacle;
    public float width_saObstacle;
    public float height_saObstacle;


    public int paddingCoin;
    public GameObject coin;
    public GameObject[] arrayEnemy;
    public GameObject[] arrayPickup;
    public GameObject[] arrayObstacle;

    private int countCoin;
    public int maxspawnCoin;
    //public int maxspawnPickup = 1;

    // Color for Gizmos
    public Color enemySpawnColor = Color.red;
    public Color coinSpawnColor = Color.yellow;
    public Color pickupSpawnColor = Color.green;
    public Color obstacleSpawnColor = Color.blue;

    // Draw Gizmos to visualize spawn areas
    private void OnDrawGizmos()
    {
        Gizmos.color = enemySpawnColor;
        Gizmos.DrawWireCube(spawnareaEnemy, new Vector3(width_saEnemy, height_saEnemy, 1f) );

        Gizmos.color = coinSpawnColor;
        Gizmos.DrawWireCube(spawnareaCoin, new Vector3(width_saCoin, height_saCoin, 1f) );

        Gizmos.color = pickupSpawnColor;
        Gizmos.DrawWireCube(spawnareaPickup, new Vector3(width_saPickup, height_saPickup, 1f) );

        Gizmos.color = obstacleSpawnColor;
        Gizmos.DrawWireCube(spawnareaObstacle, new Vector3(width_saObstacle, height_saObstacle, 1f) );
    }

    // Start is called before the first frame update
    void Start()
    {
        //timerEnemy = 0f;
        //timerCoin = 0f;
        //timerPickup = 0f;
        //timerObstacle = 0f;

        //gameManager = GameManager.mgrGame;
        //healthManager = HealthManager.mgrHealth;

        // assign spawners
        timerEnemy = spawnintervalEnemy;
        timerCoin = spawnintervalCoin;
        timerPickup = spawnintervalPickup;
        timerObstacle = spawnintervalObstacle;

    }

    // Update is called once per frame
    void Update()
    {
        //while ((countCoin + 1) % 10 == 0)
        //{

        //}
        timerEnemy -= Time.deltaTime;
        timerCoin -= Time.deltaTime;
        timerPickup -= Time.deltaTime;
        timerObstacle -= Time.deltaTime;


        if (timerCoin <= 0)
        {
            spawnCoins();
            timerCoin = spawnintervalCoin;
        }

        if (timerPickup <= 0)
        {
            spawnPickup();
            timerPickup = spawnintervalPickup;
        }
        
        if (timerEnemy <= 0)
        {
            spawnEnemy();
            timerEnemy = spawnintervalEnemy;
        }
        
        if (timerObstacle <= 0)
        {
            spawnObstacle();
            timerObstacle = spawnintervalObstacle;
        }

    }

    private void spawnCoins()
    {
        for (; countCoin < maxspawnCoin; countCoin++)
        {
            // add space per coin
            float xAxis = spawnareaCoin.x - (width_saCoin / 2) + (countCoin * (width_saCoin + paddingCoin));
           
            Vector3 positionCoin = new Vector3(
                //UnityEngine.Random.Range(spawnareaCoin.x - width_saCoin / 2, spawnareaCoin.x + width_saCoin / 2),
                //paddingCoin,
                xAxis,
                spawnareaCoin.y,
                spawnareaCoin.z
            );

            // spawn coins, within spawn area, can be seen with gizmos
            GameObject newCoin = Instantiate(coin, positionCoin, Quaternion.identity);
            
            // set reference to every coin object
            Pickup coinScript = newCoin.GetComponent<Pickup>();
            if ( coinScript != null )
            {
                coinScript.gameManager = GameManager.mgrGame;
            }

            // spacingCoin += paddingCoin;

 
        }

        if (countCoin >= maxspawnCoin)
        {
            countCoin = 0;

            double[] locations = {1, -1.27, -3.27};
            
            int randomSelect = UnityEngine.Random.Range(0, locations.Length);
            double randomHeight = locations[randomSelect]; 

            spawnareaCoin.y = (float) randomHeight;
        }
    }

    //Vector3 positionCoin as param
    private void spawnPickup()
        {
        
        // pick random item from array
        int randomIndex = UnityEngine.Random.Range(0, arrayPickup.Length);
        GameObject randomPickup = arrayPickup[randomIndex];

        //if (countCoin % 10 == 0)
        //{
        Vector3 positionPickup = new Vector3(
            spawnareaPickup.x,
            spawnareaPickup.y,
            spawnareaPickup.z
        );

        // spawn pickups, within spawn area, can be seen with gizmos
        GameObject newPickup = Instantiate(randomPickup, positionPickup, Quaternion.identity);

        // set reference to every pickup object
        Pickup pickupScript = newPickup.GetComponent<Pickup>();

        // get component from "bar_health" child of CanvasPlayer tagged as "HealthManager", use its HealthManager script
        //GameObject healthObject = GameObject.FindGameObjectWithTag("HealthManager");
        //HealthManager healthbar = healthObject.GetComponent<HealthManager>();

        if (pickupScript != null)
        {
            pickupScript.gameManager = GameManager.mgrGame;

            // FIXME healthmanager reference is successful, but cloned health potions do not do anything when touched
            pickupScript.healthManager = HealthManager.mgrHealth;

            // random y axis for spawner
            double[] locations = {1, -1.27, -3.27};

            int randomSelect = UnityEngine.Random.Range(0, locations.Length);
            double randomHeight = locations[randomSelect];

            spawnareaPickup.y = (float)randomHeight;
        }

    }
    private void spawnEnemy()
    {
        // pick random enemy from array
        int randomIndex = UnityEngine.Random.Range(0, arrayEnemy.Length);
        GameObject randomEnemy = arrayEnemy[randomIndex];

        // randomize y axis of spawned enemy
        Vector3 positionEnemy = new Vector3(
            spawnareaEnemy.x,
            UnityEngine.Random.Range(spawnareaEnemy.y - width_saEnemy / 2, spawnareaEnemy.y + width_saEnemy / 2),
            spawnareaEnemy.z
        );

        //
        Instantiate(randomEnemy, positionEnemy, Quaternion.identity);

        // for loop to horizontal
        // TODO vector3 spawn position + random w/ range
        // TODO delta time spawn
    }

    private void spawnObstacle()
    {
        // pick random obstacle from array
        int randomIndex = UnityEngine.Random.Range(0, arrayObstacle.Length);
        GameObject randomObstacle = arrayObstacle[randomIndex];

        Vector3 positionObstacle = new Vector3(
            spawnareaObstacle.x,
            spawnareaObstacle.y,
            spawnareaObstacle.z
        );

        Instantiate(randomObstacle, positionObstacle, Quaternion.identity);
        // for loop to horizontal
        // TODO vector3 spawn position + random w/ range
        // TODO delta time spawn
    }

}
