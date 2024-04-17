using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Jang => https://www.notion.so/01_12-PlayerScript-d94013ad7f754c48b851c51bfa1cc5dd?pvs=4#a971918a79084f7a94582e7327e8566e 
// ����Ƽ 2D ���� �ڵ带 Update, fixedupdate �� �ִ��� ������� �ʰ� ����

// �ش� �Ӽ��� �� ��ũ��Ʈ�� ������ �� �ۼ��� ������ ������Ʈ�� �䱸��.
// �� �Ӽ��� ������ ������ �츮�� �ۼ��� ������ ������Ʈ�� �ν����Ϳ��� ���� �� �� ����.
// �� ��ũ��Ʈ ����� �ν����Ϳ� �ش� ������Ʈ�� ���� ��� �ڵ����� ����
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
    public bool jumpstate; // Jang => ���� ����
    public LayerMask groundLayer; // ���� ���̾�
    bool onGround = false; // ������ �ִ��� ����
    #endregion

    //RaycastHit2D hit;



    private void Awake()
    {
        rbody = this.GetComponent<Rigidbody2D>();
        groundLayer = (1 << GameObject.Find("Map").layer);
    }

    void Update()
    {
        // Jang => Horizontal�� ��� ����Ű�� �Է��ϸ� -1, 0, 1�� ���� ��Ÿ����
        axisH = Input.GetAxisRaw("Horizontal");

        // Jang => Asset/ProjectSettings �� InputManager�� Jump Ű������ ��� Space �� �����Ǿ�����
        jumpstate = Input.GetButtonDown("Jump");

        // Jang => axisH�� 0�� �ƴ� ���� ����Ű�� �Է��� ������ ��
        if (axisH != 0)
        {
            // Jang => Move Coroutine ȣ��
            StartCoroutine(Move());
        }
        // Jang => ����Ű�� �Է����� �ʰ� ����Ű�� �Է�������
        else if (axisH == 0 && jumpstate != false)
        {
            // Jang => Jump Coroutine ȣ��
            StartCoroutine(Jump());
        }

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

        // Jang => ������ if���� ���� �¿������ �ѵ� �̵� ���� ����
        rbody.velocity = new Vector2(axisH * speed, rbody.velocity.y);

        // Jang => ����Ű�� ���� Move Coroutine ������ ����Ű�� �������� Jump Coroutine ȣ��
        if (jumpstate)
        {
            yield return StartCoroutine(Jump());
        }

        yield return null;
    }

    IEnumerator Jump()
    {
        // Jang => ���� ������ ���� �پ��ִ� ���¿����� �����ϵ��� ����� ���Ͽ� 
        // Raycast�� ����Ͽ� GroundLayer�� ������ �� true / false ��ȯ
        onGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 1f), groundLayer);

        // Jang => onGround�� True ���¸� ����
        if (onGround)
        {
            Debug.Log("CoroutineJump");
            Vector2 jumpdir = new Vector2(0, jump);
            rbody.AddForce(jumpdir, ForceMode2D.Impulse);
        }
        yield return null;

    }
}

