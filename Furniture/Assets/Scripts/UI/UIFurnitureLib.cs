/****************************************************************************
 * 2018.9 DESKTOP-S5L3H66
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace QFramework.Example
{
	public class UIFurnitureLibData : UIPanelData
	{
		// TODO: Query Mgr's Data
	}

    public partial class UIFurnitureLib : UIPanel
    {
        private Dropdown mLibDrd;

        private List<LibOptionInfo> mLibOpList = new List<LibOptionInfo>();

        private int mCurrentDropValue;

        protected override void InitUI(IUIData uiData = null)
        {
            mLibDrd = LibDrd.GetComponent<Dropdown>();

            mData = uiData as UIFurnitureLibData ?? new UIFurnitureLibData();

            mCurrentDropValue = mLibDrd.value;

            mLibDrd.options.Clear();
            InitOpInfos();
        }

        protected override void ProcessMsg(int eventId, QMsg msg)
        {
            throw new System.NotImplementedException();
        }

        protected override void RegisterUIEvent()
        {
            //返回按钮事件
            ReturnBtn.onClick.AddListener(() =>
            {
                UIMgr.OpenPanel<UIMainMenu>();
                CloseSelf();
            });

            mLibDrd = LibDrd.GetComponent<Dropdown>();
            EventTrigger trigger = mLibDrd.gameObject.AddComponent<EventTrigger>();
            UnityAction<BaseEventData> action = new UnityAction<BaseEventData>(OnSelectDelegate);
            EventTrigger.Entry entry = new EventTrigger.Entry();

            entry.eventID = EventTriggerType.Select;
            entry.callback.AddListener(action);
            trigger.triggers.Add(entry);
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
            Debug.Log("[ UIFurnitureLib:]" + content);
        }

        void InitOpInfos()
        {
            mLibOpList.Add(new LibOptionInfo() { Nmae = "桌子", BgSprite = null });
            mLibOpList.Add(new LibOptionInfo() { Nmae = "椅子", BgSprite = null });
            mLibOpList.Add(new LibOptionInfo() { Nmae = "床", BgSprite = null });
            mLibOpList.Add(new LibOptionInfo() { Nmae = "柜子", BgSprite = null });

            Dropdown.OptionData tempOpData;

            for (int i = 0; i < mLibOpList.Count; i++)
            {
                tempOpData = new Dropdown.OptionData();
                Debug.Log(mLibOpList[i].Nmae);
                tempOpData.text = mLibOpList[i].Nmae;
                tempOpData.image = mLibOpList[i].BgSprite;
                mLibDrd.options.Add(tempOpData);
            }

            mLibDrd.captionText.text = mLibOpList[0].Nmae;
        }

        void JumpToSelectLib(int mLibID)
        {
            UIMgr.OpenPanel<UIPopUpBox>(UILevel.Common, new UIPopUpBoxData()
            {
                BtnTexts = new string[] { "OK"},
                TitleText = "家具展示："+ mLibOpList[mLibID].Nmae,
                HintText = "暂时还未添加，敬请期待。。。",
                ShowBg = true
            });

            Debug.Log(mLibOpList[mLibID].Nmae);
        }

        public void OnSelectDelegate(BaseEventData data)
        {
            if (mLibDrd.value != mCurrentDropValue)
            {
                JumpToSelectLib(mLibDrd.value);
                mCurrentDropValue = mLibDrd.value;
            }    
        }
    }

    public class LibOptionInfo
    {
        public string Nmae;
        public Sprite BgSprite = null;
    }
}