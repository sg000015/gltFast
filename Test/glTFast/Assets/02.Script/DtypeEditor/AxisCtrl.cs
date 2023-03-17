using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisCtrl : MonoBehaviour
{
    public enum Axis { None, X, Y, Z, XY, YZ, ZX }

    [SerializeField] Axis axis;


    //1. 범위에 OnMouseEnter
    //2. OnMouseDown => highLight 이후  OnMouseUp에서 빽, 이게 선처리


    [SerializeField] AxisManager axisManager;
    AxisMove axisMove;


    HighlightPlus.HighlightEffect highlight;

    bool OnDown = false;
    bool onDown
    {
        get { return OnDown; }
        set
        {
            if (value)
            {
                if (!axisManager.onClicking) { OnDown = value; }
            }
            else
            {
                OnDown = value;
            }
        }
    }

    void OnValidate()
    {
        transform.parent.root.GetComponent<AxisManager>();
        highlight = GetComponent<HighlightPlus.HighlightEffect>();
    }

    void Start()
    {
        switch (axis)
        {
            case Axis.X: axisMove = new AxisMoveX(); break;
            case Axis.Y: axisMove = new AxisMoveY(); break;
            case Axis.Z: axisMove = new AxisMoveZ(); break;
            case Axis.XY: axisMove = new AxisMoveXY(); break;
            case Axis.YZ: axisMove = new AxisMoveYZ(); break;
            case Axis.ZX: axisMove = new AxisMoveZX(); break;
            default: axisMove = new AxisMove(); break;
        }
    }

    void OnMouseDown()
    {
        onDown = true;
        axisManager.onClicking = true;
        axisManager.StartClick();

    }
    void OnMouseUp()
    {
        onDown = false;
        highlight.highlighted = false;
        axisManager.onClicking = false;
    }

    void OnMouseEnter()
    {
        if (onDown) { return; }
        if (axisManager.onClicking) { return; }
        highlight.highlighted = true;
    }

    void OnMouseExit()
    {
        if (onDown) { return; }
        highlight.highlighted = false;
    }

    void OnMouseDrag()
    {
        axisMove.Move(axisManager);
    }



    class AxisMove
    {
        public virtual void Move(AxisManager axis) { }
    }

    class AxisMoveX : AxisMove
    {
        public override void Move(AxisManager axis)
        {
            axis.MoveX();
        }
    }
    class AxisMoveY : AxisMove
    {
        public override void Move(AxisManager axis)
        {
            axis.MoveY();
        }

    }
    class AxisMoveZ : AxisMove
    {
        public override void Move(AxisManager axis)
        {
            axis.MoveZ();
        }
    }
    class AxisMoveXY : AxisMove
    {
        public override void Move(AxisManager axis)
        {
            axis.MoveXY();
        }
    }
    class AxisMoveYZ : AxisMove
    {
        public override void Move(AxisManager axis)
        {
            axis.MoveYZ();
        }
    }
    class AxisMoveZX : AxisMove
    {
        public override void Move(AxisManager axis)
        {
            axis.MoveZX();
        }
    }
}

