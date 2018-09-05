﻿using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QAssetBundle;

public class InDoorSceneCtrl : MonoBehaviour {

    #region Private Variables

    private ResLoader mResLoader = null;
    private bool mEnableSwipe = false;
    private bool mLoadingFinish = false;
    private float mDuration = 0.5f;

    private List<InDoorInfo> mOrderedInDoorInfos;

    private InDoorElement mCurElement;
    private InDoorElement mBeforeElement;

    #endregion

    #region Properties

    protected ResLoader ResLoader
    {
        set { mResLoader = value; }
    }

    public bool EnableSwipe
    {
        set
        {
            if (mLoadingFinish) mEnableSwipe = value;
        }
        get { return mEnableSwipe; }
    }

    #endregion

    /// <summary>
    /// 加载场景包括资源
    /// </summary>
    /// <returns></returns>
    public static InDoorSceneCtrl Load()
    {
        ResLoader resLoader = ResLoader.Allocate();
        var prefab = resLoader.LoadSync<GameObject>(Indoorsceneprefab.BundleName, Indoorsceneprefab.INDOORSCENE);
        var inDoorSceneCtrl = Instantiate(prefab).AddComponent<InDoorSceneCtrl>();
        inDoorSceneCtrl.ResLoader = resLoader;
        inDoorSceneCtrl.Init();
        return inDoorSceneCtrl;
    }

    public void Init()
    {
        transform.LocalIdentity();
        gameObject.SetActive(true);
    }

    public void UnLoad()
    {
        mResLoader.Recycle2Cache();
        mResLoader = null;
        StopAllCoroutines();
        mOrderedInDoorInfos.Clear();
        Destroy(gameObject);
    }

    public void LoadScenes(List<InDoorInfo> orderedSkuActiveInfos, int selectedIndex)
    {
        mOrderedInDoorInfos = orderedSkuActiveInfos;
        mCurElement = InDoorPool.Instance.AddInDoor(orderedSkuActiveInfos[selectedIndex]);
        if (mCurElement != null)
        {
            mCurElement.transform.SetParent(this.transform);
        }
        mCurElement.CustomShow();

        StartCoroutine(LoadSceneConten(selectedIndex));
    }

    IEnumerator LoadSceneConten(int selectedIndex)
    {
        //yield return null;
        for (int i = 0; i < mOrderedInDoorInfos.Count; ++i)
        {
            if (i != selectedIndex)
            {
                yield return null;
                InDoorElement inDoorElement = InDoorPool.Instance.AddInDoor(mOrderedInDoorInfos[i]);
                if (inDoorElement != null)
                {
                    inDoorElement.transform.SetParent(this.transform);
                    inDoorElement.gameObject.Hide();
                }
            }
        }
        mLoadingFinish = true;
        mEnableSwipe = true;
       //SendEvent(UISKUMenuEvent.CarLoadFinish);
    }

    public void MoveNext(int index)
    {
        mEnableSwipe = false;
        mBeforeElement = mCurElement;

        mCurElement = InDoorPool.Instance.GetSceneWithInDoor(mOrderedInDoorInfos[index]);
        mCurElement.GetComponent<OnShowCtrl>().OnShowRight(mDuration);

        mBeforeElement.GetComponent<OnHideCtrl>().OnHideRight(mDuration);
    }

    public void MovePrevious(int index)
    {
        mEnableSwipe = false;
        mBeforeElement = mCurElement;

        mCurElement = InDoorPool.Instance.GetSceneWithInDoor(mOrderedInDoorInfos[index]);
        mCurElement.GetComponent<OnShowCtrl>().OnShowLeft(mDuration);

        mBeforeElement.GetComponent<OnHideCtrl>().OnHideLeft(mDuration);
    }

    void ChangeColor(int index, bool isFade = true)
    {
        
    }

    void OnDestroy()
    {
        UnLoad();
        InDoorPool.Instance.Destroy();
    }
}