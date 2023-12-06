using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public static HealthManager mgrHealth;
    
    public PlayerMain player;
    public Sprite heartFull;
    public Sprite heartEmpty;
    public Image[] arrayHearts;

    private AudioManager audioManager;

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();

        if (mgrHealth == null)
        {
            //Debug.Log("new health manager");
            mgrHealth = this;

            //DontDestroyOnLoad(gameObject);

            //audioManager = FindObjectOfType<AudioManager>();

            //player = (PlayerMain)GameObject.FindGameObjectWithTag("Player");
            //heartFull = Resources.Load<Sprite>("Assets/Sprites/Items/crop_health_full");
            //heartEmpty = Resources.Load<Sprite>("Assets/Sprites/Items/crop_health_empty");

            //arrayHearts = new Image[3];
            //arrayHearts[0] = GameObject.Find("unit_health").GetComponent<Image>();
            //arrayHearts[1] = GameObject.Find("unit_health (1)").GetComponent<Image>();
            //arrayHearts[2] = GameObject.Find("unit_health (2)").GetComponent<Image>();
        }
        else
        {
            //Debug.Log("old health manager");
            mgrHealth = this;
            //Destroy(gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
        // empty hearts
        for (int i = 0; i < arrayHearts.Length; i++)
        {
            arrayHearts[i].sprite = heartEmpty;
        }

        try
        {
            // full hearts
            for (int i = 0; i < player.currentHealth; i++)
            {
                arrayHearts[i].sprite = heartFull;
            }

        }
        catch (System.IndexOutOfRangeException e)
        {

            //throw e;
            return;
        }



    }

    public void addHealth(int amtHeal)
    {
        if ((amtHeal + player.currentHealth) > player.maxHealth)
        {
            // add to player's score
            player.score += 20f;

            amtHeal = player.maxHealth - player.currentHealth;
            player.currentHealth += amtHeal;

        } else {
            if (amtHeal >= player.maxHealth)
            {
                // large health    
                amtHeal = player.maxHealth - player.currentHealth;
                player.currentHealth += amtHeal;
            }
            else if (amtHeal < player.maxHealth && amtHeal > 0)
            {
                // small health    
                player.currentHealth += amtHeal;
            }
        }

    }
}
