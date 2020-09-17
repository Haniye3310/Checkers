using UnityEngine;
using UnityEngine.UI;

public enum PieceColor
{
    Brown,
    Cream,
}

public class Piece : MonoBehaviour
{
    PieceColor pieceColor;
    GameObject kingChild;

    void Start() 
    {
        kingChild = transform.GetChild(0).gameObject;
        kingChild.SetActive(false);
    }
    public void SetColor(PieceColor color)
    {
        pieceColor = color;
        if (color == PieceColor.Brown)
        {
            GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.3f, 0.2f, 1);
        }
        if (color == PieceColor.Cream)
        {
            GetComponent<SpriteRenderer>().color = new Color(0.8f,0.8f,0.5f,1);
        }
    }
    public PieceColor GetColor() 
    {
        return pieceColor;
    }

    public void SetKing() 
    {
        kingChild.SetActive(true);
    }

    public bool IsKing() 
    {
        return kingChild.activeInHierarchy;
    }
}

