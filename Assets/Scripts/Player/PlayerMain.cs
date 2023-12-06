using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerMain : MonoBehaviour
{
    public TMP_Text ScoreTxt;
    public GameObject gameoverScreen;
    public int currentHealth;
    public int maxHealth;
    private int minHealth;
    public float JumpForce;
    public float SlideTime;
    public Transform groundCheck;
    public LayerMask groundLayer;

    Rigidbody2D rb;
    BoxCollider2D boxcol;
    public float score;
    public float highScore;

    private float swipeStartTime;
    private Vector2 startSwipePosition;
    private float slideEndTime;  // To keep track of when the slide should end
    private AudioManager audioManager;

    [SerializeField]
    bool isGrounded = false;
    bool isSliding = false;

    Color gizmoColor = Color.magenta;

    private void OnDrawGizmos()
    {
        if (boxcol != null)
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawWireCube(boxcol.bounds.center, boxcol.bounds.size);
        }
    }

    private void Awake()
    {
        Time.timeScale = 1;

        audioManager = FindObjectOfType<AudioManager>();

        minHealth = maxHealth % (maxHealth - 1);
        currentHealth = maxHealth;
        
        rb = GetComponent <Rigidbody2D>();
        boxcol = GetComponent <BoxCollider2D>();
        
        score = 0;
        //PlayerPrefs.DeleteAll();
        highScore = PlayerPrefs.GetFloat("HighScore", 0);
        
        //Debug.Log("best score: " + highScore);
        gameoverScreen.SetActive(false);
    }

    void Update()
    {
        checkBest(score, highScore);

        if (currentHealth < minHealth)
        {
            die();
        }
        else
        {
            score += Time.deltaTime * 4;
            ScoreTxt.text = score.ToString("F0");

            if (isGrounded == true)
            {
                //audioManager.PlaySFX(audioManager.walkPlayer);
                actionSwipe();
            } else
            {
                //audioManager.walkPlayer.Stop();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hurt(collision);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hurt(collision);

        if (collision.gameObject.CompareTag("Ground"))
        {
            if (isGrounded == false)
            {
                isGrounded = true;
            }
        }
    }

    private void actionSwipe()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                swipeStartTime = Time.time;
                startSwipePosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                float swipeTime = Time.time - swipeStartTime;
                Vector2 swipeDelta = touch.position - startSwipePosition;

                if (swipeTime < 0.5f && swipeDelta.y > 20f)
                {
                    isGrounded = false;
                    rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
                }
                else if (swipeTime < 0.5f && swipeDelta.y < -20f)
                {
                    StartSlide();
                }
            }
        }

        if (isSliding && Time.time >= slideEndTime)
        {
            ResetSlide();
        }
    }

    private void StartSlide()
    {
        if (!isSliding)
        {
            isSliding = true;
            boxcol.size = new Vector2(boxcol.size.x * 1.7f, boxcol.size.y * 0.5f);
            //transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 90);
    
            slideEndTime = Time.time + SlideTime;
        }
    }

    private void ResetSlide()
    {
        isSliding = false;
        boxcol.size = new Vector2(0.47f, 0.75f);
    
        //transform.rotation = Quaternion.Euler(0, 0, 0);

    }

    private void hurt(Collision2D collideObject)
    {
        string collideTag = collideObject.gameObject.tag;

        switch (collideTag)
        {
            case "Hazard":
                checkHazard(collideTag);
                break;

            case "Enemy":
                checkEnemy(collideTag);
                break;

            default:
                break;
        }
    }

    private void hurt(Collider2D collideObject)
    {
        string collideTag = collideObject.gameObject.tag;

        switch (collideTag)
        {
            case "Hazard":
                checkHazard(collideTag);
                break;

            case "Enemy":
                checkEnemy(collideTag);
                break;

            default:
                break;
        }
    }

    private void die()
    {
        Time.timeScale = 0;

        //audioManager.StopMusic(audioManager.musicSource1);
        //audioManager.StopMusic(audioManager.musicSource2);
        //audioManager.StopSFX(audioManager.startHowl);

        //audioManager.PlaySFX(audioManager.deathPlayer);

        gameoverScreen.SetActive(true);
    }

    private void Bounce(int bounceFactor)
    {
        rb.velocity = new Vector2(rb.velocity.x, 1.2f) * bounceFactor;
    }

    private void checkBest(float score, float highscore)
    {
        score = Mathf.Round(score);
      
        highscore = score;
        highscore = Mathf.Round(highscore);

        if (score > highscore)
        {
            audioManager.PlaySFX(audioManager.newHighscore);
            PlayerPrefs.SetFloat("HighScore", highscore);
        }
    }

    private void checkHazard(string tagHazard)
    {
        audioManager.PlaySFX(audioManager.hurtPlayer);

        GameObject[] hurtHazard = GameObject.FindGameObjectsWithTag(tagHazard);
        GameObject hazard;
        int dmgHazard = 1;

        for (int i = 0; i < hurtHazard.Length; i++)
        {
            hazard = hurtHazard[i];

            if (hazard.tag == tagHazard && currentHealth > 0)
            {
                if (hazard.name.Contains("spike_long"))
                {
                    currentHealth -= dmgHazard;
                    Bounce(6);
                    break;
                }

                if (hazard.name.Contains("spike_short"))
                {
                    currentHealth -= dmgHazard;
                    Bounce(6);
                    break;
                }

                if (hazard.name.Contains("saw"))
                {
                    currentHealth -= dmgHazard;
                    Bounce(6);
                    break;
                }
            }
        }
    }

    private void checkEnemy(string tagEnemy)
    {
        audioManager.PlaySFX(audioManager.hurtPlayer);

        GameObject[] hurtEnemy = GameObject.FindGameObjectsWithTag(tagEnemy);
        GameObject enemy;
        int dmgBat = 1;
        int dmgPinky = 1;

        for (int i = 0; i < hurtEnemy.Length; i++)
        {
            enemy = hurtEnemy[i];

            if (enemy.tag == tagEnemy && currentHealth > 0)
            {
                if (enemy.name.Contains("bat-blue"))
                {
                    currentHealth -= dmgBat;
                    Bounce(4);
                    Destroy(enemy);
                    break;
                }

                if (enemy.name.Contains("bat-red"))
                {
                    currentHealth -= dmgBat;
                    Bounce(4);
                    Destroy(enemy);
                    break;
                }

                if (enemy.name.Contains("pinky"))
                {
                    currentHealth -= dmgPinky;
                    Bounce(2);
                    Destroy(enemy);
                    break;
                }
            }
        }
    }

    public static explicit operator PlayerMain(GameObject v)
    {
        throw new NotImplementedException();
    }
}
