using UnityEngine;

public class BGScroll : MonoBehaviour
{
    public float speed;
    public Transform[] backgrounds; // ��� �迭

    private float imageWidth; // ��� �̹����� �ʺ�
    private float cameraWidth;

    void Start()
    {
        // ù ��° ����� SpriteRenderer ������Ʈ�� ����Ͽ� �ʺ� ���
        imageWidth = backgrounds[0].GetComponent<SpriteRenderer>().bounds.size.x;

        // ī�޶��� �ʺ� ���
        cameraWidth = Camera.main.orthographicSize * Camera.main.aspect;

        // ��� �̹������� ���������� ��ġ
        float totalWidth = imageWidth * backgrounds.Length;
        float startX = -(totalWidth / 2) + (imageWidth / 2);
        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].position = new Vector3(startX + i * imageWidth, backgrounds[i].position.y, backgrounds[i].position.z);
        }
    }

    void Update()
    {
        // ��� ����� �������� �̵�
        foreach (Transform background in backgrounds)
        {
            background.position += new Vector3(-speed, 0, 0) * Time.deltaTime;

            // �̹����� ī�޶� �� ���� ��踦 ������ ��� ���
            if (background.position.x < Camera.main.transform.position.x - cameraWidth - (imageWidth / 2))
            {
                // ���� �����ʿ� ��ġ�� ��� �̹����� �������� �̵�
                float rightMostX = 0;
                foreach (Transform bg in backgrounds)
                {
                    if (bg.position.x > rightMostX)
                    {
                        rightMostX = bg.position.x;
                    }
                }
                background.position = new Vector3(rightMostX + imageWidth, background.position.y, background.position.z);
            }
        }
    }
}