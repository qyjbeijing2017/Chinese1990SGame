using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : MonoBehaviour {




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<MPlayerController>())
        {
            MPlayerController player = collision.GetComponent<MPlayerController>();
            player.PlayerHP--;
            if (!player.IsDead)
            {
                player.gameObject.SetActive(false);
                MagnetLevelControl.Instance.OnReborn(player);
            }
        }
    }
}
