using UnityEngine;
using UnityEngine.UI;
using static SceneManagers;

public class UIEndingBackHome : MonoBehaviour, SceneManagers
{
    private Button _lobby;


    private void Awake()
    {
        _lobby = transform.Find("Button - Lobby").GetComponent<Button>();
    }

    private void Start()
    {
        _lobby.onClick.AddListener(LobbyButtonClick);
    }

    // KJH => �κ��ư Ŭ���� ȣ���Լ�.
    public void LobbyButtonClick()
    {
        SoundManager.instance.SfxAuioSource.clip = SoundManager.instance.SfxAuioClip[0];
        SceneManagers.LoadScenes(MoveScene.Main);
    }
}
