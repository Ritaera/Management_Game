using System;
using System.Collections.Generic;
using UnityEngine;

public class UIUpgrade : MonoBehaviour
{
    PlayerController2 _playerController;


    private void Awake()
    {
        if (_playerController == null)
        {
            _playerController=FindFirstObjectByType<PlayerController2>();
        }
    }
    public void Upgrade()
    {
        if (_playerController.HitName == "sam smith")
        {
            //Todo: 대장간 작업 처리.
        }
        else if (_playerController.HitName == "maria")
        {
            //Todo: 성당처리
        }
        else if(_playerController.HitName == "수상한 미모의 여종업원")
        {
            //Todo: 모럼가길드
        }
        else if (_playerController.HitName == "수상한 미모의 하녀")
        {
            //Todo: 성
        }
        else
        {
            //Todo: 은행
        }
    }
}
