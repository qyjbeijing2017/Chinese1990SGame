using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TestStart : MonoBehaviour {
	[SerializeField]Button m_startGame;
	

	// Use this for initialization
	void Start () {
		m_startGame.onClick.AddListener(OnStartGameClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnStartGameClick(){
        DaemonTools.LoadSceneManager.Instance.LoadSceneAsync("GameStage1");
        //SceneManager.LoadScene("GameStage1");
	}
}
