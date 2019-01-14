using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIPanel : UIBase
{
    
    [SerializeField] PlayerUI m_playerUIPrefab;
    List<PlayerUI> playerUIs = new List<PlayerUI>();
    [SerializeField] Transform m_playersLayout;

    public override void close()
    {

    }

    public override void show(bool isfirstOpen, object[] value)
    {
        if (isfirstOpen)
        {
            List<MPlayerController> players = value[0] as List<MPlayerController>;
            InitPlayerUI(players);
        }
    }

    void InitPlayerUI(List<MPlayerController>  players)
    {
        
        for (int i = 0; i < playerUIs.Count; i++)
        {
            Destroy(playerUIs[i].gameObject);
        }
        playerUIs.Clear();

        for (int i = 0; i < players.Count; i++)
        {
            PlayerUI playerUI = Instantiate(m_playerUIPrefab);
            playerUI.transform.SetParent(m_playersLayout, false);
            playerUI.Player = players[i];
            playerUIs.Add(playerUI);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
