using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public float levelSpeed;
    public float lifeTimer;

    public GameManager gameManager;
    public HealthManager healthManager;

    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.mgrGame;
        healthManager = HealthManager.mgrHealth;
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        moveSelf();
        
    }

    private void moveSelf()
    {
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0)
        {
            Destroy(gameObject);
        }

        // TODO vector left
        transform.position = transform.position + Vector3.left * Time.deltaTime * levelSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // pickup item will identify itself before deciding what to do        
            pickup_item(this.gameObject);

            // delete from the screen
            Destroy(this.gameObject);
        }
    }

    
    private void pickup_item(GameObject gameobject)
    {
        // string pickupName = gameobject.name;
        string pickupTag = gameobject.tag;

        switch (pickupTag)
        {
            case "Coin":
                gameManager.addCoinScore();
                break;

            case "Health":
                itemHeal(pickupTag);       
                break;

            case "Powerup":
                Debug.Log("Picked up a powerup");
                itemPowerup(pickupTag);
                break;

            default:
                // do nothing
                break;

        }
        // TODO add numCoin
    }

    private void itemHeal(string tagHeal)
    {
        GameObject[] itemsHeal = GameObject.FindGameObjectsWithTag(tagHeal);
        GameObject item;
        /* note: gameObject refers to the GameObject this script is attached to in unity */


        // check if small or large health item
        for (int i = 0; i < itemsHeal.Length; i++)
        {
            // temp item
            item = itemsHeal[i];

            // confirm that item is using the tag in unity
            if (item.name == gameObject.name)
            {
                // FIXME
                if (gameObject.name.Contains("heal_small"))
                {
                    
                    healthManager.addHealth(1);
                    audioManager.PlaySFX(audioManager.pickupHealth);
                    break;
                }

                if (gameObject.name.Contains("heal_large"))
                {
                    
                    healthManager.addHealth(3);
                    audioManager.PlaySFX(audioManager.pickupHealth);
                    break;
                }
            };
        }
    }

    private void itemPowerup(string tagPowerup)
    {
        GameObject[] itemsPowerup = GameObject.FindGameObjectsWithTag(tagPowerup);
        GameObject item;
        /* note: gameObject refers to the GameObject this script is attached to in unity */

        // check which powerup was used
        for (int i = 0; i < itemsPowerup.Length; i++)
        {
            // temp item
            item = itemsPowerup[i];

            // confirm that item is using the tag in unity
            if (item.name == gameObject.name)
            {
                if (gameObject.name.Contains("jacko"))
                {
                    Debug.Log("jacko");
                    // TODO Speedup
                    // gameManager.addHealth();
                    audioManager.PlaySFX(audioManager.powerupShield);
                    break;
                }

                if (gameObject.name.Contains("bone"))
                {
                    Debug.Log("bone");
                    // TODO Invincibility 
                    // gameManager.addHealth();
                    audioManager.PlaySFX(audioManager.powerupSB);
                    break;
                }
            };
        }


    }


}
