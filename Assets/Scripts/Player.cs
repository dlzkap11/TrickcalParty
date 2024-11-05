using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

//�� ���� -> �ֻ��� ������ -> �ֻ��� ���� ���ڸ�ŭ �̵� -> ���� ��ȣ�ۿ� -> �� ����
public class Player : MonoBehaviour
{
    Player _player;
    Dice _dices;
    Board _board;
    int[] _dice = { 1, 2, 3, 4, 5};

    Define.State _state = Define.State.Idle;

    [SerializeField]
    public bool _myTurn = false;
    Vector3 _desPos;
    int result;

    [SerializeField]
    int position = 0;

    

    [SerializeField]
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

    }

    //�ֻ��� ������
    public int OnDiceRoll()
    {

        result = Random.Range(0, _dice.Length);
        Debug.Log("�ֻ��� �� : "+ _dice[result]);

        return _dice[result];
        
    }

    public IEnumerator OnMoving(int result)
    {
        _myTurn = false;
        int ct = _boardTile.Length;

        //��� ���� ���� �� ĭ�� ����
        for (int i = 0; i < result; i++) 
        {
            
            _desPos = _boardTile[(i + 1 + position) % ct].transform.position + new Vector3(0.0f, 0.1f, 0.0f);
            Vector3 dir = _desPos - transform.position;

            while(dir.magnitude > 0.01f)
            {
                float moveDist = Mathf.Clamp(1 * Time.deltaTime, 0, dir.magnitude);
                transform.position += dir.normalized * moveDist;
                dir = _desPos - transform.position; //�Ÿ� ������Ʈ
                
                yield return null;
            }


            Debug.Log($"���� ĭ : {_boardTile[(i + 1 + position) % ct]}");
            

        }

        position += result;  // �ڽ��� ��ġ ���

        Debug.Log("�̵� �Ϸ�");    
        

    }

    public void Init()
    {
        //int i = 0;
        _boardTile = new GameObject[4];
        go = GameObject.Find($"StartTile");
        _boardTile[0] = go;
        go = GameObject.Find("RedTile");
        _boardTile[1] = go;
        go = GameObject.Find("BlueTile");
        _boardTile[2] = go;
        go = GameObject.Find("GreenTile");
        _boardTile[3] = go;
    }
}
