using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DtypeUI;

public class UIManager : Singleton<UIManager>
{


    ///<summary>
    /// 0:경고, 1:알림
    ///</summary>
    public static void Notice(int type, string msg)
    {
        if (UIManager.Inst == null)
        {
            return;
        }

        UIManager.Inst.NoticeMessage(type, msg);
    }
    public void NoticeMessage(int type, string msg)
    {

        switch (type)
        {
            case 0: Debug.LogError(msg); break;
            case 1: Debug.Log(msg); break;

            default: Debug.Log(msg); break;
        }
    }


}
