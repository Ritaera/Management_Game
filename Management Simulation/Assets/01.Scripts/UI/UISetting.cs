﻿using UnityEngine;
using UnityEngine.UI;
using static SceneManagers;
using Button = UnityEngine.UI.Button;


public class UISetting : MonoBehaviour, SceneManagers
{
    private Button _lobby;
    private Button _cancel;
    private Button _setting;
    private Scrollbar _scrollbar;
    private GameObject _panel;
    private GameObject _barrier;


    private void Awake()
    {
        _lobby = transform.Find("Panel - Setting/Game Button/Button - Lobby").GetComponent<Button>();
        _cancel = transform.Find("Panel - Setting/Game Button/Button - Cancel").GetComponent<Button>();
        _setting = transform.Find("Button - Setting").GetComponent<Button>();
        _scrollbar = transform.Find("Panel - Setting/Sound/Scrollbar - Volume").GetComponent<Scrollbar>();
        _panel = transform.Find("Panel - Setting").gameObject;
        _barrier = transform.Find("Panel - Setting/Barrier").gameObject;
    }

    private void Start()
    {
        _lobby.onClick.AddListener(LobbyButtonClick);
        _cancel.onClick.AddListener(CancelButtonClick);
        _setting.onClick.AddListener(SettingButtonClick);
    }

    // KJH => 로비버튼 클릭시 호출함수.
    public void LobbyButtonClick()
    {
        SoundManager.instance.SfxAuioSource.clip = SoundManager.instance.SfxAuioClip[0];
        SceneManagers.LoadScenes(MoveScene.Main);
    }

    // KJH => 취소버튼 클릭시 호출 함수.
    public void CancelButtonClick()
    {
        SoundManager.instance.SfxAuioSource.clip = SoundManager.instance.SfxAuioClip[0];
        _panel.SetActive(false);
        _barrier.SetActive(false);
    }


    public void Update()
    {
        // KJH => 스크립트 비활성화 상태면 Update 함수 실행 안함.
        SoundVolumeUpDown();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CancelButtonClick();
        }
    }


    // KJH => 사운드바 소리조절 코드
    public void SoundVolumeUpDown()
    {
        // Todo: 사운드 조절 기능 추가하기.
    }

    // KJH =>  설정 버튼 눌렀을때 이 함수 호출.
    public void SettingButtonClick()
    {
        SoundManager.instance.SfxAuioSource.clip = SoundManager.instance.SfxAuioClip[0];
        _panel.SetActive(true);
        _barrier.SetActive(true);
    }
}
