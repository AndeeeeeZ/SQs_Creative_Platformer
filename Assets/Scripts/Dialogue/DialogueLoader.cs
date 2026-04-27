using TMPro;
using UnityEngine;

public class DialogueLoader : MonoBehaviour
{
    [Header("Dialogue")]
    [SerializeField] private ExpressionDataBase database;
    [SerializeField] private DialogueObject currentDialogue;

    [Header("UI References")]
    [SerializeField] private GameObject[] AllUI; // All objects that should be enabled/disabled 
    [SerializeField] private SpriteRenderer characterArtSprite;
    [SerializeField] private TextMeshProUGUI text;

    private void Start()
    {
        currentDialogue.Reset();
        LoadDialogue();
        SetUIActiveness(true);
    }

    public void StartNewDialogue(DialogueObject dialogue)
    {
        currentDialogue = dialogue;
        dialogue.Reset();
        SetUIActiveness(true);
        LoadDialogue();
    }

    public void LoadDialogue()
    {
        if (currentDialogue.IsAtEnd())
        {
            if (currentDialogue.TriggerEventWhenEnd)
            {
                currentDialogue.endEvent.Raise(); 
            }
            Debug.LogWarning("At the end of dialogue");
            SetUIActiveness(false);
            return;
        }
        Dialogue currentLine = currentDialogue.GetCurrentLine();
        text.text = currentLine.line;
        Sprite expression = database.GetSprite(currentLine.expression);
        characterArtSprite.sprite = expression;

        currentDialogue.ToNextLine();
    }

    private void SetUIActiveness(bool v)
    {
        foreach (GameObject g in AllUI)
        {
            g.SetActive(v);
        }
    }
}
