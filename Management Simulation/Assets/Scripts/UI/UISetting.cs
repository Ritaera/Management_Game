﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UISetting : MonoBehaviour
{
    private Button _lobby;
    private Button _cancel;
    private Scrollbar _scrollbar;

    // KJH => 로비버튼 클릭시 호출함수.
    public void OnLobbyButtonClick()
    {
        // Todo: 로비씬으로 이동.
    }

    // KJH => 취소버튼 클릭시 호출 함수.
    public void OnCancelButtonClick()
    {
        // KJH => 설정창을 안보이게 비활성화 처리하기
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
}
