using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePanel : UIBase
{
    public Text Dialogue;
    public override void close()
    {
        if (FindObjectOfType<PlayerMovement>())
        {
            PlayerMovement player = FindObjectOfType<PlayerMovement>();
            player.IsLock = false;
        }
    }
    public override void show(bool IsfirstOpen, object[] value)
    {
        if (FindObjectOfType<PlayerMovement>())
        {
            PlayerMovement player = FindObjectOfType<PlayerMovement>();
            player.IsLock = true;

        }
        Dialogue.text = (string)value[0];

    }
}
