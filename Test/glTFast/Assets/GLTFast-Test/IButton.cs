using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEngine.Events;





///자바스크립트 함수 원활한 동작을 위한, 더블클릭 처리인듯?
public class IButton : Button
{

    bool isChecked;

    [Serializable]
    public class ButtonDownEvent : UnityEvent { }
    [Serializable]
    public class ButtonUpEvent : UnityEvent { }
    // [Serializable]
    // public class ButtonClickEvent : UnityEvent { }
    // [Serializable]
    // public class ButtonToggleDownEvent : UnityEvent { }
    // [Serializable]
    // public class ButtonToggleUpEvent : UnityEvent { }


    [SerializeField]
    ButtonDownEvent _onDown = new ButtonDownEvent();
    [SerializeField]
    ButtonUpEvent _onUp = new ButtonUpEvent();
    // [SerializeField]
    // ButtonClickEvent _onClick = new ButtonClickEvent();

    // [SerializeField]
    // ButtonToggleDownEvent _onToggleDown = new ButtonToggleDownEvent();
    // [SerializeField]
    // ButtonToggleUpEvent _onToggleUp = new ButtonToggleUpEvent();

    public ButtonDownEvent onDown
    {
        get { return _onDown; }
        set { _onDown = value; }
    }

    public ButtonUpEvent onUp
    {
        get { return _onUp; }
        set { _onUp = value; }
    }

    // public ButtonClickEvent onclick
    // {
    //     get { return _onClick; }
    //     set { _onClick = value; }
    // }

    // public ButtonToggleDownEvent onToggleDown
    // {
    //     get { return _onToggleDown; }
    //     set { _onToggleDown = value; }
    // }

    // public ButtonToggleUpEvent onToggleUp
    // {
    //     get { return _onToggleUp; }
    //     set { _onToggleUp = value; }
    // }

    // protected IButton() { }

    // public override void OnPointerClick(PointerEventData eventData)
    // {
    //     base.OnPointerClick(eventData);

    //     if (eventData.button == PointerEventData.InputButton.Left)
    //     {
    //         _onClick.Invoke();
    //     }
    // }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            _onDown.Invoke();
            _onUp.Invoke();
        }
    }

    // public override void OnPointerUp(PointerEventData eventData)
    // {
    //     base.OnPointerUp(eventData);
    //     if (eventData.button == PointerEventData.InputButton.Left)
    //     {
    //         if (isChecked)
    //         {
    //             _onToggleUp.Invoke();
    //             isChecked = false;
    //         }
    //         else
    //         {
    //             _onToggleDown.Invoke();
    //             isChecked = true;
    //         }
    //     }
    // }




}
