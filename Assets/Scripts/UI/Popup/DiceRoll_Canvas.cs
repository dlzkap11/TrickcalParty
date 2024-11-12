using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DiceRoll_Canvas : UI_Popup
{
    enum Buttons
    {
        DiceRoll_Button,
    }

    enum Texts
    {
        DiceRoll_Text,
        Result_Text,
    }

    enum Images
    {

    }

    Dice _dice;
    Player _player;

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons)); //enum 값 보내기
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));
        
        GameObject dio = GameObject.Find("Dice");
        _dice = dio.GetComponent<Dice>();
        GameObject go = GameObject.Find("Player");
        _player = go.GetComponent<Player>();

        GetButton((int)Buttons.DiceRoll_Button).gameObject.BindEvent(OnButtonClick);

        //GameObject go = GetImage((int)Images.ItemIcon).gameObject;
        //BindEvent(go, (PointerEventData data) => { go.transform.position = data.position; }, Define.UIEvent.Drag);

    }

    int _result = 0;

    public void OnButtonClick(PointerEventData data)
    {
        if (_player._myTurn == false)
            return;

        Debug.Log("Button Click!");
        _result = _dice.OnDiceRoll();

        StartCoroutine(_player.OnMoving(_result));

        GetText((int)Texts.Result_Text).text = $"주사위 결과 : {_result}";
    }
}
