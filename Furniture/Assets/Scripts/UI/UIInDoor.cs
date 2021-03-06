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
	public class UIInDoorData : UIPanelData
	{
		// TODO: Query Mgr's Data
	}

	public partial class UIInDoor : UIPanel
	{
        private int mCurInDoor;

		protected override void InitUI(IUIData uiData = null)
		{
			mData = uiData as UIInDoorData ?? new UIInDoorData();
            //please add init code here
        }

		protected override void ProcessMsg (int eventId,QMsg msg)
		{
			throw new System.NotImplementedException ();
		}

		protected override void RegisterUIEvent()
		{
            //场景切换按钮事件
            DoorSwitchBtn.onClick.AddListener(() =>
            {
                mCurInDoor++;
                if (mCurInDoor == InDoorSceneCtrl.Instance.OrderedInDoorNum) mCurInDoor = 0;

                InDoorSceneCtrl.Instance.MoveNext(mCurInDoor);
            });

            //时空切换按钮事件
            TimeAndSpaceSwitchBtn.onClick.AddListener(() =>
            {
                UIMgr.OpenPanel<UIPopUpBox>(UILevel.Common, new UIPopUpBoxData()
                {
                    BtnTexts = new string[] { "OK" },
                    TitleText = "提示",
                    HintText = "此功能暂未开通，敬请期待。。。",
                    ShowBg = true
                });
            });

            //一键摆放按钮事件
            AutoPutBtn.onClick.AddListener(() =>
            {
                UIMgr.OpenPanel<UIPopUpBox>(UILevel.Common, new UIPopUpBoxData()
                {
                    BtnTexts = new string[] { "OK" },
                    TitleText = "提示",
                    HintText = "此功能暂未开通，敬请期待。。。",
                    ShowBg = true
                });
            });

            //返回按钮事件
            ReturnBtn.onClick.AddListener(() =>
            {
                UIMgr.OpenPanel<UIMainMenu>();
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
			Debug.Log("[ UIInDoor:]" + content);
		}
	}
}