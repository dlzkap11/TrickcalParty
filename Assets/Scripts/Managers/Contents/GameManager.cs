using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player _player;
    private Dice _dice;
    private void Start()
    {

    }

    public void RollAndMove()
    {
        if (_player._myTurn == false)
            return;

        int rolledNumber = _player.OnDiceRoll();
        
        StartCoroutine(_player.OnMoving(rolledNumber));

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // 스페이스 키를 눌렀을 때
        {
            RollAndMove();
        }
    }

}
