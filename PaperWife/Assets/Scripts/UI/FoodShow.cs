using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class FoodShow : MonoBehaviour {

    public Temperament.TemperamentType m_temperament;
    [SerializeField] Image m_mad;
    [SerializeField] Image m_sad;
    [SerializeField] Image m_complacent;
    [SerializeField] Image m_none;
    public event UnityAction OnFoodChange;

    public void SetState(Temperament.TemperamentType temperamentType)
    {
        m_temperament = temperamentType;
        switch (temperamentType)
        {
            case Temperament.TemperamentType.Mad:
                m_mad.gameObject.SetActive(true);
                m_sad.gameObject.SetActive(false);
                m_complacent.gameObject.SetActive(false);
                m_none.gameObject.SetActive(false);
                break;
            case Temperament.TemperamentType.Sad:
                m_mad.gameObject.SetActive(false);
                m_sad.gameObject.SetActive(true);
                m_complacent.gameObject.SetActive(false);
                m_none.gameObject.SetActive(false);
                break;
            case Temperament.TemperamentType.Complacent:
                m_mad.gameObject.SetActive(false);
                m_sad.gameObject.SetActive(false);
                m_complacent.gameObject.SetActive(true);
                m_none.gameObject.SetActive(false);
                break;
            default:
                m_mad.gameObject.SetActive(false);
                m_sad.gameObject.SetActive(false);
                m_complacent.gameObject.SetActive(false);
                m_none.gameObject.SetActive(true);
                break;
        }
        if (OnFoodChange != null)
        {
            OnFoodChange.Invoke();
        }
    }
}
