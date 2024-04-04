using System;
using System.Collections.Generic;
using UnityEngine;

public class UIUpgrade : MonoBehaviour
{
    CharacterInfo characterInfo = new CharacterInfo();

    public void Upgrade()
    {
        if (characterInfo.npcName == "sam smith")
        {
            //Todo: 대장간 작업 처리.
        }
        else if (characterInfo.npcName == "maria")
        {
            //Todo: 성당처리
        }
        else if(characterInfo.npcName == "수상한 미모의 여종업원")
        {
            //Todo: 모럼가길드
        }
        else if (characterInfo.npcName == "수상한 미모의 하녀")
        {
            //Todo: 성
        }
        else
        {
            //Todo: 은행
        }
    }
}
