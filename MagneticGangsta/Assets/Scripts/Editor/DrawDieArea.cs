using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(PlayerReborn))]
public class DrawDieArea : Editor
{
    private void OnSceneGUI()
    {
        PlayerReborn playerReborn = target as PlayerReborn;
        if (playerReborn == null)
        {
            return;
        }
        Vector2 leftTop = new Vector2(playerReborn.LeftEdge, playerReborn.TopEdge);
        Vector2 rightTop = new Vector2(playerReborn.RightEdge, playerReborn.TopEdge);
        Vector2 leftButton = new Vector2(playerReborn.LeftEdge, playerReborn.ButtonEdge);
        Vector2 rightButton = new Vector2(playerReborn.RightEdge, playerReborn.ButtonEdge);

        Handles.DrawLine(leftTop, rightTop);
        Handles.DrawLine(rightTop, rightButton);
        Handles.DrawLine(rightButton, leftButton);
        Handles.DrawLine(leftTop, leftButton);
    }



}
