using System;
using System.Collections;
using UnityEngine;

// Jang => https://www.notion.so/01_12-PlayerScript-d94013ad7f754c48b851c51bfa1cc5dd?pvs=4#a971918a79084f7a94582e7327e8566e 
// ����Ƽ 2D ���� �ڵ带 Update, fixedupdate �� �ִ��� ������� �ʰ� ����

// �ش� �Ӽ��� �� ��ũ��Ʈ�� ������ �� �ۼ��� ������ ������Ʈ�� �䱸��.
// �� �Ӽ��� ������ ������ �츮�� �ۼ��� ������ ������Ʈ�� �ν����Ϳ��� ���� �� �� ����.
// �� ��ũ��Ʈ ����� �ν����Ϳ� �ش� ������Ʈ�� ���� ��� �ڵ����� ����
namespace Jang.Player
{


    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController2 : MonoBehaviour
    {
        #region Movement
        Rigidbody2D rbody;
        float axisH = 0f;
        private float _speed = 3.0f;
        private Transform _playerMaxPosition;
        private Transform _playerMinPosition;

        #endregion
        #region Jump
        private float _jump = 5.0f;
        private bool _jumpstate; // Jang => ���� ����
        private LayerMask _groundLayer; // ���� ���̾�
        private bool _onGround = false; // ������ �ִ��� ����
        #endregion

        #region raycast
        Vector2 worldPoint;
        RaycastHit2D hit;
#endregion

        private void Awake()
        {
            rbody = this.GetComponent<Rigidbody2D>();
            _groundLayer = (1 << GameObject.Find("Map").layer);
            _playerMaxPosition = GameObject.Find("PlayerMaxPosition").GetComponent<Transform>();
            _playerMinPosition = GameObject.Find("PlayerMinPosition").GetComponent<Transform>();
        }

        void Update()
        {
            // Jang => Horizontal�� ��� ����Ű�� �Է��ϸ� -1, 0, 1�� ���� ��Ÿ����
            axisH = Input.GetAxisRaw("Horizontal");

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
                Debug.Log("Move");
                StartCoroutine(Jump());
            }

            if (Input.GetMouseButtonDown(0))
            {
                worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                hit = Physics2D.Raycast(worldPoint, Vector2.zero);

                if (hit.collider != null)
                {
                    Debug.Log(hit.collider.name);
                    //Jang => todo => hit.collider.name�� ""���� ���� ��ȭ  UI ȣ�� 
                }
            }

            //if (Input.GetKeyDown(KeyCode.F))
            //{
            //    playerPoint = Camera.main.ScreenToWorldPoint(transform.position);

            //    RaycastHit2D hit2 = Physics2D.Raycast(transform.position, Vector2.zero);

            //    if (hit2.collider != null)
            //    {
            //        Debug.Log(hit2.collider.name);
            //    }
            //}


        }

        IEnumerator Move()
        {
            // Jang => axisH ���� ���� ĳ������ localScale�� ���� �¿� ����
            if (axisH > 0.0f)
            {
                transform.localScale = new Vector2(1, 1);
            }
            else if (axisH < 0.0f)
            {
                transform.localScale = new Vector2(-1, 1);
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
            _onGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 1.5f), _groundLayer);


            Debug.Log(_onGround);

            // Jang => onGround�� True ���¸� ����
            if (_onGround)
            {
                Debug.Log("CoroutineJump");
                Vector2 jumpdir = new Vector2(0, _jump);
                rbody.AddForce(jumpdir, ForceMode2D.Impulse);
            }
            yield return null;

        }

    }

}