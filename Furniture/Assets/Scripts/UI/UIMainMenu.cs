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

		protected override void ProcessMsg (int eventId,QMsg msg)
		{
			throw new System.NotImplementedException ();
		}

		protected override void RegisterUIEvent()
		{
            //进入场景按钮事件
            ToDoorBtn.onClick.AddListener(() =>
            {
                UIMgr.OpenPanel<UIDoor>();
                CloseSelf();
            });

            //进入家具库按钮事件
            ToLibrBtn.onClick.AddListener(() =>
            {
                UIMgr.OpenPanel<UIFurnitureLib>();
                CloseSelf();
            });
        }

		protected override void OnShow()
		{
			base.OnShow();
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