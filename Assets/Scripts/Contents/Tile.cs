using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    public Define.Tiles TilesType = Define.Tiles.Unknown;
    public List<Tile> _connectedTiles; // 연결된 타일 리스트

    private void OnDrawGizmos()
    {
        // 씬에서 연결된 타일을 확인 가능 ㄷㄷ;;
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

