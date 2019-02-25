using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaemonTools;
using UnityEngine.UI;

public class StartLogic : MonoBehaviour
{

    [SerializeField] GameObject pressAnyStart;
    [SerializeField] Button start;
    [SerializeField] Button about;
    [SerializeField] Button quit;

    // Start is called before the first frame update
    void Start()
    {
        quit.onClick.AddListener(OnQuit);
        start.onClick.AddListener(OnStart);
        about.onClick.AddListener(OnAbout);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && pressAnyStart.activeSelf)
        {
            pressAnyStart.SetActive(false);
            start.gameObject.SetActive(true);
            about.gameObject.SetActive(true);
            quit.gameObject.SetActive(true);
        }
    }


    public void OnStart()
    {
        print("start");
    }

    public void OnAbout()
    {
        print("about");
    }
    public void OnQuit()
    {
        print("quit");
    }
}
