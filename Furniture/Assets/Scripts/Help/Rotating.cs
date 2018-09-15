using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotating : MonoBehaviour {

    private bool isRota;

    public bool IsRota
    {
        get { return isRota; }
    }

	// Update is called once per frame
	void Update () {
        if (IsRota)
        {
           transform.Rotate(Vector3.up * 25 * Time.deltaTime, Space.Self);
        }     
    }

    public void ActiveRota()
    {
        isRota = true;
    }

    public void InactiveRota()
    {
        isRota = false;
    }
}
