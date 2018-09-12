using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

[MonoSingletonPath("[Entrance]/AppMgr")]
public class AppMgr : MonoBehaviour,ISingleton {
    public static AppMgr Instance
    {
        get { return MonoSingletonProperty<AppMgr>.Instance; }
    }

    public void OnSingletonInit() { }

    void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    void Start()
    {
        ResMgr.Init();

        //添加启动模块
        //gameObject.AddComponent<StartProcessModule>();
    }
}
