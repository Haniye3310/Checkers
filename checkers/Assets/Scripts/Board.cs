﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject TilePrefab;
    public GameObject PiecePrefab;
    const int LENGTH = 8;
    Tile[,] tiles = new Tile[LENGTH, LENGTH];
    Piece beginPiece;
    Tile beginTile;
    UIManager uIManager;
    public static PieceColor turn { get; private set; }
    void Start()
    {
        uIManager = FindObjectOfType<UIManager>();
        turn = PieceColor.Cream;
        uIManager.SwitchTurn(PieceColor.Cream);
        CreateBoard();
        SetPieces();
    }
    void CreateBoard()
    {
        for (int i = 0 ; i<LENGTH ; i++)
        { 
            for(int j = 0 ; j < LENGTH ; j++)
            {
                GameObject tile = Instantiate(TilePrefab, new Vector3(i, j, 0), Quaternion.identity) as GameObject;
                tiles[i, j] = tile.GetComponent<Tile>();
                tile.name = "Tile(" + i + "," + j + ")";
                if (i % 2 == 0 && j % 2 != 0) 
                    tile.GetComponent<Tile>().SetColor(TileColor.Black);
                if(i % 2 != 0 && j % 2 == 0)
                    tile.GetComponent<Tile>().SetColor(TileColor.Black);
                tile.transform.parent = transform;
            }
        }
    }
    void SetPieces()
    {
        for (int i = 0; i < LENGTH ; i++) 
        {
            for (int j = 0; j < LENGTH ; j++) 
            {
                if( j != 3 && j != 4 )
                { 
                    if(i % 2 == 0 && j % 2 != 0 || i % 2 != 0 && j % 2 == 0) 
                    { 
                   
                        GameObject piece = Instantiate(PiecePrefab, new Vector3(i,j,0),Quaternion.identity) as GameObject;
                        tiles[i, j].SetPiece(piece.GetComponent<Piece>());
                        piece.name = "Piece(" + i + "," + j + ")";
                        if (j < 3)
                            piece.GetComponent<Piece>().SetColor(PieceColor.Brown);
                        if (j > 4)
                            piece.GetComponent<Piece>().SetColor(PieceColor.Cream);
                       
                    }
                
                }
            }
        }
    }

    public void RecognizeGoal(int i , int j) 
    {
        OffLight();
        HighLight(i, j);
    }

    bool IsPlayerPin(PieceColor pieceColor)
    {
        bool ret = true;

        for (int i = 0; i < LENGTH; i++)
            for (int j = 0; j < LENGTH; j++)
            {
                if (tiles[i,j].GetPiece() == null) continue;
                if (tiles[i,j].GetPiece().GetColor() != pieceColor) continue;

                if (HighLight(i, j, true))
                {
                    ret = false;
                    break;
                }
            }

        return ret;
    }

    bool HighLight(int i ,int j, bool justCheck = false)
    {
        int greenCounter = 0;
        beginPiece = tiles[i,j].GetPiece();
        beginTile = tiles[i, j];
        if (!tiles[i, j].GetPiece().IsKing()) 
        {
            if (tiles[i, j].GetPiece().GetColor() == PieceColor.Brown)
            {
                if (i - 1 > -1 && i - 1 < 8)
                {
                    if (j + 1 > -1 && j + 1 < 8)
                    {
                        if (tiles[i - 1, j + 1].GetPiece() == null)
                        {
                            if(!justCheck)
                                tiles[i - 1, j + 1].GetComponent<Tile>().SetColor(TileColor.Green);
                            greenCounter++;
                        }
                        else if (tiles[i - 1, j + 1].GetPiece() != null)
                        {
                            if (tiles[i - 1, j + 1].GetPiece().GetColor() == PieceColor.Cream)
                            {
                                if (i - 2 > -1 && j + 2 < 8 && tiles[i - 2, j + 2].GetPiece() == null)
                                {
                                    if (!justCheck)
                                        tiles[i - 2, j + 2].GetComponent<Tile>().SetColor(TileColor.Green);
                                    greenCounter++;
                                    if (!justCheck)
                                        tiles[i - 1, j + 1].SetColor(TileColor.Red);


                                }
                            }
                        }
                    }

                }
                if (i + 1 > -1 && i + 1 < 8)
                {
                    if (j + 1 > -1 && j + 1 < 8)
                    {
                        if (tiles[i + 1, j + 1].GetPiece() == null)
                        {
                            if (!justCheck)
                                tiles[i + 1, j + 1].GetComponent<Tile>().SetColor(TileColor.Green);
                            greenCounter++;
                        }
                        else if (tiles[i + 1, j + 1].GetPiece() != null)
                        {
                            if (tiles[i + 1, j + 1].GetPiece().GetColor() == PieceColor.Cream)
                            {
                                if (i + 2 < 8 && j + 2 < 8 && tiles[i + 2, j + 2].GetPiece() == null)
                                {
                                    if (!justCheck)
                                        tiles[i + 2, j + 2].GetComponent<Tile>().SetColor(TileColor.Green);
                                    greenCounter++;
                                    if (!justCheck)
                                        tiles[i + 1, j + 1].SetColor(TileColor.Red);


                                }
                            }
                        }
                    }
                }
            }

            if (tiles[i, j].GetPiece().GetColor() == PieceColor.Cream)
            {
                if (i - 1 > -1 && i - 1 < 8)
                {
                    if (j - 1 > -1 && j - 1 < 8)
                    {
                        if (tiles[i - 1, j - 1].GetPiece() == null)
                        {
                            if (!justCheck)
                                tiles[i - 1, j - 1].GetComponent<Tile>().SetColor(TileColor.Green);
                            greenCounter++;
                        }
                        else if (tiles[i - 1, j - 1].GetPiece() != null)
                        {
                            if (tiles[i - 1, j - 1].GetPiece().GetColor() == PieceColor.Brown)
                            {
                                if (i - 2 > -1 && j - 2 > -1 && tiles[i - 2, j - 2].GetPiece() == null)
                                {
                                    if (!justCheck)
                                        tiles[i - 2, j - 2].GetComponent<Tile>().SetColor(TileColor.Green);
                                    greenCounter++;
                                    if (!justCheck)
                                        tiles[i - 1, j - 1].SetColor(TileColor.Red);


                                }
                            }
                        }
                    }
                }
                if (i + 1 > -1 && i + 1 < 8)
                {
                    if (j - 1 > -1 && j - 1 < 8)
                    {
                        if (tiles[i + 1, j - 1].GetPiece() == null)
                        {
                            if (!justCheck)
                                tiles[i + 1, j - 1].GetComponent<Tile>().SetColor(TileColor.Green);
                            greenCounter++;
                        }
                        else if (tiles[i + 1, j - 1].GetPiece() != null)
                        {
                            if (tiles[i + 1, j - 1].GetPiece().GetColor() == PieceColor.Brown)
                            {
                                if (i + 2 < 8 && j - 2 > -1 && tiles[i + 2, j - 2].GetPiece() == null)
                                {
                                    if (!justCheck)
                                        tiles[i + 2, j - 2].GetComponent<Tile>().SetColor(TileColor.Green);
                                    greenCounter++;
                                    if (!justCheck)
                                        tiles[i + 1, j - 1].SetColor(TileColor.Red);

                                }
                            }
                        }
                    }
                }
            }
            if(greenCounter == 0)
            {
                return false;
            }else
            {
                return true;
            }
        }

        else if (tiles[i,j].GetPiece().IsKing()) 
        {
            return HighLightKing(i, j, justCheck);
        }

        return false;

    }
     void OffLight() 
    {
        for (int i = 0; i < LENGTH; i++) 
            for (int j = 0; j < LENGTH; j++) 
                if(i % 2 == 0 && j % 2 != 0 || i % 2 != 0 && j % 2 == 0)
                    tiles[i,j].GetComponent<Tile>().SetColor(TileColor.Black);
            
    }
    public void Move(int i , int j) 
    {

        int x;
        int y;
        if (i > beginTile.transform.position.x && j > beginTile.transform.position.y) 
        {
            x = i-1;
            y = j-1;
            while (x!= beginTile.transform.position.x&& y!= beginTile.transform.position.y) 
            {
                if (tiles[x, y].GetPiece() != null) 
                {
                    Destroy(tiles[x,y].GetPiece().gameObject);
                    tiles[x, y].SetPiece(null);
                }
                x = x - 1;
                y = y - 1;
            }
        }
        if (i < beginTile.transform.position.x && j > beginTile.transform.position.y) 
        {
            x = i+1;
            y = j-1;
            while (x != beginTile.transform.position.x && y != beginTile.transform.position.y)
            {
                if (tiles[x, y].GetPiece() != null)
                {
                    Destroy(tiles[x, y].GetPiece().gameObject);
                    tiles[x, y].SetPiece(null);
                }
                x = x + 1;
                y = y - 1;
            }
        }
        if (i < beginTile.transform.position.x && j < beginTile.transform.position.y) 
        {
            x = i+1;
            y = j+1;
            while (x != beginTile.transform.position.x && y != beginTile.transform.position.y)
            {
                if (tiles[x, y].GetPiece() != null)
                {
                    Destroy(tiles[x, y].GetPiece().gameObject);
                    tiles[x, y].SetPiece(null);
                }
                x = x + 1;
                y = y + 1;
            }
        }
        if (i > beginTile.transform.position.x && j < beginTile.transform.position.y) 
        {
            x = i-1;
            y = j+1;
            while (x != beginTile.transform.position.x && y != beginTile.transform.position.y)
            {
                if (tiles[x, y].GetPiece() != null)
                {
                    Destroy(tiles[x, y].GetPiece().gameObject);
                    tiles[x, y].SetPiece(null);
                }
                x = x - 1;
                y = y + 1;
            }
        }


        OffLight();
        beginTile.SetPiece(null);
        tiles[i, j].SetPiece(beginPiece);
        beginPiece.name= "Piece(" + i + "," + j + ")";
        beginPiece.GetComponent<Transform>().position = new Vector3(i,j,0);
        if (j == 7 || j == 0)
        {
            tiles[i, j].GetPiece().SetKing();
        }

        WinBrown();
        WinCream();
        SwitchTurn();
    }

    public void WinBrown() 
    {
        int counter = 0;
        for (int i = 0 ; i < LENGTH; i++)
        {
            for (int j = 0; j< LENGTH;j++) 
            {
                if(tiles[i,j].GetPiece()!=null && tiles[i, j].GetPiece().GetColor() == PieceColor.Cream)
                {
                    counter++;
                }
                
            }
        }

        if (counter == 0) 
        {
            uIManager.SetWinUI(PieceColor.Brown);
        }
            

    }
    public void WinCream()
    {
        int counter = 0;
        for (int i = 0; i < LENGTH; i++)
        {
            for (int j = 0; j < LENGTH; j++)
            {
                if (tiles[i, j].GetPiece() != null && tiles[i, j].GetPiece().GetColor() == PieceColor.Brown)
                {
                    counter++;
                }

            }
        }

        if (counter == 0) 
        {
            uIManager.SetWinUI(PieceColor.Cream);
        }
    }

    bool HighLightKing(int i , int j, bool justCheck = false) 
    {
        int greenCounter = 0;
        int x = i+1;
        int y = j+1;
        while (x < 8 && x > -1 && y < 8 && y > -1 )
        {
            if (tiles[x, y].GetPiece() == null)
            {
                if (!justCheck)
                    tiles[x, y].SetColor(TileColor.Green);
                greenCounter++;
            }
            if (tiles[x, y].GetPiece() != null) 
            {
                if (x + 1 < 8 && x + 1 > -1 && y + 1 < 8 && y + 1 > -1) 
                {
                    if (tiles[x + 1, y + 1].GetPiece() != null)
                    {
                        x = 8;
                        break;
                    }
                }

                if (tiles[x, y].GetPiece().GetColor() == tiles[i, j].GetPiece().GetColor()) 
                {
                    x = 8;
                    break;
                }
                if (tiles[x, y].GetPiece().GetColor() != tiles[i, j].GetPiece().GetColor() && x+1<8 && x + 1 > -1 && y + 1 < 8 && y + 1 > -1) 
                {
                    if (!justCheck)
                        tiles[x, y].SetColor(TileColor.Red);
                }
                
            }
            x = x + 1;
            y = y + 1;
        }
        x = i - 1;
        y = j + 1;
        while (x < 8 && x > -1 && y < 8 && y > -1)
        {
            if (tiles[x, y].GetPiece() == null)
            {
                if (!justCheck)
                    tiles[x, y].SetColor(TileColor.Green);
                greenCounter++;
            }
            if (tiles[x, y].GetPiece() != null)
            {
                if (x - 1 > -1 && y + 1 < 8 && y + 1 > -1 && x - 1 < 8) 
                {
                    if (tiles[x - 1, y + 1].GetPiece() != null )
                    {
                        x = 8;
                        break;
                    }
                }
               
                if (tiles[x, y].GetPiece().GetColor() == tiles[i, j].GetPiece().GetColor())
                {
                    x = 8;
                    break;
                }
                if (tiles[x, y].GetPiece().GetColor() != tiles[i, j].GetPiece().GetColor() && x - 1 > -1 && y + 1 < 8 && y + 1 > -1&&x - 1 < 8)
                {
                    if (!justCheck)
                        tiles[x, y].SetColor(TileColor.Red);
                }

            }
            x = x - 1;
            y = y + 1;
        }
        x = i - 1;
        y = j - 1;
        while (x < 8 && x > -1 && y < 8 && y > -1)
        {
            if (tiles[x, y].GetPiece() == null)
            {
                if (!justCheck)
                    tiles[x, y].SetColor(TileColor.Green);
                greenCounter++;
            }
            if (tiles[x, y].GetPiece() != null)
            {
                if (x - 1 > -1 && y - 1 < 8 && y - 1 > -1 && x - 1 < 8)
                {
                    if (tiles[x - 1, y - 1].GetPiece() != null)
                    {
                        x = 8;
                        break;
                    }
                }

                if (tiles[x, y].GetPiece().GetColor() == tiles[i, j].GetPiece().GetColor())
                {
                    x = 8;
                    break;
                }
                if (tiles[x, y].GetPiece().GetColor() != tiles[i, j].GetPiece().GetColor() && x - 1 > -1 && y - 1 < 8 && y - 1 > -1 && x - 1 < 8)
                {
                    if (!justCheck)
                        tiles[x, y].SetColor(TileColor.Red);
                }

            }
            x = x - 1;
            y = y - 1;
        }
        x = i + 1;
        y = j - 1;
        while (x < 8 && x > -1 && y < 8 && y > -1)
        {
            if (tiles[x, y].GetPiece() == null)
            {
                if (!justCheck)
                    tiles[x, y].SetColor(TileColor.Green);
                greenCounter++;
            }
            if (tiles[x, y].GetPiece() != null)
            {
                if (x + 1 > -1 && y - 1 < 8 && y - 1 > -1 && x + 1 < 8)
                {
                    if (tiles[x + 1, y - 1].GetPiece() != null)
                    {
                        x = 8;
                        break;
                    }
                }

                if (tiles[x, y].GetPiece().GetColor() == tiles[i, j].GetPiece().GetColor())
                {
                    x = 8;
                    break;
                }
                if (tiles[x, y].GetPiece().GetColor() != tiles[i, j].GetPiece().GetColor() && x + 1 > -1 && y - 1 < 8 && y - 1 > -1 && x + 1 < 8)
                {
                    if (!justCheck)
                        tiles[x, y].SetColor(TileColor.Red);
                }

            }
            x = x + 1;
            y = y - 1;
        }
        if(greenCounter == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
  
    private void SwitchTurn() 
    {
        if (turn== PieceColor.Cream) 
        {
            turn = PieceColor.Brown;
            uIManager.SwitchTurn(PieceColor.Brown);
            if(IsPlayerPin(PieceColor.Brown))
            {
                uIManager.SetWinUI(PieceColor.Brown, true);
            }
        }
        else if (turn == PieceColor.Brown)
        {
            turn = PieceColor.Cream;
            uIManager.SwitchTurn(PieceColor.Cream);
            if (IsPlayerPin(PieceColor.Cream))
            {
                uIManager.SetWinUI(PieceColor.Cream, true);
            }
        }
    }
}




