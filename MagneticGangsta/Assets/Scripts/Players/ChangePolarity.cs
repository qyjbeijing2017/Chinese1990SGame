using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePolarity : PlayerFunctionBase
{

    public override void PlayerLoop()
    {
        if (Input.GetButtonDown("ChangePolarity" + Player.ID))
        {
            Player.PlayerPolarity = !Player.PlayerPolarity;
        }
    }

}
