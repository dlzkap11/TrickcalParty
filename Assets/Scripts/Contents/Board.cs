using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.SceneManagement;
using UnityEditor.SceneTemplate;
using UnityEngine;
using UnityEngine.UIElements;
using static Define;

public class Board : MonoBehaviour
{
    
    public List<BoardPosition> _boardTiles = new List<BoardPosition>();
    

    [System.Serializable]
    public class BoardPosition
    {
        public Define.Tiles TilesType { get; protected set; } = Define.Tiles.Unknown;
        public GameObject currentTile;
        public Vector3 tilepos;
        public bool isCross = false;
        public GameObject nextTile;

        public BoardPosition(GameObject c_Tile)
        {
            switch (c_Tile.name)
            {
                case "StartTile":
                    TilesType = Define.Tiles.StratTile;
                    break;
                case "GreenTile":
                    TilesType = Define.Tiles.GreenTile;
                    break;
                case "BlueTile":
                    TilesType = Define.Tiles.BlueTile;
                    break;
                case "RedTile":
                    TilesType = Define.Tiles.RedTile;
                    break;
                default:
                    TilesType = Define.Tiles.GreenTile;
                    break;
            }
            currentTile = c_Tile;
            //nextTile = n_Tile;
            tilepos = c_Tile.transform.position;
            if (c_Tile.name == "Cross")
            {

                isCross = true;
            }              
        }
    }
    void Start()
    {   
        Init();
    }

    void Update()
    {       
        
    }

    public void Init()
    {
        int childs = transform.childCount;
        
        //보드에 타일적용(자식 순서대로)
        for (int i = 0; i < childs; i++)
        {
            //_boardTiles[i] = transform.GetChild(i).gameObject;

            _boardTiles.Add(new BoardPosition(transform.GetChild(i).gameObject));
            
            
        }

    }
    
}
