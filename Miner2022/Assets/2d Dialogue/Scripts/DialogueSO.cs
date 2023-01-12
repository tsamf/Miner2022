using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Dialogue", fileName ="New Dialogue")]
public class DialogueSO:ScriptableObject 
{
    [SerializeField] string NPCName;
    [TextArea(3,10)]
    [SerializeField]public string[] sentences;

    public string getNPCName ()
    {
        return NPCName;
    }

    public string[] getSentences()
    {
        return sentences;
    }
}
