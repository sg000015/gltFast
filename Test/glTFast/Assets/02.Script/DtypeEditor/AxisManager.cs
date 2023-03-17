using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisManager : MonoBehaviour
{

    public bool onClicking = false;

    //todo 임시, 드래그할때 확대축소 고려해서 설정할것

    [Range(0, 5)]
    public float cameraDistance;

    [Range(0, 5)]
    public float moveValue;



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
        while (onClicking)
        {
            mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraDistance);
            objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            if (prePos != Vector3.zero)
            {
                disPos = objPosition - prePos;
            }
            prePos = objPosition;
            yield return Yielder.EndOfFrame;
        }
        prePos = Vector3.zero;
        disPos = Vector3.zero;
    }


    public void MoveX()
    {
        transform.position += Vector3.right * disPos.x * moveValue;
    }
    public void MoveY()
    {
        transform.position += Vector3.up * disPos.y * moveValue;
    }
    public void MoveZ()
    {
        transform.position += Vector3.forward * disPos.z * moveValue;
    }
    public void MoveXY()
    {
        transform.position += Vector3.right * disPos.x * moveValue;
        transform.position += Vector3.up * disPos.y * moveValue;
    }
    public void MoveYZ()
    {
        transform.position += Vector3.up * disPos.y * moveValue;
        transform.position += Vector3.forward * disPos.z * moveValue;
    }
    public void MoveZX()
    {
        transform.position += Vector3.forward * disPos.z * moveValue;
        transform.position += Vector3.right * disPos.x * moveValue;
    }





}
