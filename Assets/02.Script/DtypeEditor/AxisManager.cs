using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AxisManager : MonoBehaviour
{

    public GameObject target;


    ///<summary>
    /// 0: gray , 1~3 : rgb, 4: 
    ///</summary>
    public Material[] mats;

    // 추후적용
    // [SerializeField] HighlightPlus.HighlightEffect highlight;








    public UnityAction<AxisCtrl.Axis> OnSelect = (axis) => { };
    public bool onClicking = false;

    //todo 임시, 드래그할때 확대축소 고려해서 설정할것

    [Range(0, 5)]
    public float cameraDistance;

    [Range(0, 5)]
    public float moveValue;

    [Range(0, 5)]
    public float scaleValue;



    Vector3 prePos;
    Vector3 disPos;

    Coroutine coroutine;

    public void StartClick()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(OnDrag());
    }



    IEnumerator OnDrag()
    {
        Vector3 mousePosition;
        Vector3 objPosition;
        Vector3 startPosition = transform.position;

        while (onClicking)
        {
            mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraDistance);


            //마우스포지션 월드로 변환
            objPosition = Camera.main.ScreenToWorldPoint(mousePosition) - startPosition;

            // objPosition -= transform.position;
            //마우스월드 - transform.position


            if (prePos != Vector3.zero)
            {
                disPos = objPosition - prePos;
            }
            prePos = objPosition;
            yield return Yielder.EndOfFrame;
        }
        prePos = Vector3.zero;
        disPos = Vector3.zero;
        OnSelect(AxisCtrl.Axis.None);
        // UpdateScale();
    }


    public void MoveX()
    {
        transform.position += transform.right * disPos.x * moveValue;
    }
    public void MoveY()
    {
        transform.position += transform.up * disPos.y * moveValue;
    }
    public void MoveZ()
    {
        transform.position += transform.forward * disPos.z * moveValue;
    }
    public void MoveXY()
    {
        transform.position += transform.right * disPos.x * moveValue;
        transform.position += transform.up * disPos.y * moveValue;
    }
    public void MoveYZ()
    {
        transform.position += transform.up * disPos.y * moveValue;
        transform.position += transform.forward * disPos.z * moveValue;
    }
    public void MoveZX()
    {
        transform.position += transform.forward * disPos.z * moveValue;
        transform.position += transform.right * disPos.x * moveValue;
    }



    [SerializeField] Transform[] scaleTr;

    public void ScaleX()
    {
        // transform.localScale = new Vector3(transform.localScale.x * (1 + disPos.x * scaleValue), transform.localScale.y, transform.localScale.z);
        target.transform.localScale += Vector3.right * disPos.x * scaleValue;
    }

    public void ScaleY()
    {
        target.transform.localScale += Vector3.up * disPos.y * scaleValue;
    }

    public void ScaleZ()
    {
        target.transform.localScale += Vector3.forward * disPos.z * scaleValue;
    }

    void UpdateScale()
    {
        Vector3 scale = transform.localScale;

        if (target != null)
        {
            target.transform.localScale = new Vector3(target.transform.localScale.x * scale.x, target.transform.localScale.y * scale.y, target.transform.localScale.z * scale.z);
        }
    }




}
