using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppLoader : MonoBehaviour {

    [SerializeField] UICtrl mUICtrl;
    private void Awake()
    {
        Log.I("Init[{0}]", AppMgr.Instance.GetType().Name);
    }

    private void Start()
    {
        Input.multiTouchEnabled = false;
        mUICtrl.gameObject.Show();
        Destroy(gameObject);
    }
}
