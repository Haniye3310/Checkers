using UnityEngine;
using UnityEngine.UI;

public enum TileColor 
{
    Black,
    White,
    Green,
    Red,
}

public class Tile : MonoBehaviour
{
    Board board;
    Piece _piece = null;
    TileColor tileColor;
    int turn =0;
    void Start() 
    {
        board = FindObjectOfType<Board>();
    }
    public void SetColor(TileColor color) 
    {
        tileColor = color;
        if (color == TileColor.Black)
        {
            GetComponent<SpriteRenderer>().color = Color.black;       
        }
        if (color == TileColor.White)
        {
            GetComponent<SpriteRenderer>().color =  Color.white;
        }
        if (color == TileColor.Green)
        {
            GetComponent<SpriteRenderer>().color = Color.green;
        }
        if (color == TileColor.Red)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    public void SetPiece(Piece piece)
    {
        _piece = piece;
    }
   
    void OnMouseDown() 
    {
        if ((GetColor() == TileColor.Black && GetPiece() != null) || (GetColor() == TileColor.Red && GetPiece() != null)) 
        {
            if (Board.turn!=GetPiece().GetColor()) { return; }
           
            board.RecognizeGoal((int)transform.position.x, (int)transform.position.y);
        }


        if (GetColor() == TileColor.Green)
        {
            board.Move((int)transform.position.x, (int)transform.position.y);
        }


    }
    public Piece GetPiece() 
    {
        return _piece;
    }
    public TileColor GetColor() 
    {
        return tileColor;
    }
    void SwitchTurn() 
    {
        if (turn == 0)
            turn = 1;
        else if (turn == 1)
            turn = 0;
    }
}
