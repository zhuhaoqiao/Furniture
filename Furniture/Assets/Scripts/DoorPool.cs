using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

public class DoorPool : MonoBehaviour {

    public class SkuScenePool
    {
        private static SkuScenePool mInstance;

        public static SkuScenePool Instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = new SkuScenePool();
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

        public SkuScenePool()
        {
            ResLoader = ResLoader.Allocate();
        }

        public void Destroy()
        {
            mResLoader.Recycle2Cache();
            mResLoader = null;
            mInstance = null;
        }

        public Dictionary<string, DoorType> skuSceneTypesDict = new Dictionary<string, DoorType>();

        public DoorElement AddDoor(DoorInfo activeInfo)
        {
            //if (!skuSceneTypesDict.ContainsKey(activeInfo.SceneType))
            //{
            //    SkuSceneType skuSceneType = new SkuSceneType();
            //    skuSceneType.Types = activeInfo.SceneType;
            //    skuSceneTypesDict.Add(activeInfo.SceneType, skuSceneType);
            //    return skuSceneType.AddSceneByType(activeInfo);
            //}
            //else
            //{
            //    SkuSceneType skuSceneType = skuSceneTypesDict[activeInfo.SceneType];
            //    if (skuSceneType.SceneElementCount < 3)
            //    {
            //        return skuSceneType.AddSceneByType(activeInfo);
            //    }
            //}
            //Debug.LogFormat ("Add Scene ,超过3个的时候可以执行到这里:index:{0}".ColorFormat(Color.red),activeInfo.Index);
            return null;
        }

        public void RecycleScene(DoorElement sceneElement)
        {
            //string key = sceneElement.ElementSKUActiveInfo.SceneType;
            //if (skuSceneTypesDict.ContainsKey(key))
            //{
            //    skuSceneTypesDict[key].RecycleSceneElement(sceneElement);
            //}
        }

        public DoorElement GetSceneWithCar(DoorInfo activeInfo)
        {
            //if (skuSceneTypesDict.ContainsKey(activeInfo.SceneType))
            //{
            //    return skuSceneTypesDict[activeInfo.SceneType].GetSceneElement(activeInfo);
            //}
            //else
            //{
            //    //此处为处理特殊超出缓存情况，一般情况下应该不会执行到。
            //    AddScene(activeInfo);
            //}
            return null;
        }
    }



    public class DoorType
    {
        public string Types;
        private List<DoorElement> doorElements = new List<DoorElement>();

        public int DoorElementCount
        {

            get { return doorElements.Count; }
        }

        //public DoorElement AddSceneByType(SKUInfo activeInfo)
        //{
        //    var oceanScenePrefab =
        //        SkuScenePool.Instance.ResLoader.LoadSync<GameObject>(DoorElement.BundleName, activeInfo.SceneType);
        //    GameObject sceneObj = GameObject.Instantiate(oceanScenePrefab);
        //    DoorElement skuSceneElement = sceneObj.AddComponent<DoorElement>();
        //  sceneObj.AddComponent<OnHideTween>();
        //  sceneObj.AddComponent<OnShowTween>();
        //  skuSceneElement.ElementSKUActiveInfo = activeInfo;

        //  sceneElements.Add(skuSceneElement);
        //    return skuSceneElement;
        //}

        //public void RecycleSceneElement(DoorElement sceneElement)
        //{
        //    for (int i = sceneElements.Count - 1; i >= 0; i--)
        //    {
        //        if (sceneElements[i].ElementSKUActiveInfo == sceneElement.ElementSKUActiveInfo)
        //        {
        //            return;
        //        }
        //    }
        //    sceneElements.Add(sceneElement);
        //}

        //public DoorElement GetSceneElement(SKUInfo activeInfo)
        //{
        //    bool hasFind = false;
        //    for (int i = sceneElements.Count - 1; i >= 0; i--)
        //    {
        //        if (sceneElements[i].ElementSKUActiveInfo == activeInfo)
        //        {
        //            DoorElement sceneElement = sceneElements[i];
        //            sceneElements.RemoveAt(i);
        //            hasFind = true;
        //            return sceneElement;
        //        }
        //    }
        //    if (!hasFind)
        //    {
        //        DoorElement sceneElement = sceneElements[0];
        //        sceneElement.ElementSKUActiveInfo = activeInfo;
        //        sceneElements.RemoveAt(0);
        //        return sceneElement;
        //    }
        //    return null;
        //}

    }

    public class DoorInfo()
    {
    }
}
