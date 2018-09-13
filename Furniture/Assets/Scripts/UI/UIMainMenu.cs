/****************************************************************************
 * 2018.9 DESKTOP-S5L3H66
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
    public class UIMainMenuData : UIPanelData
    {
        // TODO: Query Mgr's Data
    }

    public partial class UIMainMenu : UIPanel
    {
        protected override void InitUI(IUIData uiData = null)
        {
            mData = uiData as UIMainMenuData ?? new UIMainMenuData();
            //please add init code here
        }

        protected override void ProcessMsg(int eventId, QMsg msg)
        {
            throw new System.NotImplementedException();
        }

        protected override void RegisterUIEvent()
        {
            //进入场景按钮事件
            ToDoorBtn.onClick.AddListener(() =>
            {
                //   Debug.Log("indoor!!!!!!");
                UIMgr.OpenPanel<UIInDoor>(UILevel.Common, new UIInDoorData()
                {
                    UIPos = new Vector3(0.1749878f, -1.272003f, -879.746f) ,
                    UIScale = new Vector3(0.005f, 0.005f, 0.005f),
                    UIRotation = new Vector3(0, 3.84f, -33.33f)
                });
                CloseSelf();
            });

            //进入家具库按钮事件
            ToLibrBtn.onClick.AddListener(() =>
            {
                UIMgr.OpenPanel<UIFurnitureLib>();
                CloseSelf();
            });

            ExitBtn.onClick.AddListener(() =>
            {
                Application.Quit();
            });
        }

        protected override void OnShow()
        {
            base.OnShow();

            QUIManager.Instance.SetPos(0);

            transform.localPosition = new Vector3(0f, 0f, 880f);
        }

        void Update()
        {
            Debug.Log(transform.localPosition);

           
        }

        protected override void OnHide()
        {
            base.OnHide();
        }

        protected override void OnClose()
        {
            base.OnClose();
        }

        void ShowLog(string content)
        {
            Debug.Log("[ UIMainMenu:]" + content);
        }
    }
}