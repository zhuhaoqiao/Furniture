using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

public class InDoorElement : MonoBehaviour {

    private InDoorInfo inDoorInfoData;
    public InDoorInfo ElementInDoorInfo
    {

        get { return inDoorInfoData; }
        set
        {
            if (inDoorInfoData != value)
            {
                inDoorInfoData = value;
            }
        }
    }
    public void CustomShow()
    {
        this.transform.localPosition = Vector3.zero;
        this.gameObject.Show();
        InDoorPool.Instance.RecycleScene(this);
    }

    public void CustomHide()
    {
        InDoorPool.Instance.RecycleScene(this);
    }
}
