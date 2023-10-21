using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    public Action KeyAction = null;
    public Action<Define.MouseEvnet> MouseAction = null;

    bool _pressed = false;
    public void OnUpdate()
    {
        // if (Input.anyKey == false) {
        //     return;
        // }

        // KeyAction?.Invoke();
        // // if (KeyAction != null) {
        // //     KeyAction.Invoke();
        // // }

        if (Input.anyKey && KeyAction != null) {
            KeyAction.Invoke();
        }

        if (MouseAction != null) {
            if (Input.GetMouseButton(0)) {
                MouseAction.Invoke(Define.MouseEvnet.Press);
                _pressed = true;
            } else {
                if (_pressed) {
                    MouseAction.Invoke(Define.MouseEvnet.Click);
                }
                _pressed = false;
            }
        }
        
    }
}
