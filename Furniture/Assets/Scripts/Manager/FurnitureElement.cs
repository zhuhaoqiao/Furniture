using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureElement : MonoBehaviour {

    public Material[] mSetMats;

    public Dictionary<string, Material> mMatsDict = new Dictionary<string, Material>();

    private void Start()
    {
        for (int i = 0; i < mSetMats.Length; i++)
        {
            mMatsDict.Add(mSetMats[i].name, mSetMats[i]);
        }
    }
}
