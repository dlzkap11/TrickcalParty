using System;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.UI.Image;

public class Cross_Canvas : UI_Popup
{
    Player _player;
    Tile _currentTile;

    public override void Init()
    {
        base.Init();
        Transform parent = transform.parent;
        transform.position = parent.position;
        Canvas canvas = transform.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = Camera.main;

        GameObject go = GameObject.FindWithTag("Player");
        _player = go.GetComponent<Player>();
        _currentTile = _player._currentTile;

        //갈림길 UI버튼 생성
        for (int i = 0; i < _currentTile._connectedTiles.Count; i++)
        {            
            if (_player._prevTile == _currentTile._connectedTiles[i])
                continue;

            CreatePathButton(i, _currentTile._connectedTiles[i]);
        }

    }

    private void CreatePathButton(int index, Tile _connectedTile)
    {    
           
        GameObject gos = Managers.Resource.Instantiate($"UI/Popup/Path_Button");
        gos.transform.SetParent(this.transform);

        gos.name = $"Path_Button_{index}";
        Debug.Log(_connectedTile.name);
        gos.transform.position = (_currentTile.transform.position + _connectedTile.transform.position) / 2;

        Vector3 dir = (_currentTile.transform.position - _connectedTile.transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        gos.transform.rotation = Quaternion.Euler(0, 0, angle) * Quaternion.Euler(0, 0, 90); //각도 조정

        //버튼 바인딩
        Button button = gos.GetComponent<Button>();
        if(button != null)
        {
            button.onClick.AddListener(() => OnPathButtonClick(index));
        }
    }

    private void OnPathButtonClick(int index)
    {
        Debug.Log("갈림길 선택!");

        _player._prevTile = _player._currentTile;
        _player._currentTile = _currentTile._connectedTiles[index];
        if (_player._prevTile == _player._currentTile._connectedTiles[0] && _player._isDir == false || 
            _player._prevTile == _player._currentTile._connectedTiles[1] && _player._isDir == true)
            _player._isDir = !_player._isDir;

        _player._isDirChoose = true;
        Managers.Game.Resume();
        ClosePopupUI();
        
    }

}
