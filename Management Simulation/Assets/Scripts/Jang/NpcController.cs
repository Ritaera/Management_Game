using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    private float _speed = 3.5f;  
    private float _moveRange = 20f;

    private Vector2 moveDirection;

    private float xScaleValue = 0f;

    void Start()
    {
        moveDirection = new Vector2(1f, 0f);
        xScaleValue = transform.localScale.x;
    }

    void Update()
    {

        // ��ֹ��� �̵���Ŵ
        transform.Translate(moveDirection * _speed * Time.deltaTime);

        if (moveDirection.x > 0)
        {
            transform.localScale = new Vector2(xScaleValue, transform.localScale.y);
        }
        else
        {
            transform.localScale = new Vector2(-xScaleValue, transform.localScale.y);
        }

        // ��ֹ��� ������ ������ �����, �ݴ� �������� �����̵��� ������ �ٲ�
        if (transform.position.x <= -_moveRange || transform.position.x >= _moveRange)
        {
            transform.localScale = new Vector2(-xScaleValue, transform.localScale.y);
            moveDirection *= -1f;
        }
    }


}
