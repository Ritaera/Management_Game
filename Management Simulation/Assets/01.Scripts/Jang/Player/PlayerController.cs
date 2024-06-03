using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// 해당 속성은 이 스크립트를 연결할 때 작성한 형태의 컴포넌트를 요구함.
// 이 속성을 가지고 있으면 우리가 작성한 형태의 컴포넌트를 인스펙터에서 제거 할 수 없다.
// 이 스크립트 연결시 인스펙터에 해당 컴포넌트가 없을 경우 자동으로 연결
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    #region Movement
    Rigidbody2D rbody;
    float axisH = 0.0f;
    public float speed = 3.0f;
    #endregion
    #region Jump
    public float jump = 9.0f; // Jump 수치
    public LayerMask groundLayer; // 착지 레이어
    bool goJump = false; // 점프 진행 여부
    bool onGround = false; // 땅위에 있는지 여부
    #endregion




    private void Awake()
    {
        rbody = this.GetComponent<Rigidbody2D>(); // Rigidybody Component 호출 => Script에서 사용하기 위해서

    }

    void Update()
    {
        // Axis의 경우 유니티 에디터에서 제공해주는 InputManager에서 사용하는 고유 키 이며 사용자가 커스텀도 가능

        axisH = Input.GetAxisRaw("Horizontal"); // GetAxisRaw의 경우 -1. 0 , 1 같이 소숫점 부분을 제외
                                                // GetAxis의 경우 소수점까지 계산하여 일반적으로 Raw 채용

        if (axisH > 0.0f)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if (axisH < 0.0f)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        // 방향 잡아서 localsescale 설정

        // JUMP 기능
        if (Input.GetButtonDown("Jump"))
        {
            Jump(); // 플래그
            Debug.Log(onGround);
        }
    }

    private void Jump() => goJump = !goJump; // Lamda Expression



    private void FixedUpdate()
    {

        // 착지에 대한 판정
        onGround = Physics2D.Linecast(transform.position,
            transform.position - (transform.up * 1f), groundLayer);
        // 플레이어의 위치에서 도착 위치까지 측정해 그 위치가 
        // "Ground" Layer인지 체크하는 작업 책.100p

        // Movement
        if (onGround || axisH != 0)
        {
            rbody.velocity = new Vector2(axisH * speed, rbody.velocity.y);
        }
        Debug.DrawRay(transform.position, transform.position - (transform.up * 1f), Color.red);

        //Jump Movement
        if (onGround && goJump)
        {
            //Jump Logic 구현 1. Jump Direction 2. 해당 방향으로 힘을 가하는 작업(순간적)
            Vector2 jumpdir = new Vector2(0, jump);
            rbody.AddForce(jumpdir, ForceMode2D.Impulse);
            Jump();
        }

    }
}

