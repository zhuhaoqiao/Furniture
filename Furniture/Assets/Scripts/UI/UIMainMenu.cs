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
            //���볡����ť�¼�
            ToDoorBtn.onClick.AddListener(() =>
            {
                //   Debug.Log("indoor!!!!!!");
                UIMgr.OpenPanel<UIInDoor>();
                CloseSelf();
            });

            //����Ҿ߿ⰴť�¼�
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

        protected override void OnHide()
        {
            base.OnHide();
        }

        void Update()
        {
            Debug.Log(transform.localPosition + "  " + transform.position);
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