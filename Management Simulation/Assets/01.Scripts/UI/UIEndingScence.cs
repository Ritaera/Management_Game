using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIEndingScene : MonoBehaviour
{
    private TMP_Text _endingText;

    private EndingScriptableObject _endingscript;

    private Image _endingImage;

    // ���� ũ������ ǥ���� �����
    public string[] creditSentences;

    // ���� ��� ���� ������ �ε���
    private int _currentSentenceIndex = 0;

    // ���� ��� ���� ������ ���� �ε���
    private int _currentCharIndex = 0;

    // ���� ���� ����� ���� ������
    public float delayBetweenChars = 0.1f;

    private float _nextCharTime; // ���� ���� ��±����� �ð�

    private bool _isEndOfSentence = false; // ������ �������� ����

    public string loadEndingName = "";

    // ���� ��� �����͸� �ε��� �����ϴ� ����.
    public GameResultData gameResultData;

    [SerializeField]
    private string resultFilePath = "Assets/ResultData/Result.txt";


    private void Awake()
    {
        _endingText = FindFirstObjectByType<UIEndingScene>().GetComponentInChildren<TMP_Text>();
        _endingImage = transform.Find("Image - BG").GetComponent<Image>();
        _endingText.text = string.Empty;
    }

    void Start()
    {
        // ���� �� ������ �� ���� ������ ������ ���� ��� �����͸� �ε�.
        LoadGameResultDataFromFile();

        // ���ǿ���.
        if (gameResultData.Date >= 31)
        {
            if (gameResultData.BeliefPoint >= 70 && gameResultData.CulturePoint >= 70)
            {
                loadEndingName = "HappyBothEnding";
            }
            else if (gameResultData.BeliefPoint >= 70)
            {
                loadEndingName = "HappyBeliefEnding";
            }
            else if (gameResultData.CulturePoint >= 70)
            {
                loadEndingName = "HappyCultureEnding";
            }
        }

        // ��忣��
        if (gameResultData.HappyPoint <= 0)
        {
            loadEndingName = "BadHappyPointEnding";
        }
        else if (gameResultData.SafetyPoint <= 0)
        {
            loadEndingName = "BadSafetyPointEnding";
        }
        else if (gameResultData.Gold <= 0)
        {
            loadEndingName = "BadGoldEnding";
        }

        // ���ǿ���
        //if (GameManager.instance.Date >= 31)
        //{
        //    if (GameManager.instance.BeliefPoint.Value >= 70 && GameManager.instance.CulturePoint.Value >= 70)
        //    {
        //        loadEndingName = "HappyBothEnding";
        //    }
        //    else if (GameManager.instance.BeliefPoint.Value >= 70)
        //    {
        //        loadEndingName = "HappyBeliefEnding";
        //    }
        //    else if (GameManager.instance.CulturePoint.Value >= 70)
        //    {
        //        loadEndingName = "HappyCultureEnding";
        //    }
        //}

        //// ��忣��
        //if (GameManager.instance.HappyPoint.Value <= 0)
        //{
        //    loadEndingName = "BadHappyPointEnding";
        //}
        //else if (GameManager.instance.SafetyPoint.Value <= 0)
        //{
        //    loadEndingName = "BadSafetyPointEnding";
        //}
        //else if (GameManager.instance.Gold <= 0)
        //{
        //    loadEndingName = "BadGoldEnding";
        //}



        Utils.Log(loadEndingName);

        _endingscript = Resources.Load<EndingScriptableObject>($"EndingScriptableObject/{loadEndingName}");

        if (_endingscript == null)
        {
            Utils.LogRed("������ũ���ͺ� ������Ʈ�� �ε����� ���Ͽ��� Ȯ�� �ʿ�");
            return;
        }

        creditSentences = _endingscript.endingDescription.Split(',');

        _nextCharTime = Time.time; // ���� �ð� ����

        if (_endingscript.endingImage == null)
        {
            _endingImage.color = Color.black;
            _endingText.color = Color.white;
        }

        int random = Random.Range(0, _endingscript.endingImage.Length);
        _endingImage.sprite = _endingscript.endingImage[random];


        DisplayNextCharacter();
    }

    void Update()
    {
        // ���� ���� ����� �ð��� �ƴ��� Ȯ��
        if (Time.time >= _nextCharTime)
        {
            // ���� ������ ��� ���ڸ� ����ߴ��� Ȯ��
            if (_currentCharIndex < creditSentences[_currentSentenceIndex].Length)
            {
                // ���� ���� ���
                _endingText.text += creditSentences[_currentSentenceIndex][_currentCharIndex];
                _currentCharIndex++;

                // ���� ���� ��±����� �ð� ����
                _nextCharTime = Time.time + delayBetweenChars;
            }
            else
            {
                _isEndOfSentence = true; // ������ �������� ǥ��
            }
        }

        // ����Ű�� �Է��ϰų� 1�ʰ� ������ �� ���� ���� ���
        if (_isEndOfSentence || Time.time >= _nextCharTime + 1f)
        {
            _isEndOfSentence = false; // ���� ������ ����ϹǷ� �ٽ� false�� ����
            _currentSentenceIndex++; // ���� ���� �ε��� ����

            // ��� ������ ����ߴ��� Ȯ��
            if (_currentSentenceIndex < creditSentences.Length)
            {
                // �ٹٲ� �� ���� ���� ����� ���� �غ�
                _endingText.text += "\n";
                _currentCharIndex = 0;
                _nextCharTime = Time.time + delayBetweenChars;
            }
            else
            {
                // ���� ũ���� ����
                enabled = false;
            }
        }
    }

    // ���� ���ڸ� ����ϴ� �Լ�
    void DisplayNextCharacter()
    {
        // ���� �ð����� ���� ���� ��±����� �ð� ����
        _nextCharTime = Time.time + delayBetweenChars;
    }

    // ���� ��� ������ �ε� �Լ�.
    void LoadGameResultDataFromFile()
    {
        gameResultData = JsonUtility.FromJson<GameResultData>(File.ReadAllText(resultFilePath));
    }
}