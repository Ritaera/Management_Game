using DiceGame.Singleton;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : SingletonMonoBase<SoundManager>
{

<<<<<<< HEAD

    override protected void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
        private void Start()
    {
        if (SceneManager.GetActiveScene().name == "StartScene")
=======
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "GameScene1")
        {
            BgmAuioSource.Stop();
        }
        else if (SceneManager.GetActiveScene().name == "Main")
>>>>>>> origin/L.Gyeol
        {
            BgmAuioSource.clip = BgmAudioClip[0];
            BgmAuioSource.Play();
        }
<<<<<<< HEAD
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
=======
>>>>>>> origin/L.Gyeol
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