using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

//턴 시작 -> 주사위 굴리기 -> 주사위 나온 숫자만큼 이동 -> 보드 상호작용 -> 턴 종료
public class Player : MonoBehaviour
{
    [SerializeField]
    Dice _dice;
    [SerializeField]
    public Tile _currentTile;
    [SerializeField]
    public Tile _prevTile;

    GameObject _tile;

    Define.State _state = Define.State.Idle;

    [SerializeField]
    public bool _myTurn = false;
    public bool _isDir = false;
    public bool _isDirChoose = false;

    Vector3 _offset = new Vector3(0.0f, 0.1f, 0.0f);
    Vector3 _desPos;
 
    public Define.State State 
    { 
        get { return _state; }
        set
        {
            _state = value;

            switch(_state)
            {
                case Define.State.Idle:
                    break;

                case Define.State.Moving:                     
                    break;

            }
        }
    }

    void Start()
    {
        Init();
        
        _myTurn = true;
        
        Managers mg = Managers.Instance; // 매니저 생성 일단은..
    }
    
    void Update()
    {
        if (this._myTurn == false && !Managers.Game.isLive)
            return;

        if (Input.GetKeyDown(KeyCode.Space)) // 스페이스 키를 눌렀을 때
        {
            int rolledNumber = _dice.OnDiceRoll();

            StartCoroutine(this.OnMoving(rolledNumber));
        }
    }

    public IEnumerator OnMoving(int result)
    {
        _myTurn = false;

        //결과 값에 따라 한 칸씩 전진
        for (int i = 0; i < result; i++) 
        {
            //갈림길 선택
            if (_currentTile._connectedTiles.Count >= 3)
            {
                Managers.Game.Pause();
                Managers.UI.ShowPopupUI<Cross_Canvas>(this.transform, "Cross_Canvas");

                yield return StartCoroutine(WaitForDirectionChoice());
            }
            else
            {
                _prevTile = _currentTile;
                _currentTile = !_isDir ? _currentTile._connectedTiles[0] : _currentTile._connectedTiles[1]; //isDir이 true면 1 false면 0
                //_currentTile = CheckVisitedTile();
            }


            _desPos = _currentTile.transform.position + _offset;
            Vector3 dir = _desPos - transform.position;
          
            while(dir.magnitude > 0.01f)
            {               
                float moveDist = Mathf.Clamp(3 * Time.deltaTime, 0, dir.magnitude);
                transform.position += dir.normalized * moveDist;
                dir = _desPos - transform.position; //거리 업데이트

                yield return null;
            }

            Debug.Log($"현재 칸 : {_currentTile.name}");
                
        }

        Debug.Log($"{_currentTile.name}에 도착!");
        //플레이어가 멈추고 타일 상호작용 -- 타일타입을 받아서 적용해야함
    }

    public IEnumerator WaitForDirectionChoice()
    {
        _isDirChoose = false;

        while (!_isDirChoose)
        {
            yield return null;
        }

    }

    public void Init()
    {
        GameObject go;

        go = GameObject.Find("Dice");
        _dice = go.GetComponent<Dice>();

        go = GameObject.Find("StartTile");
        _currentTile = go.GetComponent<Tile>();

        //시작위치 조정
        transform.position = _currentTile.transform.position + _offset;
    }
}
