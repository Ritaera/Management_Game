using UnityEngine;
using UnityEngine.UI;
using static SceneManagers;


public class UIStartMenu : MonoBehaviour
{
    private Button _startButton;


    private void Awake()
    {
        _startButton = transform.Find("Panel/Panel - Buttons/Button - GameStart").GetComponent<Button>();
    }

    private void Start()
    {
        _startButton.onClick.AddListener(StartButtonClick);
    }

    public void StartButtonClick()
    {
        SceneManagers.LoadScenes(MoveScene.InGame);
    }
}
