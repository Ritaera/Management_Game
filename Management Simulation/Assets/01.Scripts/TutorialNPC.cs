using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SceneManagers;

public class TutorialNPC : MonoBehaviour
{
    PlayerController2 _playerController;

    // Update is called once per frame

    private void Awake()
    {
        if (_playerController == null)
        {
            _playerController = FindFirstObjectByType<PlayerController2>();
        }

    }
    public void ScenceMove()
    {
        if (_playerController.GetHitName() == PlayerController2.EHitName.None)
        {
            Utils.LogRed("_playerController.GetHitName()���� None�� ��ȯ�ϸ� �ȵ�.");
            return;
        }
        if (_playerController.GetHitName() == PlayerController2.EHitName.Heize)
        {
            SceneManagers.LoadScenes(MoveScene.InGame);
        }
    }
}
