using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using QAssetBundle;

public class InDoorPool
{
    private static InDoorPool mInstance;

    public static InDoorPool Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = new InDoorPool();
            }
            return mInstance;
        }
    }

    private ResLoader mResLoader = null;

    public ResLoader ResLoader
    {

        set { mResLoader = value; }
        get { return mResLoader; }
    }

    public InDoorPool()
    {
        ResLoader = ResLoader.Allocate();
    }

    public void Destroy()
    {
        mResLoader.Recycle2Cache();
        mResLoader = null;
        mInstance = null;
    }

    public Dictionary<string, InDoorType> inDoorTypesDict = new Dictionary<string, InDoorType>();

    public InDoorElement AddInDoor(InDoorInfo activeInfo)
    {
        if (!inDoorTypesDict.ContainsKey(activeInfo.name))
        {
            InDoorType inDoorType = new InDoorType();
            inDoorType.Types = activeInfo.name;
            inDoorTypesDict.Add(activeInfo.name, inDoorType);
            return inDoorType.AddSceneByType(activeInfo);
        }
        else
        {
            InDoorType skuSceneType = inDoorTypesDict[activeInfo.name];
            if (skuSceneType.DoorElementCount < 3)
            {
                return skuSceneType.AddSceneByType(activeInfo);
            }
        }
        return null;
    }

    public void RecycleScene(InDoorElement inDoorElement)
    {
        string key = inDoorElement.ElementInDoorInfo.name;
        if (inDoorTypesDict.ContainsKey(key))
        {
            inDoorTypesDict[key].RecycleSceneElement(inDoorElement);
        }
    }

    public InDoorElement GetSceneWithInDoor(InDoorInfo activeInfo)
    {
        if (inDoorTypesDict.ContainsKey(activeInfo.name))
        {
            return inDoorTypesDict[activeInfo.name].GetInDoorElement(activeInfo);
        }
        else
        {
            //此处为处理特殊超出缓存情况，一般情况下应该不会执行到。
            AddInDoor(activeInfo);
        }
        return null;
    }
}



public class InDoorType
{
    public string Types;
    private List<InDoorElement> inDoorElements = new List<InDoorElement>();

    public int DoorElementCount
    {

        get { return inDoorElements.Count; }
    }

    public InDoorElement AddSceneByType(InDoorInfo activeInfo)
    {
        var currentInDoorPrefab =
            InDoorPool.Instance.ResLoader.LoadSync<GameObject>(Indoorsceneprefab.BundleName, activeInfo.name);
        GameObject sceneObj = GameObject.Instantiate(currentInDoorPrefab);
        InDoorElement skuSceneElement = sceneObj.AddComponent<InDoorElement>();
        skuSceneElement.ElementInDoorInfo = activeInfo;

        inDoorElements.Add(skuSceneElement);
        return skuSceneElement;
    }

    public void RecycleSceneElement(InDoorElement sceneElement)
    {
        for (int i = inDoorElements.Count - 1; i >= 0; i--)
        {
            if (inDoorElements[i].ElementInDoorInfo == sceneElement.ElementInDoorInfo)
            {
                return;
            }
        }
        inDoorElements.Add(sceneElement);
    }

    public InDoorElement GetInDoorElement(InDoorInfo activeInfo)
    {
        bool hasFind = false;
        for (int i = inDoorElements.Count - 1; i >= 0; i--)
        {
            if (inDoorElements[i].ElementInDoorInfo == activeInfo)
            {
                InDoorElement sceneElement = inDoorElements[i];
                inDoorElements.RemoveAt(i);
                hasFind = true;
                return sceneElement;
            }
        }
        if (!hasFind)
        {
            InDoorElement sceneElement = inDoorElements[0];
            sceneElement.ElementInDoorInfo = activeInfo;
            inDoorElements.RemoveAt(0);
            return sceneElement;
        }
        return null;
    }

}

public class InDoorInfo
{
    public int id;
    public string name;
}
