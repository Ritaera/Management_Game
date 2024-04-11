using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    private float _speed = 5f;  // ��ֹ��� ������ �ӵ�
    private float _moveRange = 10f;

    private Vector2 moveDirection;  // ��ֹ��� ������ ����

    void Start()
    {
        moveDirection = new Vector2(1f, 0f);

      
    }

    void Update()
    {
        if (transform.position.x >= -1.84f)
        {
            transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y);
        }
        else if (transform.position.x < -2.81f)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);

        }
        // ��ֹ��� �̵���Ŵ
        transform.Translate(moveDirection * _speed * Time.deltaTime);

        // ��ֹ��� ������ ������ �����, �ݴ� �������� �����̵��� ������ �ٲ�
        if (transform.position.x <= -_moveRange || transform.position.x >= _moveRange)
        {
            
            moveDirection *= -1f;
        }
    }


}
