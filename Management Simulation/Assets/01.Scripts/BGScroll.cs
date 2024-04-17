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

        // ��� �̹������� ī�޶� ���̱�
        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].parent = Camera.main.transform;
        }

        // ��� �̹������� ���������� ��ġ
        float totalWidth = imageWidth * (backgrounds.Length - 1); // ������ �̹����� ù ��° �̹����� ��ġ�Ƿ� �ϳ��� ����
        float startX = -(totalWidth / 2);
        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].localPosition = new Vector3(startX + i * imageWidth, backgrounds[i].localPosition.y, backgrounds[i].localPosition.z);
        }
    }

    void Update()
    {
        // ��� �̹������� �������� �̵�
        foreach (Transform background in backgrounds)
        {
            background.localPosition += new Vector3(-speed, 0, 0) * Time.deltaTime;

            // �̹����� ���� ��踦 ������� Ȯ��
            if (background.localPosition.x + (imageWidth / 2) < -cameraWidth)
            {
                // ������ ������ �̵�
                float rightMostX = 0;
                foreach (Transform bg in backgrounds)
                {
                    if (bg.localPosition.x > rightMostX)
                    {
                        rightMostX = bg.localPosition.x;
                    }
                }
                background.localPosition = new Vector3(rightMostX + imageWidth, background.localPosition.y, background.localPosition.z);
            }
        }
    }
}
