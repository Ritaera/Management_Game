using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SceneManagers;

// Jang => https://www.notion.so/01_12-PlayerScript-d94013ad7f754c48b851c51bfa1cc5dd?pvs=4#a971918a79084f7a94582e7327e8566e 
// 유니티 2D 수업 코드를 Update, fixedupdate 를 최대한 사용하지 않고 정리

// 해당 속성은 이 스크립트를 연결할 때 작성한 형태의 컴포넌트를 요구함.
// 이 속성을 가지고 있으면 우리가 작성한 형태의 컴포넌트를 인스펙터에서 제거 할 수 없다.
// 이 스크립트 연결시 인스펙터에 해당 컴포넌트가 없을 경우 자동으로 연결
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController2 : MonoBehaviour
{
    public enum EHitName
    {
        SamSmith, Maria, Hani, Hyeji, Banksy, Heize, None
    }

    UIUpgrade _uIUpgrade;


    #region Movement
    Rigidbody2D rbody;
    float axisH = 0f;
    private float _speed = 5.0f;
    private Transform _playerMaxPosition;
    private Transform _playerMinPosition;

    #endregion
    #region Jump
    private float _jump = 6.0f;
    private bool _jumpstate; // Jang => 점프 여부
    private LayerMask _groundLayer; // 착지 레이어
    private bool _onGround = false; // 땅위에 있는지 여부
    #endregion

    #region raycast
    Vector2 worldPoint;
    RaycastHit2D hit;
    RaycastHit2D hit2;
    #endregion

    //private string _hitName = "";
    private float xScaleValue = 0f;

    public string HitName { get; private set; }

    public string HiddenHitName { get; private set; }

    private Dictionary<string, EHitName> hitNamePair = new Dictionary<string, EHitName>();

    //public string HitName
    //{
    //    get
    //    {
    //        return _hitName;
    //    }

    //    private set
    //    {

    //    }
    //}

    private void Awake()
    {
        if (_uIUpgrade == null)
        {
            _uIUpgrade = FindFirstObjectByType<UIUpgrade>();
        }

        rbody = this.GetComponent<Rigidbody2D>();
        _groundLayer = (1 << GameObject.Find("Map").layer);
        _playerMaxPosition = GameObject.Find("PlayerMaxPosition").GetComponent<Transform>();
        _playerMinPosition = GameObject.Find("PlayerMinPosition").GetComponent<Transform>();

        // 시작할 때 x 스케일 값을 저장 (크기 저장을 위함).
        xScaleValue = transform.localScale.x;

        // 키 이름 - 열거형 해시테이블 초기화.
        hitNamePair = new Dictionary<string, EHitName>();
        hitNamePair.TryAdd("Sam Smith", EHitName.SamSmith);
        hitNamePair.TryAdd("Maria", EHitName.Maria);
        hitNamePair.TryAdd("Hani", EHitName.Hani);
        hitNamePair.TryAdd("Hyeji", EHitName.Hyeji);
        hitNamePair.TryAdd("Banksy", EHitName.Banksy);
        hitNamePair.TryAdd("Heize", EHitName.Heize);
    }

    void Update()
    {
        // Jang => Horizontal의 경우 방향키를 입력하면 -1, 0, 1로 값을 나타내줌
        axisH = Input.GetAxis("Horizontal");

        // Jang => Asset/ProjectSettings 의 InputManager의 Jump 키워드의 경우 Space 로 설정되어있음
        _jumpstate = Input.GetButtonDown("Jump");

        // Jang => axisH가 0이 아닌 경우는 방향키의 입력이 들어왔을 때
        if (axisH != 0)
        {
     
            // Jang => Move Coroutine 호출
            StartCoroutine(Move());
        }
        // Jang => 방향키를 입력하지 않고 점프키만 입력했을때
        else if (axisH == 0 && _jumpstate != false)
        {
            // Jang => Jump Coroutine 호출
            Utils.Log("Move");
            StartCoroutine(Jump());
        }

        if (Input.GetMouseButtonDown(0))
        {
            worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null)
            {
                HiddenHitName = hit.collider.name;
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            // Todo: Raycast 방식 변경 고려.
            hit2 = Physics2D.Raycast(transform.position, Vector3.back, 1f, 1 << LayerMask.NameToLayer("npc"));

            if (hit2.collider != null)
            {
                //_hitName = hit2.collider.name;
                HitName = hit2.collider.name;
                Utils.Log(HitName);
                if (HitName != "Heize")
                {
                    _uIUpgrade.Upgrade();
                }
                else
                    SceneManagers.LoadScenes(MoveScene.InGame);
                

            }
        }
    }

    IEnumerator Move()
    {
        // Jang => axisH 값에 따라서 캐릭터의 localScale을 통해 좌우 반전
        if (axisH >= 0.0f)
        {
            transform.localScale = new Vector2(xScaleValue, transform.localScale.y);
        }
        else if (axisH < 0.0f)
        {
            transform.localScale = new Vector2(-xScaleValue, transform.localScale.y);
        }


        if (transform.position.x > _playerMaxPosition.position.x)
        {
            transform.position = _playerMaxPosition.position;
        }
        else if (transform.position.x < _playerMinPosition.position.x)
        {
            transform.position = _playerMinPosition.position;
        }


        // Jang => 위에서 if문을 통해 좌우반전을 한뒤 이동 로직 실행
        rbody.velocity = new Vector2(axisH * _speed, rbody.velocity.y);

        // Jang => 방향키를 눌러 Move Coroutine 실행중 점프키를 눌렀을때 Jump Coroutine 호출
        if (_jumpstate)
        {
            yield return StartCoroutine(Jump());
        }

        yield return null;
    }

    IEnumerator Jump()
    {
        // Jang => 현재 점프를 땅에 붙어있는 상태에서만 가능하도록 만들기 위하여 
        // Raycast를 사용하여 GroundLayer를 감지한 뒤 true / false 반환
        _onGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.5f), _groundLayer);


        Utils.Log(_onGround);

        // Jang => onGround가 True 상태면 실행
        if (_onGround)
        {
            Utils.Log("CoroutineJump");
            Vector2 jumpdir = new Vector2(0, _jump);
            rbody.AddForce(jumpdir, ForceMode2D.Impulse);
        }

        yield return null;

    }

    public void PlayerReset()
    {
        transform.position = new Vector2(0, 0);
    }

    public EHitName GetHitName()
    {
        if (hitNamePair.TryGetValue(HitName, out EHitName hitName))
        {
            return hitName;
        }

        return EHitName.None;
    }
}