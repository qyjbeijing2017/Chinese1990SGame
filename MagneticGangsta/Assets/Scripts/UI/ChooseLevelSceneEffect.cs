using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DaemonTools;

public class ChooseLevelSceneEffect : MonoBehaviour
{
    [SerializeField] Button Level1;
    [SerializeField] Button Level2;
    [SerializeField] Button Level3;

    // Start is called before the first frame update
    void Start()
    {
        Level1.onClick.AddListener(OnChooseLevel1);
    }

    void OnChooseLevel1()
    {
        LoadSceneManager.Instance.LoadSceneAsync("Level1Scene");
    }
    // Update is called once per frame
    void Update()
    {

    }
}
