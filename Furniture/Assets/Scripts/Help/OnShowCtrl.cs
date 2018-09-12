using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnShowCtrl : MonoBehaviour
{
    private Vector3 ToPos = Vector3.zero;

    public void OnShowLeft(float duration, Action onComplete = null)
    {
        this.gameObject.GetComponent<InDoorElement>().CustomShow();
    }

    public void OnShowRight(float duration, Action onComplete = null)
    {
        this.gameObject.GetComponent<InDoorElement>().CustomShow();
    }
}
