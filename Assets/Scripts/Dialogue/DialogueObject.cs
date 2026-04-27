using UnityEngine;

// Stores a series of dialogue with corresponding facial expressions
[CreateAssetMenu(menuName = "Dialogue/Dialogue Object")]
public class DialogueObject : ScriptableObject
{
    [SerializeField] private Dialogue[] dialogues;
    public bool TriggerEventWhenEnd = false; 
    public VoidEvent endEvent; 
    private int index; 

    public void StartDialogue()
    {
        index = 0; 
    }

    public bool IsAtEnd()
    {
        return index >= dialogues.Length; 
    }

    public void Reset()
    {
        index = 0; 
    }

    public bool ToNextLine()
    {
        index++;
        return IsAtEnd(); 
    }

    public Dialogue GetCurrentLine()
    {
        if (IsAtEnd())
        {
            Debug.LogWarning("At the end of dialogue already"); 
            return null; 
        }

        return dialogues[index]; 
    }

}
