using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField]
    int[] _dice = { 2, 2, 3, 4, 5, 6 };
    int result;

    //�ֻ��� ������
    public int OnDiceRoll()
    {

        result = Random.Range(0, _dice.Length);
        Debug.Log("�ֻ��� �� : " + _dice[result]);

        return _dice[result];

    }

}
