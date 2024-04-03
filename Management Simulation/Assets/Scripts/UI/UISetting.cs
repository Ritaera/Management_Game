using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UISetting : MonoBehaviour
{
    private Button _lobby;
    private Button _cancel;
    private Button _setting;
    private Scrollbar _scrollbar;
    private GameObject _panel;


    private void Awake()
    {
        // Todo: 자동 컴포넌트 연결 코드 작성하기.
        _lobby = transform.Find("Panel - Setting/Game Button/Button - Lobby").GetComponent<Button>();
        _cancel = transform.Find("Panel - Setting/Game Button/Button - Cancel").GetComponent<Button>();
        _setting = transform.Find("Button - Setting").GetComponent<Button>();
        _scrollbar = transform.Find("Panel - Setting/Sound/Scrollbar - Volume").GetComponent<Scrollbar>();
        _panel = transform.Find("Panel - Setting").gameObject;
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
        // Todo: 로비씬으로 이동.
    }

    // KJH => 취소버튼 클릭시 호출 함수.
    public void CancelButtonClick()
    {
        _panel.SetActive(false);
    }


    public void Update()
    {
        // KJH => 스크립트 비활성화 상태면 Update 함수 실행 안함.
        SoundVolumeUpDown();
    }


    // KJH => 사운드바 소리조절 코드
    public void SoundVolumeUpDown()
    {
        // Todo: 사운드 조절 기능 추가하기.
    }

    // KJH =>  설정 버튼 눌렀을때 이 함수 호출.
    public void SettingButtonClick()
    {
        _panel.SetActive(true);
    }

}
