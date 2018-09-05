using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

namespace QFramework.Example
{
    public class UICtrl : MonoBehaviour
    {
        private LoadingCanvas mLoadingCanvas;

        private List<InDoorInfo> mInDoorList = new List<InDoorInfo>();
        private InDoorSceneCtrl mInDoorSceneCtrl;
        // Use this for initialization
        void Start()
        {
            mLoadingCanvas = GameObject.Find("LoadingCanvas").GetComponent<LoadingCanvas>();

            OpenUIMain();

            InDoorInfo inDoorInfoTemp = new InDoorInfo()
            { id = 0, name = "InDoorOne" };

            mInDoorList.Add(inDoorInfoTemp);

            mInDoorSceneCtrl = InDoorSceneCtrl.Load();
            mInDoorSceneCtrl.LoadScenes(mInDoorList, 0);
        }

        private void OpenUIMain()
        {
            if (mLoadingCanvas.gameObject)
            {
                Destroy(mLoadingCanvas.gameObject);
            }

            UIMgr.SetResolution(1280, 720, 0);

            UIMgr.OpenPanel<UIMainMenu>();
        }
    }
}
