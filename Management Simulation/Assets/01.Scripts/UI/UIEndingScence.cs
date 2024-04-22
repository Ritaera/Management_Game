using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIEndingScene : MonoBehaviour
{
    private TMP_Text _endingText;

    private EndingScriptableObject _endingscript;

    private Image _endingImage;

    // 엔딩 크레딧에 표시할 문장들
    public string[] creditSentences;

    // 현재 출력 중인 문장의 인덱스
    private int _currentSentenceIndex = 0;

    // 현재 출력 중인 문장의 글자 인덱스
    private int _currentCharIndex = 0;

    // 다음 문장 출력을 위한 딜레이
    public float delayBetweenChars = 0.1f;

    private float _nextCharTime; // 다음 글자 출력까지의 시간

    private bool _isEndOfSentence = false; // 문장이 끝났는지 여부

    public string loadEndingName = "";

    // 게임 결과 데이터를 로드해 저장하는 변수.
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
        // 엔딩 씬 시작할 때 이전 씬에서 저장한 게임 결과 데이터를 로드.
        LoadGameResultDataFromFile();

        // 해피엔딩.
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

        // 배드엔딩
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

        // 해피엔딩
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

        //// 배드엔딩
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
            Utils.LogRed("엔딩스크립터블 오브젝트를 로드하지 못하였음 확인 필요");
            return;
        }

        creditSentences = _endingscript.endingDescription.Split(',');

        _nextCharTime = Time.time; // 시작 시간 설정

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
        // 다음 글자 출력할 시간이 됐는지 확인
        if (Time.time >= _nextCharTime)
        {
            // 현재 문장의 모든 글자를 출력했는지 확인
            if (_currentCharIndex < creditSentences[_currentSentenceIndex].Length)
            {
                // 다음 글자 출력
                _endingText.text += creditSentences[_currentSentenceIndex][_currentCharIndex];
                _currentCharIndex++;

                // 다음 글자 출력까지의 시간 설정
                _nextCharTime = Time.time + delayBetweenChars;
            }
            else
            {
                _isEndOfSentence = true; // 문장이 끝났음을 표시
            }
        }

        // 엔터키를 입력하거나 1초가 지났을 때 다음 문장 출력
        if (_isEndOfSentence || Time.time >= _nextCharTime + 1f)
        {
            _isEndOfSentence = false; // 다음 문장을 출력하므로 다시 false로 설정
            _currentSentenceIndex++; // 다음 문장 인덱스 증가

            // 모든 문장을 출력했는지 확인
            if (_currentSentenceIndex < creditSentences.Length)
            {
                // 줄바꿈 후 다음 문장 출력을 위한 준비
                _endingText.text += "\n";
                _currentCharIndex = 0;
                _nextCharTime = Time.time + delayBetweenChars;
            }
            else
            {
                // 엔딩 크레딧 종료
                enabled = false;
            }
        }
    }

    // 다음 글자를 출력하는 함수
    void DisplayNextCharacter()
    {
        // 시작 시간부터 다음 글자 출력까지의 시간 설정
        _nextCharTime = Time.time + delayBetweenChars;
    }

    // 게임 결과 데이터 로드 함수.
    void LoadGameResultDataFromFile()
    {
        gameResultData = JsonUtility.FromJson<GameResultData>(File.ReadAllText(resultFilePath));
    }
}