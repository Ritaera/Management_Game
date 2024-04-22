using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SceneManagers;

// Jang => https://www.notion.so/01_12-PlayerScript-d94013ad7f754c48b851c51bfa1cc5dd?pvs=4#a971918a79084f7a94582e7327e8566e 
// ����Ƽ 2D ���� �ڵ带 Update, fixedupdate �� �ִ��� ������� �ʰ� ����

// �ش� �Ӽ��� �� ��ũ��Ʈ�� ������ �� �ۼ��� ������ ������Ʈ�� �䱸��.
// �� �Ӽ��� ������ ������ �츮�� �ۼ��� ������ ������Ʈ�� �ν����Ϳ��� ���� �� �� ����.
// �� ��ũ��Ʈ ����� �ν����Ϳ� �ش� ������Ʈ�� ���� ��� �ڵ����� ����
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
    private bool _jumpstate; // Jang => ���� ����
    private LayerMask _groundLayer; // ���� ���̾�
    private bool _onGround = false; // ������ �ִ��� ����
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

        // ������ �� x ������ ���� ���� (ũ�� ������ ����).
        xScaleValue = transform.localScale.x;

        // Ű �̸� - ������ �ؽ����̺� �ʱ�ȭ.
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
        // Jang => Horizontal�� ��� ����Ű�� �Է��ϸ� -1, 0, 1�� ���� ��Ÿ����
        axisH = Input.GetAxis("Horizontal");

        // Jang => Asset/ProjectSettings �� InputManager�� Jump Ű������ ��� Space �� �����Ǿ�����
        _jumpstate = Input.GetButtonDown("Jump");

        // Jang => axisH�� 0�� �ƴ� ���� ����Ű�� �Է��� ������ ��
        if (axisH != 0)
        {
     
            // Jang => Move Coroutine ȣ��
            StartCoroutine(Move());
        }
        // Jang => ����Ű�� �Է����� �ʰ� ����Ű�� �Է�������
        else if (axisH == 0 && _jumpstate != false)
        {
            // Jang => Jump Coroutine ȣ��
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
            // Todo: Raycast ��� ���� ���.
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
        // Jang => axisH ���� ���� ĳ������ localScale�� ���� �¿� ����
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


        // Jang => ������ if���� ���� �¿������ �ѵ� �̵� ���� ����
        rbody.velocity = new Vector2(axisH * _speed, rbody.velocity.y);

        // Jang => ����Ű�� ���� Move Coroutine ������ ����Ű�� �������� Jump Coroutine ȣ��
        if (_jumpstate)
        {
            yield return StartCoroutine(Jump());
        }

        yield return null;
    }

    IEnumerator Jump()
    {
        // Jang => ���� ������ ���� �پ��ִ� ���¿����� �����ϵ��� ����� ���Ͽ� 
        // Raycast�� ����Ͽ� GroundLayer�� ������ �� true / false ��ȯ
        _onGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.5f), _groundLayer);


        Utils.Log(_onGround);

        // Jang => onGround�� True ���¸� ����
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