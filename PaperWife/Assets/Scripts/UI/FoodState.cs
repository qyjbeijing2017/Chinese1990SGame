using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FoodState : MonoBehaviour,IBeginDragHandler,IEndDragHandler,IDragHandler {

    Vector3 m_startT;
    int m_startSiblingIndex;
    [SerializeField] Temperament.TemperamentType m_temperament;
    [SerializeField] Image m_image;


    public void OnBeginDrag(PointerEventData eventData)
    {
        m_image.raycastTarget = false;
        m_startT = transform.position;
        m_startSiblingIndex = transform.GetSiblingIndex();
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {

        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject go = eventData.pointerEnter;
        Debug.Log(go.name);
        m_image.raycastTarget = true;
        transform.position = m_startT;
        transform.SetSiblingIndex(m_startSiblingIndex);

        if (go.transform.parent.GetComponent<FoodShow>())
        {
            go.transform.parent.GetComponent<FoodShow>().SetState(m_temperament);
        }
    }

}
