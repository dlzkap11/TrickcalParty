using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    private Dice dice;
    private void Start()
    {

    }

    public void RollAndMove()
    {
        if (player._myTurn == false)
            return;

        int rolledNumber = player.OnDiceRoll();
        Debug.Log("�ֻ��� ���: " + rolledNumber);
        StartCoroutine(player.OnMoving(rolledNumber));

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // �����̽� Ű�� ������ ��
        {
            RollAndMove();
        }
    }

}
