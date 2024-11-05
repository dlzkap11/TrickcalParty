using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Board : MonoBehaviour
{
    int _boards = 10;
    GameObject go;

    [SerializeField]
    GameObject[] _tiles;

    

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
        _tiles = new GameObject[_boards];
        _tiles = GameObject.FindGameObjectsWithTag("Tile");
        
    }
}
