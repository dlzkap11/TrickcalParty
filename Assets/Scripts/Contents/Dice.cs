using UnityEngine;

public class Dice : MonoBehaviour
{
    int[] _dice = { 1, 2, 3, 4, 5 };
    int result;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    //�ֻ��� ������
    public int OnDiceRoll()
    {

        result = Random.Range(0, _dice.Length);
        Debug.Log("�ֻ��� �� : " + _dice[result]);

        return _dice[result];

    }

}
