using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public static GameManager mgrGame;
    public PlayerMain player;
    public TMP_Text txtCoin;
    public int numCoin;

    private AudioManager audioManager;

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();

        if (mgrGame == null)
        {
            //Debug.Log("new game manager");
            mgrGame = this;
            //DontDestroyOnLoad(gameObject);

            //audioManager = FindObjectOfType<AudioManager>();

            //player = (PlayerMain)GameObject.FindGameObjectWithTag("Player");
            //txtCoin = GameObject.Find("txt_coin").GetComponent<TextMeshPro>();
        }
        else
        {
            //Debug.Log("old game manager");

            // mgrGame = this;
            //Destroy(gameObject);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addCoinScore()
    {
        audioManager.PlaySFX(audioManager.pickupCoin);
        player.score += 10;
    }

    public void addCoin()
    {
        audioManager.PlaySFX(audioManager.pickupCoin);
        numCoin++;
        txtCoin.text = numCoin.ToString();  
    }

    public void addCoin(int newCoin)
    {
        audioManager.PlaySFX(audioManager.pickupCoin);
        numCoin = numCoin + newCoin;
        txtCoin.text = numCoin.ToString();
    }

    // TODO player preferences coins

    public void gamePause(int timePause)
    {
        // only accept values 0 or 1
        if (timePause < 0 || timePause > 1)
        {
            Time.timeScale = 1;
        }

        Time.timeScale = timePause;

        // TODO
        // olyPause.SetActive(pauseMenu);
    }

    public void goStart()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void goExit()
    {
        SceneManager.LoadScene("MenuMain");
    }

    public void gameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void gameEnd()
    {
        // TODO load next scene
        // game over
        //
    }

    public void goScene(string sceneName, GameObject sceneCanvas)
    {
        SceneManager.LoadScene(sceneName);
        sceneCanvas.SetActive(true);
    }


}
