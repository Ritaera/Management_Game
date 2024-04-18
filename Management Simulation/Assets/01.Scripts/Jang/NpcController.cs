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

    private float xPosition = 0f;


    void Start()
    {
        moveDirection = new Vector2(_moveVector, 0f);
        xScaleValue = transform.localScale.x;
        xPosition = transform.localPosition.x;
    }

    void Update()
    {

        // 장애물을 이동시킴
        transform.Translate(moveDirection * _speed * Time.deltaTime);

        if (moveDirection.x > 0)
        {
            transform.localScale = new Vector2(-xScaleValue, transform.localScale.y);
        }
        else
        {
            transform.localScale = new Vector2(+xScaleValue, transform.localScale.y);
        }

        // 장애물이 움직일 범위를 벗어나면, 반대 방향으로 움직이도록 방향을 바꿈
        if (transform.position.x <= xPosition -_moveRange || transform.position.x >= xPosition + _moveRange)
        {
            moveDirection *= -1f;
        }
    }
}
