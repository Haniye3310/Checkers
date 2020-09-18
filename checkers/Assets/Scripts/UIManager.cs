using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text text;
    public GameObject panel;
    public Text turnText;
    public Image image;
    public Color brownColor;
    public Color creamColor;
    public void SetWinUI(PieceColor pieceColor) 
    {
        panel.SetActive(true);
        if (pieceColor ==PieceColor.Cream) 
        {
            text.text = "Cream Win :)";
        }
        if (pieceColor == PieceColor.Brown)
        {
            text.text = "Brown Win :)";
        }
    }
    public void SwitchTurn(PieceColor pieceColor) 
    {
        if (pieceColor == PieceColor.Cream) 
        {
            turnText.text = "Cream Turn !";
            image.color = creamColor;
        }
        if (pieceColor == PieceColor.Brown)
        {
            image.color = brownColor;
            turnText.text = "Brown Turn !";
        }
    }

}
