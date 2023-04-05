using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisCtrl : MonoBehaviour
{
    public enum Axis { None, X, Y, Z, XY, YZ, ZX }
    public enum AxisType { None, Move, Rotate, Scale }

    [SerializeField] Axis axis;
    [SerializeField] AxisType type;


    //1. 범위에 OnMouseEnter
    //2. OnMouseDown => highLight 이후  OnMouseUp에서 빽, 이게 선처리


    [SerializeField] AxisManager axisManager;
    AxisClass axisClass;


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
        axisManager = transform.parent.root.GetComponent<AxisManager>();
        highlight = GetComponent<HighlightPlus.HighlightEffect>();
    }

    void Start()
    {
        switch (type)
        {
            case AxisType.Move:
                switch (axis)
                {
                    case Axis.X: axisClass = new AxisMoveX(); break;
                    case Axis.Y: axisClass = new AxisMoveY(); break;
                    case Axis.Z: axisClass = new AxisMoveZ(); break;
                    case Axis.XY: axisClass = new AxisMoveXY(); break;
                    case Axis.YZ: axisClass = new AxisMoveYZ(); break;
                    case Axis.ZX: axisClass = new AxisMoveZX(); break;
                    default: axisClass = new AxisClass(); break;
                }
                break;
            case AxisType.Rotate:
                //todo

                break;
            case AxisType.Scale:
                switch (axis)
                {
                    case Axis.X: axisClass = new AxisScaleX(); break;
                    case Axis.Y: axisClass = new AxisScaleY(); break;
                    case Axis.Z: axisClass = new AxisScaleZ(); break;
                    default: axisClass = new AxisClass(); break;
                }
                break;
        }

        axisManager.OnSelect += OnSelect;
    }

    void OnSelect(Axis _axis)
    {
        if (_axis == Axis.None)
        {
            EnableMaterial();
            highlight.highlighted = false;
        }
        else
        {
            DisableMaterial();
            axisClass.SetHightlight(_axis, highlight);
        }
    }

    void DisableMaterial()
    {
        foreach (var renderer in GetComponentsInChildren<Renderer>())
        {
            renderer.sharedMaterial = axisManager.mats[0];
        }
    }

    void EnableMaterial()
    {
        foreach (var renderer in GetComponentsInChildren<Renderer>())
        {
            renderer.sharedMaterial = axisManager.mats[(int)axis];
        }
    }



    void OnMouseDown()
    {
        onDown = true;
        axisManager.onClicking = true;
        axisManager.StartClick();
        highlight.highlighted = true;
        axisManager.OnSelect.Invoke(axis);

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
        if (axisManager.onClicking) { return; }
        highlight.highlighted = false;
    }

    void OnMouseDrag()
    {
        axisClass.OnDrag(axisManager);
    }






    #region  AxisMove



    class AxisClass
    {
        public virtual void OnDrag(AxisManager manager) { }
        public virtual void SetHightlight(Axis axis, HighlightPlus.HighlightEffect highlight) { }
    }

    class AxisMoveX : AxisClass
    {
        public override void OnDrag(AxisManager manager)
        {
            manager.MoveX();
        }

        public override void SetHightlight(Axis axis, HighlightPlus.HighlightEffect highlight)
        {
            if (axis == Axis.X || axis == Axis.XY || axis == Axis.ZX)
            {
                highlight.highlighted = true;
            }
        }
    }
    class AxisMoveY : AxisClass
    {
        public override void OnDrag(AxisManager manager)
        {
            manager.MoveY();
        }
        public override void SetHightlight(Axis axis, HighlightPlus.HighlightEffect highlight)
        {
            if (axis == Axis.Y || axis == Axis.XY || axis == Axis.YZ)
            {
                highlight.highlighted = true;
            }
        }

    }
    class AxisMoveZ : AxisClass
    {
        public override void OnDrag(AxisManager manager)
        {
            manager.MoveZ();
        }
        public override void SetHightlight(Axis axis, HighlightPlus.HighlightEffect highlight)
        {
            if (axis == Axis.Z || axis == Axis.YZ || axis == Axis.ZX)
            {
                highlight.highlighted = true;
            }
        }
    }
    class AxisMoveXY : AxisClass
    {
        public override void OnDrag(AxisManager manager)
        {
            manager.MoveXY();
        }
        public override void SetHightlight(Axis axis, HighlightPlus.HighlightEffect highlight)
        {
            if (axis == Axis.XY)
            {
                highlight.highlighted = true;
            }
        }
    }
    class AxisMoveYZ : AxisClass
    {
        public override void OnDrag(AxisManager manager)
        {
            manager.MoveYZ();
        }
        public override void SetHightlight(Axis axis, HighlightPlus.HighlightEffect highlight)
        {
            if (axis == Axis.YZ)
            {
                highlight.highlighted = true;
            }
        }
    }
    class AxisMoveZX : AxisClass
    {
        public override void OnDrag(AxisManager manager)
        {
            manager.MoveZX();
        }
        public override void SetHightlight(Axis axis, HighlightPlus.HighlightEffect highlight)
        {
            if (axis == Axis.ZX)
            {
                highlight.highlighted = true;
            }
        }
    }


    #endregion


    #region AxisRotate

    #endregion


    #region AxisScale

    class AxisScaleX : AxisClass
    {
        public override void OnDrag(AxisManager manager)
        {
            manager.ScaleX();
        }

        public override void SetHightlight(Axis axis, HighlightPlus.HighlightEffect highlight)
        {
            if (axis == Axis.X)
            {
                highlight.highlighted = true;
            }
        }
    }
    class AxisScaleY : AxisClass
    {
        public override void OnDrag(AxisManager manager)
        {
            manager.ScaleY();
        }
        public override void SetHightlight(Axis axis, HighlightPlus.HighlightEffect highlight)
        {
            if (axis == Axis.Y)
            {
                highlight.highlighted = true;
            }
        }

    }
    class AxisScaleZ : AxisClass
    {
        public override void OnDrag(AxisManager manager)
        {
            manager.ScaleZ();
        }
        public override void SetHightlight(Axis axis, HighlightPlus.HighlightEffect highlight)
        {
            if (axis == Axis.Z)
            {
                highlight.highlighted = true;
            }
        }
    }


    #endregion

}

