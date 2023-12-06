using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{
    [Header(" Audio Source ")]
    [SerializeField] AudioSource musicSource1;
    [SerializeField] AudioSource musicSource2;
    [SerializeField] AudioSource sfx1;
    //[SerializeField] AudioSource sfx2;
    //[SerializeField] AudioSource sfx3;


    [Header(" Audio Clip ")]
    public AudioClip walkBat;
    public AudioClip walkSlime;
    public AudioClip musicGameplay1;
    public AudioClip musicGameplay2;
    public AudioClip newHighscore;
    public AudioClip startHowl;
    public AudioClip pickupCoin;
    public AudioClip pickupHealth;
    public AudioClip powerupShield;
    public AudioClip powerupSB;
    public AudioClip buttonPress;
    public AudioClip musicMM;
    public AudioClip deathPlayer;
    public AudioClip hurtPlayer;
    public AudioClip walkPlayer;

    private void Start()
    {
        musicSource1.clip = musicMM;
        musicSource1.Play();


    }

    public void PlaySFX(AudioClip clip)
    {
        sfx1.PlayOneShot(clip);
    }

    public void StopSFX(AudioSource sfxSource)
    {
        sfxSource.Stop();
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource2.clip = musicGameplay2;
        musicSource2.Play();
    }

    public void StopMusic(AudioSource source)
    {
        source.Stop();
    }
}
