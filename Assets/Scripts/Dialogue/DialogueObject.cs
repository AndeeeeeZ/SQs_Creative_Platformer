using UnityEngine;

// Stores a series of dialogue with corresponding facial expressions
[CreateAssetMenu(menuName = "Dialogue/Dialogue Object")]
public class DialogueObject : ScriptableObject
{
    [SerializeField] private Dialogue[] dialogues;
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
        if (IsAtEnd())
            index = Mathf.Clamp(index, 0, dialogues.Length - 1);  
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
