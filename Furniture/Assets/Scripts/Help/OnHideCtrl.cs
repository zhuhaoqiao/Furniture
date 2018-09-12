using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHideCtrl : MonoBehaviour
{
    private Vector3 FromPos = Vector3.zero;

    public void OnHideLeft(float duration, Action onComplete = null)
    {
        transform.localPosition = FromPos;
        this.gameObject.GetComponent<InDoorElement>().CustomHide();
    }

    public void OnHideRight(float duration, Action onComplete = null)
    {
        transform.localPosition = FromPos;
        this.gameObject.GetComponent<InDoorElement>().CustomHide();
    }
}
