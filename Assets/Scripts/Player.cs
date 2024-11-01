using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

//턴 시작 -> 주사위 굴리기 -> 주사위 나온 숫자만큼 이동 -> 보드 상호작용 -> 턴 종료
public class Player : MonoBehaviour
{
    Player _player;
    Board _board;
    int[] _dice = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

    Define.State _state = Define.State.Idle;

    [SerializeField]
    public bool _myTurn = false;
    Vector3 _desPos;
    int result;
    int position;

    GameObject[] _boardTile;
    GameObject go = null;

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
    }
    
    void Update()
    {
        //내 턴이 아니면 리턴
        

        /*
        switch (State)
        {
            case Define.State.Idle:
                OnDiceRoll(); 
                break;
                
            case Define.State.Moving:
                OnMoving(result);
                break;

        }
        */
        if (Input.GetKeyDown(KeyCode.A))
            OnDiceRoll();

    }

    //주사위 굴리기
    public int OnDiceRoll()
    {
        

        result = Random.Range(0, _dice.Length);
        Debug.Log(_dice[result]);

        return _dice[result];
        
    }

    public IEnumerator OnMoving(int result)
    {
        _myTurn = false;
        int ct = _boardTile.Length;
        //결과 값에 따라 한 칸씩 전진
        for (int i = 0; i < result; i++) 
        {
            
            _desPos = _boardTile[(i + 1) % ct].transform.position + new Vector3(0.0f, 0.1f, 0.0f);
            Vector3 dir = _desPos - transform.position;

            while(dir.magnitude > 0.01f)
            {
                float moveDist = Mathf.Clamp(1 * Time.deltaTime, 0, dir.magnitude);
                transform.position += dir.normalized * moveDist;
                dir = _desPos - transform.position; //거리 업데이트
                yield return null;
            }

            Debug.Log($"현재 칸 : {_boardTile[(i + 1) % ct]}");
            
        }
    
        Debug.Log("이동 완료");    
        

    }

    public void Init()
    {
        _boardTile = new GameObject[4];
        go = GameObject.Find("StartTile");
        _boardTile[0] = go;
        go = GameObject.Find("RedTile");
        _boardTile[1] = go;
        go = GameObject.Find("BlueTile");
        _boardTile[2] = go;
        go = GameObject.Find("GreenTile");
        _boardTile[3] = go;
    }
}
