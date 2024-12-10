using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField]
    int[] _dice = { 2, 2, 3, 4, 5, 6 };
    int result;

    //주사위 굴리기
    public int OnDiceRoll()
    {

        result = Random.Range(0, _dice.Length);
        Debug.Log("주사위 값 : " + _dice[result]);

        return _dice[result];

    }

}
