using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// �ش� �Ӽ��� �� ��ũ��Ʈ�� ������ �� �ۼ��� ������ ������Ʈ�� �䱸��.
// �� �Ӽ��� ������ ������ �츮�� �ۼ��� ������ ������Ʈ�� �ν����Ϳ��� ���� �� �� ����.
// �� ��ũ��Ʈ ����� �ν����Ϳ� �ش� ������Ʈ�� ���� ��� �ڵ����� ����
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    #region Movement
    Rigidbody2D rbody;
    float axisH = 0.0f;
    public float speed = 3.0f;
    #endregion
    #region Jump
    public float jump = 9.0f; // Jump ��ġ
    public LayerMask groundLayer; // ���� ���̾�
    bool goJump = false; // ���� ���� ����
    bool onGround = false; // ������ �ִ��� ����
    #endregion




    private void Awake()
    {
        rbody = this.GetComponent<Rigidbody2D>(); // Rigidybody Component ȣ�� => Script���� ����ϱ� ���ؼ�

    }

    void Update()
    {
        // Axis�� ��� ����Ƽ �����Ϳ��� �������ִ� InputManager���� ����ϴ� ���� Ű �̸� ����ڰ� Ŀ���ҵ� ����

        axisH = Input.GetAxisRaw("Horizontal"); // GetAxisRaw�� ��� -1. 0 , 1 ���� �Ҽ��� �κ��� ����
                                                // GetAxis�� ��� �Ҽ������� ����Ͽ� �Ϲ������� Raw ä��

        if (axisH > 0.0f)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if (axisH < 0.0f)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        // ���� ��Ƽ� localsescale ����

        // JUMP ���
        if (Input.GetButtonDown("Jump"))
        {
            Jump(); // �÷���
            Debug.Log(onGround);
        }
    }

    private void Jump() => goJump = !goJump; // Lamda Expression



    private void FixedUpdate()
    {

        // ������ ���� ����
        onGround = Physics2D.Linecast(transform.position,
            transform.position - (transform.up * 1f), groundLayer);
        // �÷��̾��� ��ġ���� ���� ��ġ���� ������ �� ��ġ�� 
        // "Ground" Layer���� üũ�ϴ� �۾� å.100p

        // Movement
        if (onGround || axisH != 0)
        {
            rbody.velocity = new Vector2(axisH * speed, rbody.velocity.y);
        }
        Debug.DrawRay(transform.position, transform.position - (transform.up * 1f), Color.red);

        //Jump Movement
        if (onGround && goJump)
        {
            //Jump Logic ���� 1. Jump Direction 2. �ش� �������� ���� ���ϴ� �۾�(������)
            Vector2 jumpdir = new Vector2(0, jump);
            rbody.AddForce(jumpdir, ForceMode2D.Impulse);
            Jump();
        }

    }
}

