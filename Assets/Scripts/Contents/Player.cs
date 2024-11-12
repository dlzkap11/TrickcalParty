using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

//턴 시작 -> 주사위 굴리기 -> 주사위 나온 숫자만큼 이동 -> 보드 상호작용 -> 턴 종료
public class Player : MonoBehaviour
{
    [SerializeField]
    Player _player;
    [SerializeField]
    Dice _dice;
    [SerializeField]
    Board _board;

    
    GameObject _tile;

    Define.State _state = Define.State.Idle;

    [SerializeField]
    public bool _myTurn = false;

    Vector3 _desPos;
    

    [SerializeField]
    int position = 0;
 
    

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
        if (this._myTurn == false)
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
        
        int ct = _board._boardTile.Length;
        

        //결과 값에 따라 한 칸씩 전진
        for (int i = 0; i < result; i++) 
        {
            //if (_board._boardTile[(i + 1 + position) % ct] == null) //보드타일이 중간에 사라졌을 때 스킵하고 다음 칸으로 전진                                                                   
                //to do
          
            _desPos = _board._boardTile[(i + 1 + position) % ct].transform.position + new Vector3(0.0f, 0.1f, 0.0f);
            Vector3 dir = _desPos - transform.position;

            while(dir.magnitude > 0.01f)
            {
                float moveDist = Mathf.Clamp(3 * Time.deltaTime, 0, dir.magnitude);
                transform.position += dir.normalized * moveDist;
                dir = _desPos - transform.position; //거리 업데이트

                yield return null;
            }


            Debug.Log($"현재 칸 : {_board._boardTile[(i + 1 + position) % ct]}");
            
            //플레이어가 시작지점을 지나갈 때
            if (_board._boardTile[(i + 1 + position) % ct] == _board._boardTile[0])
                OnStartTile();

        }

        //Debug.Log("이동 완료");
      
        //플레이어가 멈추고 타일 상호작용
        OnPlayerStop(_board._boardTile[(result + position) % ct]);

        position += result; // 자신의 위치 기억
    }

    void OnStartTile()
    {
        Debug.Log("한바퀴 완주!");
    }

    public void OnPlayerStop(GameObject tile)
    {
        switch(tile.name)
        {
            case "StartTile":
                Debug.Log("StartTile 도착");
                break;

            case "GreenTile":
                Debug.Log("GreenTile 도착");
                break;

            case "RedTile":
                Debug.Log("RedTile 도착");
                break;

            case "BlueTile":
                Debug.Log("BlueTile 도착");
                break;

        }

    }


    public void Init()
    {
        //int i = 0;
        _player = this;
        GameObject go = GameObject.Find("Board");
        _board = go.GetComponent<Board>();

        GameObject dio = GameObject.Find("Dice");
        _dice = dio.GetComponent<Dice>();


        
        //시작위치 조정
        //Vector3 startPoint = _board._boardTile[0].transform.position;
        //transform.position = startPoint;
    }
}
