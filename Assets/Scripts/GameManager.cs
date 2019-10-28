﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    public GameObject tilePrefab;
    public GameObject losePanel;
    public Tile[,] grid = new Tile[4, 4];

    private Vector2 currCell;
    private Vector2 nextCell;
    private int currX; private int currY;
    private int nextX; private int nextY;

    public bool ReadyToMove{get; set;}
    private bool waitForSpawn;
    private bool playing;

    private void Awake()
    {
        Blackboard.Instance.GameManager = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        playing = true;
        waitForSpawn = false;
        SpawnTile();
        SpawnTile();
        ReadyToMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(ReadyToMove && waitForSpawn)
        {
            waitForSpawn = false;
            SpawnTile();
        }
        InputManager();
    }

    void InputManager()
    {
        if(ReadyToMove && playing){
            #if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
                if (Input.GetKeyDown(KeyCode.UpArrow))
                MoveTiles(0);
                if (Input.GetKeyDown(KeyCode.RightArrow))
                MoveTiles(1);
                if (Input.GetKeyDown(KeyCode.DownArrow))
                MoveTiles(2);
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                MoveTiles(3);
            #elif UNITY_ANDROID || UNITY_IOS
                //Read swipes    
            #endif
        }
        if (Input.GetKeyDown(KeyCode.Space)) //For Debugging Purposes Only
            SpawnTile();
    }

    void MoveTiles(int dir)
    {
        Vector2 moveDir = Vector2.zero;

        int[] xCell = { 0, 1, 2, 3 };
        int[] yCell = { 0, 1, 2, 3 };

        //set direction for the tile to move
        switch (dir)
        {
            case 0:
                moveDir = -Vector2.up; //Minus because array has inverted y-axis than vector
                break;
            case 1:
                moveDir = Vector2.right;
                System.Array.Reverse(xCell);
                break;
            case 2:
                moveDir = -Vector2.down; //Minus because array has inverted y-axis than vector
                System.Array.Reverse(yCell);
                break;
            case 3:
                moveDir = Vector2.left;
                break;
        }

        //move each cell in y axis then x axis
        foreach(int x in xCell)
        {
            foreach(int y in yCell)
            {
                if(grid[x,y] != null)
                {
                    grid[x, y].combineable = true;

                    //currCell is the reference to the cell that we want to move the new tile to
                    //nextCell is the reference to the cell after the current cell, 
                    //if it is an occupied cell that is combineable, go combine
                    //else move tile to currcell

                    currCell = nextCell = new Vector2(x, y);
                    
                    do {    // keep moving until hits border or another tile
                        currCell = nextCell;
                        nextCell += moveDir;
                    } while (InsideGrid(nextCell) && grid[(int)nextCell.x,(int)nextCell.y] == null);

                    //if (InsideGrid(nextCell) && grid[nextX, nextY] == null) currCell = nextCell;

                    currX = (int)currCell.x; currY = (int)currCell.y;
                    nextX = (int)nextCell.x; nextY = (int)nextCell.y;
                        
                    //if next cell is another tile that is combineable, go combine
                    if (InsideGrid(nextCell) && grid[nextX, nextY] != null &&
                        TestCombinable(grid[x, y], grid[nextX, nextY]))
                    {
                        //Debug.Log("combine and move");
                        grid[x, y].SetNewDestination(x, y, nextX, nextY, true, grid[nextX, nextY]);
                    }
                    else
                    {
                        grid[x, y].SetNewDestination(x,y,currX,currY);
                        //Debug.Log(currX + "," + currY);
                    }
                }
            }
        }

        //if full, check if there is next possible move
        if (CheckGridFullness())
        {
            if (FindNextAvailableMove())
            {
                waitForSpawn = true;
            }
            else
            {
                playing = false;
                losePanel.SetActive(true);
            }
        }
        else
        {
            waitForSpawn = true;
        }
    }

    private void SpawnTile()
    {
        if (CheckGridFullness()) { Debug.Log("no more slot"); return; }

        int randX;
        int randY;
        do{
            randX = Random.Range(0, 4);
            randY = Random.Range(0, 4);
        } while (grid[randX, randY] != null);

        int startStage;
        if (Random.value < 0.90f) startStage = 0;
        else startStage = 1;
        int gender;
        if (Random.value < 0.50f) gender = 0;
        else gender = 1;

        Tile newTile = Instantiate(tilePrefab, GridController.FindTilePos(randX, randY), Quaternion.identity).GetComponent<Tile>();
        newTile.stage = startStage;
        newTile.gender = (Tile.Gender)gender;
        Blackboard.Instance.TileSpriteHandler.SetNewSprite(newTile.stage, newTile.gender, newTile.spr);
        grid[randX, randY] = newTile;
    }

    private bool FindNextAvailableMove()
    {
        Vector2 direction = Vector2.zero;
        Vector2 currTile = Vector2.zero;
        for(int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                currTile.x = x; currTile.y = y;
                for (int dir = 0; dir < 4; dir++)
                {
                    switch (dir) {
                        case 0:
                            direction = -Vector2.up; //Minus because array has inverted y-axis than vector
                            break;
                        case 1:
                            direction = Vector2.right;
                            break;
                        case 2:
                            direction = -Vector2.down; //Minus because array has inverted y-axis than vector
                            break;
                        case 3:
                            direction = Vector2.left;
                            break;
                    }
                    if (InsideGrid(currTile + direction))
                    {
                        if (TestCombinable(grid[x, y], grid[x + (int)direction.x, y + (int)direction.y]))
                        {
                            Debug.Log("match found");
                            return true;
                        }
                    }
                }
            }
        }
        Debug.Log("no more move");
        return false; //no match found. no available move
    }

    private bool CheckGridFullness()
    {
        //bool isFull = true;
        foreach(Tile tile in grid)
        {
            if (tile == null)
            {
                return false;
            }
        }
        return true;
    }

    private bool InsideGrid(Vector2 position)
    {
        if ((position.x >= 0 && position.x <= 3) && (position.y >= 0 && position.y <= 3)) return true;
        return false;
    }

    private bool TestCombinable(Tile tile1, Tile tile2)
    {
        //if (tile1.combineable && tile2.combineable && tile1.stage == tile2.stage)
        //{
        //    return tile1.stage == 4 ? (tile1.gender != tile2.gender ? true : false) : true;
        //}
        //return false;
        return (tile1.combineable && tile2.combineable && tile1.stage == tile2.stage) ? 
            (tile1.stage == 4 ? (tile1.gender != tile2.gender ? true : false) : true) : false;
    }
}