using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    public Define.Tiles TilesType = Define.Tiles.Unknown;
    public List<Tile> _connectedTiles; // ����� Ÿ�� ����Ʈ

    private void OnDrawGizmos()
    {
        // ������ ����� Ÿ���� Ȯ�� ���� ����;;
        Gizmos.color = Color.red;

        if (_connectedTiles != null)
        {
            foreach (Tile tile in _connectedTiles)
            {
                if (tile != null)
                {
                    Gizmos.DrawLine(transform.position, tile.transform.position);
                }
            }
        }
    }
}

