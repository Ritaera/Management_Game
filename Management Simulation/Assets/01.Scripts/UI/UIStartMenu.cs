<<<<<<< HEAD
using TMPro;
=======
>>>>>>> origin/L.Gyeol
using UnityEngine;
using UnityEngine.UI;
using static SceneManagers;


public class UIStartMenu : MonoBehaviour
{
    private Button _startButton;
<<<<<<< HEAD
    private Button _exitButton;
    private TMP_Text _titleName;
    private float _waitTime= 10;
=======
>>>>>>> origin/L.Gyeol


    private void Awake()
    {
        _startButton = transform.Find("Panel/Panel - Buttons/Button - GameStart").GetComponent<Button>();
<<<<<<< HEAD
        _exitButton = transform.Find("Panel/Panel - Buttons/Button - Exit").GetComponent<Button>();
        _titleName = transform.Find("Panel/Panel - Title/Text (TMP) - TitleName").GetComponent<TMP_Text>();
=======
>>>>>>> origin/L.Gyeol
    }

    private void Start()
    {
        _startButton.onClick.AddListener(StartButtonClick);
<<<<<<< HEAD
        _exitButton.onClick.AddListener(ExitButtonClick);
        InvokeRepeating("ChangeTextColor", 0f, _waitTime * Time.deltaTime);
    }

    private void StartButtonClick()
    {
        SoundManager.instance.SfxAuioSource.clip = SoundManager.instance.SfxAuioClip[0];
        SceneManagers.LoadScenes(MoveScene.Tutorial);
    }

    private void ExitButtonClick()
    {
        Application.Quit();
    }

    private void ChangeTextColor()
    {
        Color randomColor = new Color(Random.value, Random.value, Random.value); // ������ RGB ������ ���� ����
        _titleName.color = randomColor; // TMP_Text�� ��Ʈ ���� ����
=======
    }

    public void StartButtonClick()
    {
        SceneManagers.LoadScenes(MoveScene.InGame);
>>>>>>> origin/L.Gyeol
    }
}
