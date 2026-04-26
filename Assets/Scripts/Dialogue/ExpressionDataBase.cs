using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/Expression Data Base")]
public class ExpressionDataBase : ScriptableObject
{

    [System.Serializable]
    private struct ExpressionSpriteMatch
    {
        public Expression expression;
        public Sprite sprite;
    }

    [SerializeField]
    private ExpressionSpriteMatch[] sprites; 

    public Sprite GetSprite(Expression expression)
    {
        foreach (ExpressionSpriteMatch s in sprites)
        {
            if (s.expression == expression)
                return s.sprite; 
        }

        return null; 
    }
}