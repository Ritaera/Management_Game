using DiceGame.Singleton;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : SingletonMonoBase<SoundManager>
{

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "GameScene1")
        {
            BgmAuioSource.Stop();
        }
        else if (SceneManager.GetActiveScene().name == "Main")
        {
            BgmAuioSource.clip = BgmAudioClip[0];
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