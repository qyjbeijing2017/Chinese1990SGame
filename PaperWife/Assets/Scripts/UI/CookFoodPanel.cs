using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaemonTools;

public class CookFoodPanel :UIBase {

    [SerializeField]Plate m_plate;
    public override void close()
    {
        
    }

    public override void show(bool IsfirstOpen, object[] value)
    {
        m_plate.FoodRefresh();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            UIManager.Instance.close();
        }
    }

}
