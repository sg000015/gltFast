using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnPointerEnter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public int index;

    public void OnPointerExit(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        // Debug.Log("OnPointerEnter:" + index);
    }


    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        // Debug.Log("OnPointerExit:" + index);
    }



}
