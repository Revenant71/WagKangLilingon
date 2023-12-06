using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myCanvas : MonoBehaviour
{
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void goPage()
    {
        audioManager.PlaySFX(audioManager.buttonPress);
    }

}
