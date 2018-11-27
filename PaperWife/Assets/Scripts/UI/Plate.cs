using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour {

    [SerializeField] List<FoodShow> foodShows;

    private void Start()
    {
        FoodShow[] foods = FindObjectsOfType<FoodShow>();
        for (int i = 0; i < foods.Length; i++)
        {
            foodShows.Add(foods[i]);
            foods[i].OnFoodChange += OnFoodChange;
        }
    }

    void OnFoodChange()
    {
        Temperament temperament = Temperament.None;
        for (int i = 0; i < foodShows.Count; i++)
        {
            switch (foodShows[i].m_temperament)
            {
                case Temperament.TemperamentType.Mad:
                    temperament.Mad += 1;
                    break;
                case Temperament.TemperamentType.Sad:
                    temperament.Sad += 1;
                    break;
                case Temperament.TemperamentType.Complacent:
                    temperament.Complacent += 1;
                    break;
                default:
                    break;
            }
        }

        Level1Control.Instance.Food = temperament;
    }

    public void FoodRefresh()
    {
        if (Level1Control.Instance.Food == Temperament.None)
        {
            for (int i = 0; i < foodShows.Count; i++)
            {
                foodShows[i].SetState(Temperament.TemperamentType.None);
            }
        }
    }

    
}
