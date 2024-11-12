using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager
{

    public Action KeyAction = null;
    public Action<Define.MouseEvent> MouseEventAction = null;


    bool _press = false;
    float _pressedTime = 0;


    public void OnUpdate()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if(Input.anyKey && KeyAction != null )
            KeyAction.Invoke();

        if( MouseEventAction != null)
        {
            if (Input.GetMouseButton(0))
            {
                if(!_press)
                {
                    MouseEventAction.Invoke(Define.MouseEvent.PointerDown);
                    _pressedTime = Time.time;
                }
                MouseEventAction.Invoke(Define.MouseEvent.Press);
                _press = true;
            }
            else
            {
                if (_press)
                {
                    if(Time.time < _pressedTime + 0.2f)                  
                        MouseEventAction.Invoke(Define.MouseEvent.Click);
                    MouseEventAction.Invoke(Define.MouseEvent.PointerUp);
                }
                _press = false;
                _pressedTime = 0;
            }
        }
    }

    public void Clear()
    {
        KeyAction = null;
        MouseEventAction = null;
    }
}
