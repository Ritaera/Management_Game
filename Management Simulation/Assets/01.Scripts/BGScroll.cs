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

        // 배경 이미지들을 카메라에 붙이기
        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].parent = Camera.main.transform;
        }

        // 배경 이미지들을 연속적으로 배치
        float totalWidth = imageWidth * (backgrounds.Length - 1); // 마지막 이미지는 첫 번째 이미지와 겹치므로 하나를 제외
        float startX = -(totalWidth / 2);
        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].localPosition = new Vector3(startX + i * imageWidth, backgrounds[i].localPosition.y, backgrounds[i].localPosition.z);
        }
    }

    void Update()
    {
        // 배경 이미지들을 왼쪽으로 이동
        foreach (Transform background in backgrounds)
        {
            background.localPosition += new Vector3(-speed, 0, 0) * Time.deltaTime;

            // 이미지가 왼쪽 경계를 벗어났는지 확인
            if (background.localPosition.x + (imageWidth / 2) < -cameraWidth)
            {
                // 오른쪽 끝으로 이동
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
