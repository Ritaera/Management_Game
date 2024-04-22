using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "EndingData", menuName = "Create Ending Data")]

public class EndingScriptableObject : ScriptableObject
{
    public Sprite[] endingImage;
    [TextArea] public string endingDescription;

}
