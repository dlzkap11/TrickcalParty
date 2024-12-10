using Mono.Cecil;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TileManager
{
    public GameObject _boardPrefab;
    private List<Tile> _allTiles = new List<Tile>();

    public void Init()
    {
        _boardPrefab = Managers.Resource.Instantiate("Board"); //보드 불러오기

        FindTiles();
    }


    void FindTiles()
    {
        foreach (Tile tiles in _boardPrefab.transform.GetComponentsInChildren<Tile>())
        {
            _allTiles.Add(tiles);
        }

        #region 타일 연결

        // 일방통행 개척
        for (int i = 0; i < (_allTiles.Count - 1); i++)
        {
            //if (_allTiles[i].name == "Cross")
                //continue;
            ConnectTiles(_allTiles[i], _allTiles[i + 1]); 
        }

        //갈림길 보수 공사
        ConnectTiles(_allTiles[32], _allTiles[1]);
        ConnectTiles(_allTiles[62], _allTiles[8]);
        ConnectTiles(_allTiles[41], _allTiles[37]);
        ConnectTiles(_allTiles[40], _allTiles[46]);
        ConnectTiles(_allTiles[63], _allTiles[53]);
        ConnectTiles(_allTiles[74], _allTiles[67]);

        // 일방통행 돌아가는 길 개척
        for (int i = 0; i < (_allTiles.Count - 1); i++)
        {
            //if (_allTiles[i].name == "Cross")
            //continue;
            ConnectTiles(_allTiles[i + 1], _allTiles[i]); 
        }

        //갈림길 보수 공사
        ConnectTiles(_allTiles[1], _allTiles[32]);
        ConnectTiles(_allTiles[1], _allTiles[33]);
        ConnectTiles(_allTiles[33], _allTiles[1]);
        DisconnectTiles(_allTiles[32], _allTiles[33]);

        ConnectTiles(_allTiles[8], _allTiles[62]);
        DisconnectTiles(_allTiles[62], _allTiles[63]);

        ConnectTiles(_allTiles[37], _allTiles[41]);
        DisconnectTiles(_allTiles[40], _allTiles[41]);

        ConnectTiles(_allTiles[46], _allTiles[40]);

        ConnectTiles(_allTiles[53], _allTiles[63]);

        ConnectTiles(_allTiles[67], _allTiles[74]);
        #endregion

    }

    // 타일 연결
    void ConnectTiles(Tile currentTile, Tile nextTile)
    {
        // StartTile은 일방통행
        if(nextTile == _allTiles[0])        
            return;
        
        if (!currentTile._connectedTiles.Contains(nextTile))
            currentTile._connectedTiles.Add(nextTile);
    }

    // 타일 연결해제
    void DisconnectTiles(Tile currentTile, Tile nextTile)
    {
        if (currentTile._connectedTiles.Contains(nextTile))
            currentTile._connectedTiles.Remove(nextTile);

        if (nextTile._connectedTiles.Contains(currentTile))
            nextTile._connectedTiles.Remove(currentTile);
    }

    Tile CreateNode(Vector3 position, string nodeName)
    {

        // 노드 프리팹 생성
        GameObject newNodeObject = Object.Instantiate(_boardPrefab, position, Quaternion.identity);
        newNodeObject.name = nodeName;

        // Node 스크립트를 가져와 초기화
        Tile newNode = newNodeObject.GetComponent<Tile>();
        _allTiles.Add(newNode);

        return newNode;
    }
}
