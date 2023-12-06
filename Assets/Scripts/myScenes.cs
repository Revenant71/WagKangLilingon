using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class myScenes : MonoBehaviour
{
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        
        //if (audioManager != null)
        //{
        //    Debug.Log(audioManager.name);
        //}
        
    }

    // Start is called before the first frame update
    void Start()
    {
        // audioManager.PlaySFX(audioManager.buttonPress);
        
    }

    public void goStart()
    {
        audioManager.PlaySFX(audioManager.buttonPress);
        SceneManager.LoadScene("Gameplay");
    }

    public void goExit()
    {
        audioManager.PlaySFX(audioManager.buttonPress);
        SceneManager.LoadScene("MenuMain");
    }

    public void gameRestart()
    {
        audioManager.PlaySFX(audioManager.buttonPress);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
}
