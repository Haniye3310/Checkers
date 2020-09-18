using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Text text;
    public GameObject panel;
    public Text turnText;
    public Image image;
    public Color brownColor;
    public Color creamColor;
    public void SetWinUI(PieceColor pieceColor, bool isPlayerPin=false) 
    {
        if (panel.activeInHierarchy) return;

        panel.SetActive(true);
        
        if (pieceColor ==PieceColor.Cream) 
        {
            if (isPlayerPin) 
            { 
                text.text = "Brown Pin! :((";
                return;
            }
            text.text = "Cream Win :)";
        }
        if (pieceColor == PieceColor.Brown)
        {
            if(isPlayerPin)
            {
                text.text = "Cream Pin! :((";
                return;
            }
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
    public void NewGame_OnClick() 
    {
        SceneManager.LoadScene(0);
    }

}
