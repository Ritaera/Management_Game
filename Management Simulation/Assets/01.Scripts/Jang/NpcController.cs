<<<<<<< HEAD
=======
using System.Collections;
using System.Collections.Generic;
>>>>>>> origin/L.Gyeol
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

<<<<<<< HEAD
    private float xPosition = 0f;


=======
>>>>>>> origin/L.Gyeol
    void Start()
    {
        moveDirection = new Vector2(_moveVector, 0f);
        xScaleValue = transform.localScale.x;
<<<<<<< HEAD
        xPosition = transform.localPosition.x;
=======

        if(_moveVector < 0f)
        {
            transform.localScale = new Vector2(-xScaleValue, transform.localScale.y);
        }
>>>>>>> origin/L.Gyeol
    }

    void Update()
    {

        // ��ֹ��� �̵���Ŵ
        transform.Translate(moveDirection * _speed * Time.deltaTime);

        if (moveDirection.x > 0)
        {
<<<<<<< HEAD
            transform.localScale = new Vector2(-xScaleValue, transform.localScale.y);
        }
        else
        {
            transform.localScale = new Vector2(+xScaleValue, transform.localScale.y);
        }

        // ��ֹ��� ������ ������ �����, �ݴ� �������� �����̵��� ������ �ٲ�
        if (transform.position.x <= xPosition -_moveRange || transform.position.x >= xPosition + _moveRange)
        {
            moveDirection *= -1f;
        }
    }
=======
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


>>>>>>> origin/L.Gyeol
}
