using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMgr : SingleBase<InputMgr>
{
    private bool isStart = false;
    public InputMgr() 
    {
        MonoMgr.Instance().UpdateAddListener(Update);
    }
    public void StartOrEnd(bool open)
    {
        isStart = open;
    }
    public void Update()
    {
        if (!isStart)
            return;
        checkKeyCode(KeyCode.W);
        checkKeyCode(KeyCode.A);
        checkKeyCode(KeyCode.S);
        checkKeyCode(KeyCode.D);
    }
    private void checkKeyCode(KeyCode keyCode)
    {
        if(Input.GetKeyDown(keyCode))
        {
            EventCenter.Instance().EventTrigger("KeyDown", keyCode);
        }
        if (Input.GetKeyUp(keyCode))
        {
            EventCenter.Instance().EventTrigger("KeyUp", keyCode);
        }
    }
}
