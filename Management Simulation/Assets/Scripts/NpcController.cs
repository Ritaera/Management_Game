using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    private float _speed = 5f;  // 장애물이 움직일 속도
    private float _moveRange = 10f;

    private Vector2 moveDirection;  // 장애물이 움직일 방향

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
        // 장애물을 이동시킴
        transform.Translate(moveDirection * _speed * Time.deltaTime);

        // 장애물이 움직일 범위를 벗어나면, 반대 방향으로 움직이도록 방향을 바꿈
        if (transform.position.x <= -_moveRange || transform.position.x >= _moveRange)
        {
            
            moveDirection *= -1f;
        }
    }


}
