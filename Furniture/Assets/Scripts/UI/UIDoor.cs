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
	public class UIDoorData : UIPanelData
	{
		// TODO: Query Mgr's Data
	}

	public partial class UIDoor : UIPanel
	{
		protected override void InitUI(IUIData uiData = null)
		{
			mData = uiData as UIDoorData ?? new UIDoorData();
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
               
            });

            //时空切换按钮事件
            TimeAndSpaceSwitchBtn.onClick.AddListener(() =>
            {
                
            });

            //一键摆放按钮事件
            AutoPutBtn.onClick.AddListener(() =>
            {

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
			Debug.Log("[ UIDoor:]" + content);
		}
	}
}