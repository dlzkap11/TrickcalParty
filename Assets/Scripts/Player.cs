using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

//�� ���� -> �ֻ��� ������ -> �ֻ��� ���� ���ڸ�ŭ �̵� -> ���� ��ȣ�ۿ� -> �� ����
public class Player : MonoBehaviour
{
    [SerializeField]
    Player _player;
    [SerializeField]
    Dice _dice = new Dice();
    [SerializeField]
    Board _board;
    

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
        
        Managers mg = Managers.Instance; // �Ŵ��� ���� �ϴ���..
    }
    
    void Update()
    {
        if (this._myTurn == false)
            return;

        if (Input.GetKeyDown(KeyCode.Space)) // �����̽� Ű�� ������ ��
        {
            int rolledNumber = _dice.OnDiceRoll();

            StartCoroutine(this.OnMoving(rolledNumber));
        }
    }

    public IEnumerator OnMoving(int result)
    {
        _myTurn = false;
        
        int ct = _board._boardTile.Length;
        

        //��� ���� ���� �� ĭ�� ����
        for (int i = 0; i < result; i++) 
        {
            //if (_board._boardTile[(i + 1 + position) % ct] == null) //����Ÿ���� �߰��� ������� �� ��ŵ�ϰ� ���� ĭ���� ����                                                                   
                //to do
          
            _desPos = _board._boardTile[(i + 1 + position) % ct].transform.position + new Vector3(0.0f, 0.1f, 0.0f);
            Vector3 dir = _desPos - transform.position;

            while(dir.magnitude > 0.01f)
            {
                float moveDist = Mathf.Clamp(1 * Time.deltaTime, 0, dir.magnitude);
                transform.position += dir.normalized * moveDist;
                dir = _desPos - transform.position; //�Ÿ� ������Ʈ
                
                yield return null;
            }


            Debug.Log($"���� ĭ : {_board._boardTile[(i + 1 + position) % ct]}");
            

        }

        position += result;  // �ڽ��� ��ġ ���

        Debug.Log("�̵� �Ϸ�");    
        

    }

    public void Init()
    {
        //int i = 0;
        _player = this;
        GameObject go = GameObject.Find("Board");
        _board = go.GetComponent<Board>();

        //������ġ ����
        //Vector3 startPoint = _board._boardTile[0].transform.position;
        //transform.position = startPoint;
    }
}
