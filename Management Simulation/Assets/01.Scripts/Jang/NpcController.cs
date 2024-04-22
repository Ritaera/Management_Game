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

        // ��ֹ��� �̵���Ŵ
        transform.Translate(moveDirection * _speed * Time.deltaTime);

        if (moveDirection.x > 0)
        {
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
}
