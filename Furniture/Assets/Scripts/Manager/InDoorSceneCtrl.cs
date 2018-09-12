using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QAssetBundle;
using QFramework.Example;

public class InDoorSceneCtrl : MonoSingleton<InDoorSceneCtrl> {

    #region Private Variables

    private ResLoader mResLoader = null;
    private bool mEnableSwipe = false;
    private bool mLoadingFinish = false;
    private float mDuration = 0.5f;

    private List<InDoorInfo> mOrderedInDoorInfos = new List<InDoorInfo>();

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

    public int OrderedInDoorNum
    {
        get { return mOrderedInDoorInfos.Count; }
    }

    #endregion

    /// <summary>
    /// 加载场景包括资源
    /// </summary>
    /// <returns></returns>

    public override void OnSingletonInit()
    {
        ResLoader resLoader = ResLoader.Allocate();
 
        var inDoorSceneCtrl = gameObject.GetComponent<InDoorSceneCtrl>();
        inDoorSceneCtrl.ResLoader = resLoader;
        inDoorSceneCtrl.Init();

        Debug.Log(name + ":" + "OnSingletonInit");
    }

    private void Init()
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

    public void LoadScenes(List<InDoorInfo> orderedInDoorInfos, int selectedIndex ,bool isShow = false)
    {
        mOrderedInDoorInfos = orderedInDoorInfos;
        mCurElement = InDoorPool.Instance.AddInDoor(orderedInDoorInfos[selectedIndex]);

        mCurElement.gameObject.SetActive(isShow);

        if (mCurElement != null)
        {
            mCurElement.transform.SetParent(this.transform);
        }

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

        Debug.Log(mBeforeElement.ElementInDoorInfo.id + "---------------" + mCurElement.ElementInDoorInfo.id);
    }

    public void MovePrevious(int index)
    {
        mEnableSwipe = false;
        mBeforeElement = mCurElement;

        mCurElement = InDoorPool.Instance.GetSceneWithInDoor(mOrderedInDoorInfos[index]);
        mCurElement.GetComponent<OnShowCtrl>().OnShowLeft(mDuration);

        mBeforeElement.GetComponent<OnHideCtrl>().OnHideLeft(mDuration);
    }

    public void ShowScene()
    {
        mCurElement.gameObject.Show();
    }

    public void HideScene()
    {
        mCurElement.gameObject.Hide();
    }

    void ChangeColor(int index, bool isFade = true)
    {
        
    }

    private void OnDestroy()
    {
       
    }
}
