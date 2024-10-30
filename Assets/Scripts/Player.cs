using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//턴 시작 -> 주사위 굴리기 -> 주사위 나온 숫자만큼 이동 -> 보드 상호작용 -> 턴 종료
public class Player : MonoBehaviour
{
    Player _player;
    Board _board;
    int[] _dice = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    bool _myTurn = false;

    Vector3 _desPos;

    GameObject[] _boardTile = { };

    void Start()
    {
        _myTurn = true;
    }
    
    void Update()
    {
        //내 턴이 아니면 리턴
        if (_myTurn == false)
            return;

        OnDiceRoll();
      
    }

    //주사위 굴리기
    void OnDiceRoll()
    {
        int result = Random.Range(0, _dice.Length);
        Debug.Log(_dice[result]);

        OnMoving();

    }

    void OnMoving()
    {

        GameObject tile = GameObject.Find("RedTile");
        _desPos = tile.transform.position + new Vector3(0.0f, 0.1f, 0.0f);
        Vector3 dir = _desPos - transform.position;

        float moveDist = Mathf.Clamp(1 * Time.deltaTime, 0, dir.magnitude);  
        transform.position += dir.normalized * moveDist;

        

        if (dir.magnitude < 0.01f)
        {
            Debug.Log("이동 완료");
            _myTurn = false;
        }
            
        //_boardTile[0] = GameObject.Find("RedTile");

    }
}
