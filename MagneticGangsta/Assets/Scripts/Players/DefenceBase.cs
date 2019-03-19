using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceBase : PlayerFunctionBase
{
    public float DefenceCost = 5.0f;
    public override void PlayerLoop()
    {
        if (Input.GetButtonDown("Defence" + Player.ID)) Player.IsDefence = true;
        if (Player.FunctionBases.ContainsKey("Power"))
        {
            Power power = Player.FunctionBases["Power"] as Power;
            if (Player.IsDefence)
                if (!power.PowerCost(DefenceCost * Time.deltaTime)) Player.IsDefence = false;
        }
        if (Input.GetButtonUp("Defence" + Player.ID)) Player.IsDefence = false;
    }


}
