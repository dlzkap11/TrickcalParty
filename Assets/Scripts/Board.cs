using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Board : MonoBehaviour
{
    //int _boards = 10;
    //GameObject go = null;
    [SerializeField]
    public GameObject[] _boardTile;

    

    // 1 -> 2 -> 3 -> 4 -> 5 -> 6 -> 7 -> 8 -> 9 -> 10 -> 1 -> 2...

    void Start()
    {   
        Init();
    }

    
    void Update()
    {       
        
    }

    public void Init()
    {
        //_tiles = new GameObject[_boards];
        //_tiles = GameObject.FindGameObjectsWithTag("Tile");
        //if (gameObject.GetComponentInChildren<Board>() == null)
        //_board.transform.GetChild(0);

        int childs = transform.childCount;
        _boardTile = new GameObject[childs];

        //보드에 타일적용(자식 순서대로)
        for(int i = 0; i < childs; i++)
            _boardTile[i] = transform.GetChild(i).gameObject;

        //Debug.Log(_boardTile[0].transform.position);
        //Debug.Log(_boardTile[1].transform.position);
    }
}
