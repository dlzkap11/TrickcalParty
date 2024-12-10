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
        _boardPrefab = Managers.Resource.Instantiate("Board"); //���� �ҷ�����

        FindTiles();
    }


    void FindTiles()
    {
        foreach (Tile tiles in _boardPrefab.transform.GetComponentsInChildren<Tile>())
        {
            _allTiles.Add(tiles);
        }

        #region Ÿ�� ����

        // �Ϲ����� ��ô
        for (int i = 0; i < (_allTiles.Count - 1); i++)
        {
            //if (_allTiles[i].name == "Cross")
                //continue;
            ConnectTiles(_allTiles[i], _allTiles[i + 1]); 
        }

        //������ ���� ����
        ConnectTiles(_allTiles[32], _allTiles[1]);
        ConnectTiles(_allTiles[62], _allTiles[8]);
        ConnectTiles(_allTiles[41], _allTiles[37]);
        ConnectTiles(_allTiles[40], _allTiles[46]);
        ConnectTiles(_allTiles[63], _allTiles[53]);
        ConnectTiles(_allTiles[74], _allTiles[67]);

        // �Ϲ����� ���ư��� �� ��ô
        for (int i = 0; i < (_allTiles.Count - 1); i++)
        {
            //if (_allTiles[i].name == "Cross")
            //continue;
            ConnectTiles(_allTiles[i + 1], _allTiles[i]); 
        }

        //������ ���� ����
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

    // Ÿ�� ����
    void ConnectTiles(Tile currentTile, Tile nextTile)
    {
        // StartTile�� �Ϲ�����
        if(nextTile == _allTiles[0])        
            return;
        
        if (!currentTile._connectedTiles.Contains(nextTile))
            currentTile._connectedTiles.Add(nextTile);
    }

    // Ÿ�� ��������
    void DisconnectTiles(Tile currentTile, Tile nextTile)
    {
        if (currentTile._connectedTiles.Contains(nextTile))
            currentTile._connectedTiles.Remove(nextTile);

        if (nextTile._connectedTiles.Contains(currentTile))
            nextTile._connectedTiles.Remove(currentTile);
    }

    Tile CreateNode(Vector3 position, string nodeName)
    {

        // ��� ������ ����
        GameObject newNodeObject = Object.Instantiate(_boardPrefab, position, Quaternion.identity);
        newNodeObject.name = nodeName;

        // Node ��ũ��Ʈ�� ������ �ʱ�ȭ
        Tile newNode = newNodeObject.GetComponent<Tile>();
        _allTiles.Add(newNode);

        return newNode;
    }
}
