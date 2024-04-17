using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EndingData", menuName = "Create Ending Data")]

public class EndingScriptableObject : ScriptableObject
{
    [TextArea] public string endingDescription;

}
