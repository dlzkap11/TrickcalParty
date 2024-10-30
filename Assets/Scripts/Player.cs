using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�� ���� -> �ֻ��� ������ -> �ֻ��� ���� ���ڸ�ŭ �̵� -> ���� ��ȣ�ۿ� -> �� ����
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
        //�� ���� �ƴϸ� ����
        if (_myTurn == false)
            return;

        OnDiceRoll();
      
    }

    //�ֻ��� ������
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
            Debug.Log("�̵� �Ϸ�");
            _myTurn = false;
        }
            
        //_boardTile[0] = GameObject.Find("RedTile");

    }
}
