using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DaemonTools;

/// <summary>
/// 所有的UI都需要继承自UIBase；
/// isfirstOpen将会传输是否第一次开启该UI，通常用于订阅事件。
/// 需要用到uimanager的场景需要布置一个canvas到场景中这个canvas的Tag必须为UIManager
/// </summary>
public class ExamplePanel : UIBase
{


    [SerializeField] Button m_reload;
    public override void close()
    {
        print("close");
    }
    //当UIManager.Instance时被
    public override void show(bool isfirstOpen, object[] value)
    {
        if (isfirstOpen)
        {
            print("firstOpen");
            m_reload.onClick.AddListener(OnReload);
        }
        print("show");
    }


    public void OnReload()
    {
        LoadSceneManager.Instance.LoadSceneAsync("ExampleScene");
    }

    //在ui中也可以调用unity的Update函数
    void Update()
    {

    }
}
