using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    private float _speed = 3.5f;

    [SerializeField]
    private float _moveRange = 20f;

    private Vector2 moveDirection;

    [SerializeField]
    private float _moveVector;

    private float xScaleValue = 0f;

    void Start()
    {
        moveDirection = new Vector2(_moveVector, 0f);
        xScaleValue = transform.localScale.x;

        if(_moveVector < 0f)
        {
            transform.localScale = new Vector2(-xScaleValue, transform.localScale.y);
        }
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
