using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QAssetBundle;
using QFramework.Example;
using QAssetBundle;

public class FurnitureLibCtrl : MonoSingleton<FurnitureLibCtrl>
{

    #region Private Variables

    private ResLoader mResLoader = null;
    private bool mEnableSwipe = false;
    private bool mLoadingFinish = false;
    private float mDuration = 0.5f;

    private Dictionary<string, GameObject> mOrderedFurLibDict = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> mOrderedTypeNameDict = new Dictionary<string, GameObject>();

    private GameObject mCurElement;

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

    public int OrderedFurLibNum
    {
        get { return mOrderedFurLibDict.Count; }
    }

    #endregion

    /// <summary>
    /// 加载场景包括资源
    /// </summary>
    /// <returns></returns>

    public override void OnSingletonInit()
    {
        ResLoader resLoader = ResLoader.Allocate();

        var furnitureLibCtrl = gameObject.GetComponent<FurnitureLibCtrl>();
        furnitureLibCtrl.ResLoader = resLoader;
        furnitureLibCtrl.Init();

        Debug.Log(name + ":" + "OnSingletonInit");
    }

    private void Init()
    {
        transform.LocalIdentity();

        transform.localPosition = new Vector3(11f, -109f, 760f);
        gameObject.SetActive(true);
    }

    public void UnLoad()
    {
        mResLoader.Recycle2Cache();
        mResLoader = null;
        StopAllCoroutines();

        mOrderedFurLibDict.Clear();
        mOrderedTypeNameDict.Clear();
        Destroy(gameObject);
    }

    public void LoadFurniture(string furTypeName, string furName)
    {
        GameObject furTypeGO;

        if (!mOrderedTypeNameDict.ContainsKey(furTypeName))
        {
            furTypeGO = new GameObject(furTypeName);
            furTypeGO.transform.SetParent(transform);
            furTypeGO.transform.localPosition = Vector3.zero;
            mOrderedTypeNameDict.Add(furTypeName, furTypeGO);
        }
        Debug.Log(furTypeName + "   " + furName);

        if (!mOrderedFurLibDict.ContainsKey(furName))
        {
            var furGO = Instantiate(mResLoader.LoadSync<GameObject>(furTypeName + "pre", furName + "_pre")) as GameObject;
            furGO.transform.SetParent(mOrderedTypeNameDict[furTypeName].transform);
            furGO.transform.localPosition = Vector3.zero;
            furGO.transform.localScale = new Vector3(300f, 300f, 300f);
            mOrderedFurLibDict.Add(furName,furGO);
        }

        if (mCurElement != null)
        {
            mCurElement.GetComponent<Rotating>().InactiveRota();
            mCurElement.Hide();
        } 

        mCurElement = mOrderedFurLibDict[furName];

        mCurElement.Show();
        mCurElement.GetComponent<Rotating>().ActiveRota();
    }

    public void ChangeMat(string matName)
    {
        Material mat = mCurElement.GetComponent<FurnitureElement>().mMatsDict[matName];

        Renderer[] rds = mCurElement.transform.GetComponentsInChildren<Renderer>();
        //逐一遍历他的子物体中的Renderer
        for (int i = 0; i < rds.Length; i++)
        {
            rds[i].material = mat;
        }
    }

    public Sprite LoadBgByName(string bundleName,string assetName)
    {
        return mResLoader.LoadSprite(bundleName, assetName);
    }
}
