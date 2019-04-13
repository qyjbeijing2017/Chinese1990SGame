using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(MeteoriteShooter))]
public class MeteoriteShooterPosition : Editor
{

    private void OnSceneGUI()
    {
        MeteoriteShooter meteoriteShooter = target as MeteoriteShooter;

        Handles.DrawLine(new Vector3(meteoriteShooter.ShootLeft, meteoriteShooter.ShootHeight,0),
                         new Vector3(meteoriteShooter.ShootRight, meteoriteShooter.ShootHeight, 0));
    }
}
