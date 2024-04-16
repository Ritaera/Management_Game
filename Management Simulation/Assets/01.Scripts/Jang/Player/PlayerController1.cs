using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Jang => https://www.notion.so/01_12-PlayerScript-d94013ad7f754c48b851c51bfa1cc5dd?pvs=4#a971918a79084f7a94582e7327e8566e 
// 유니티 2D 수업 코드를 Update, fixedupdate 를 최대한 사용하지 않고 정리

// 해당 속성은 이 스크립트를 연결할 때 작성한 형태의 컴포넌트를 요구함.
// 이 속성을 가지고 있으면 우리가 작성한 형태의 컴포넌트를 인스펙터에서 제거 할 수 없다.
// 이 스크립트 연결시 인스펙터에 해당 컴포넌트가 없을 경우 자동으로 연결
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController1 : MonoBehaviour
{
    #region Movement
    Rigidbody2D rbody;
    float axisH = 0f;
    public float speed = 3.0f;
    #endregion
    #region Jump
    public float jump = 9.0f;
    public bool jumpstate; // Jang => 점프 여부
    public LayerMask groundLayer; // 착지 레이어
    bool onGround = false; // 땅위에 있는지 여부
    #endregion

    //RaycastHit2D hit;



    private void Awake()
    {
        rbody = this.GetComponent<Rigidbody2D>();
        groundLayer = (1 << GameObject.Find("Map").layer);
    }

    void Update()
    {
        // Jang => Horizontal의 경우 방향키를 입력하면 -1, 0, 1로 값을 나타내줌
        axisH = Input.GetAxisRaw("Horizontal");

        // Jang => Asset/ProjectSettings 의 InputManager의 Jump 키워드의 경우 Space 로 설정되어있음
        jumpstate = Input.GetButtonDown("Jump");

        // Jang => axisH가 0이 아닌 경우는 방향키의 입력이 들어왔을 때
        if (axisH != 0)
        {
            // Jang => Move Coroutine 호출
            StartCoroutine(Move());
        }
        // Jang => 방향키를 입력하지 않고 점프키만 입력했을때
        else if (axisH == 0 && jumpstate != false)
        {
            // Jang => Jump Coroutine 호출
            StartCoroutine(Jump());
        }

    }

    IEnumerator Move()
    {
        // Jang => axisH 값에 따라서 캐릭터의 localScale을 통해 좌우 반전
        if (axisH > 0.0f)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if (axisH < 0.0f)
        {
            transform.localScale = new Vector2(-1, 1);
        }

        // Jang => 위에서 if문을 통해 좌우반전을 한뒤 이동 로직 실행
        rbody.velocity = new Vector2(axisH * speed, rbody.velocity.y);

        // Jang => 방향키를 눌러 Move Coroutine 실행중 점프키를 눌렀을때 Jump Coroutine 호출
        if (jumpstate)
        {
            yield return StartCoroutine(Jump());
        }

        yield return null;
    }

    IEnumerator Jump()
    {
        // Jang => 현재 점프를 땅에 붙어있는 상태에서만 가능하도록 만들기 위하여 
        // Raycast를 사용하여 GroundLayer를 감지한 뒤 true / false 반환
        onGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 1f), groundLayer);

        // Jang => onGround가 True 상태면 실행
        if (onGround)
        {
            Debug.Log("CoroutineJump");
            Vector2 jumpdir = new Vector2(0, jump);
            rbody.AddForce(jumpdir, ForceMode2D.Impulse);
        }
        yield return null;

    }
}

