using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

//�� ���� -> �ֻ��� ������ -> �ֻ��� ���� ���ڸ�ŭ �̵� -> ���� ��ȣ�ۿ� -> �� ����
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
        
        Managers mg = Managers.Instance; // �Ŵ��� ���� �ϴ���..
    }
    
    void Update()
    {
        if (this._myTurn == false && !Managers.Game.isLive)
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

        //��� ���� ���� �� ĭ�� ����
        for (int i = 0; i < result; i++) 
        {
            //������ ����
            if (_currentTile._connectedTiles.Count >= 3)
            {
                Managers.Game.Pause();
                Managers.UI.ShowPopupUI<Cross_Canvas>(this.transform, "Cross_Canvas");

                yield return StartCoroutine(WaitForDirectionChoice());
            }
            else
            {
                _prevTile = _currentTile;
                _currentTile = !_isDir ? _currentTile._connectedTiles[0] : _currentTile._connectedTiles[1]; //isDir�� true�� 1 false�� 0
                //_currentTile = CheckVisitedTile();
            }


            _desPos = _currentTile.transform.position + _offset;
            Vector3 dir = _desPos - transform.position;
          
            while(dir.magnitude > 0.01f)
            {               
                float moveDist = Mathf.Clamp(3 * Time.deltaTime, 0, dir.magnitude);
                transform.position += dir.normalized * moveDist;
                dir = _desPos - transform.position; //�Ÿ� ������Ʈ

                yield return null;
            }

            Debug.Log($"���� ĭ : {_currentTile.name}");
                
        }

        Debug.Log($"{_currentTile.name}�� ����!");
        //�÷��̾ ���߰� Ÿ�� ��ȣ�ۿ� -- Ÿ��Ÿ���� �޾Ƽ� �����ؾ���
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

        //������ġ ����
        transform.position = _currentTile.transform.position + _offset;
    }
}
