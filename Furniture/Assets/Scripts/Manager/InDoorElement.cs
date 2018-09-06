using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using QFramework.Example;

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
        this.gameObject.Hide();
        InDoorPool.Instance.RecycleScene(this);
    }
}
