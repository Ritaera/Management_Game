using DiceGame.Singleton;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : SingletonMonoBase<SoundManager>
{


    override protected void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
        private void Start()
    {
        if (SceneManager.GetActiveScene().name == "StartScene")
        {
            BgmAuioSource.clip = BgmAudioClip[0];
            BgmAuioSource.Play();
        }
        else if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            BgmAuioSource.clip = BgmAudioClip[1];
            BgmAuioSource.Play();
        }
        else if (SceneManager.GetActiveScene().name == "GamePlay")
        {
            BgmAuioSource.clip = BgmAudioClip[2];
            BgmAuioSource.Play();
        }
        else if (SceneManager.GetActiveScene().name == "GameOver")
        {
            BgmAuioSource.clip = BgmAudioClip[3];
            BgmAuioSource.Play();
        }
    }

    [Header("++++++++BGMAuioSource++++++++")]
    public AudioSource BgmAuioSource;

    [Header("++++++++SFXAuioSource++++++++")]
    public AudioSource SfxAuioSource;

    [Header("++++++++BGMAuioClip++++++++")]
    public AudioClip[] BgmAudioClip;

    [Header("++++++++SFXAuioClip++++++++")]
    public AudioClip[] SfxAuioClip;



}