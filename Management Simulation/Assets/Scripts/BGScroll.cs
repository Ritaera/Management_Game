using UnityEngine;

public class BGScroll : MonoBehaviour
{
    public float speed;
    public Transform[] backgrounds; // 배경 배열

    private float imageWidth; // 배경 이미지의 너비
    private float cameraWidth;

    void Start()
    {
        // 첫 번째 배경의 SpriteRenderer 컴포넌트를 사용하여 너비 계산
        imageWidth = backgrounds[0].GetComponent<SpriteRenderer>().bounds.size.x;

        // 카메라의 너비 계산
        cameraWidth = Camera.main.orthographicSize * Camera.main.aspect;

        // 배경 이미지들을 연속적으로 배치
        float totalWidth = imageWidth * backgrounds.Length;
        float startX = -(totalWidth / 2) + (imageWidth / 2);
        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].position = new Vector3(startX + i * imageWidth, backgrounds[i].position.y, backgrounds[i].position.z);
        }
    }

    void Update()
    {
        // 모든 배경을 왼쪽으로 이동
        foreach (Transform background in backgrounds)
        {
            background.position += new Vector3(-speed, 0, 0) * Time.deltaTime;

            // 이미지가 카메라 뷰 왼쪽 경계를 완전히 벗어날 경우
            if (background.position.x < Camera.main.transform.position.x - cameraWidth - (imageWidth / 2))
            {
                // 가장 오른쪽에 위치한 배경 이미지를 왼쪽으로 이동
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