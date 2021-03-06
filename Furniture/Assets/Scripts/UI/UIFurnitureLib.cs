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

        private List<string> mDropDownNamesList = new List<string>();

        private Dictionary<string, LibOptionInfo> mLibOpDict = new Dictionary<string, LibOptionInfo>();

        private GameObject mConfigBtn_Pre;
        private GameObject mConfigGrid_Pre;

        private int mCurrentDropValue;

        protected override void InitUI(IUIData uiData = null)
        {
            mLibDrd = LibDrd.GetComponent<Dropdown>();

            mConfigBtn_Pre = transform.Find("ConfigBtn_Pre").gameObject;
            mConfigGrid_Pre = transform.Find("ConfigGrid_Pre").gameObject;

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
            Dropdown.OptionData tempOpData;

            List<BtnTemp> btnTemps = new List<BtnTemp>(new BtnTemp[]
                {
                    new BtnTemp(){Name = "111",Bg = null},
                    new BtnTemp(){Name = "222",Bg = null},
                    new BtnTemp(){Name = "333",Bg = null},
                }
            );        

            AddFurniturelStyle("柜子", new List<FurniturelTemp>(new FurniturelTemp[]
                {
                    new FurniturelTemp(){ TyepName = "材质",ChildBtnsList = btnTemps},
                    new FurniturelTemp(){ TyepName = "产品",ChildBtnsList = btnTemps}
                }
            ));

            AddFurniturelStyle("床", new List<FurniturelTemp>(new FurniturelTemp[]
                {
                    new FurniturelTemp(){ TyepName = "材质",ChildBtnsList = btnTemps},
                    new FurniturelTemp(){ TyepName = "产品",ChildBtnsList = btnTemps}
                }
            ));

            AddFurniturelStyle("椅子", new List<FurniturelTemp>(new FurniturelTemp[]
                {
                    new FurniturelTemp(){ TyepName = "材质",ChildBtnsList = btnTemps},
                    new FurniturelTemp(){ TyepName = "产品",ChildBtnsList = btnTemps}
                }
            ));

            AddFurniturelStyle("沙发", new List<FurniturelTemp>(new FurniturelTemp[]
                {
                    new FurniturelTemp(){ TyepName = "材质",ChildBtnsList = btnTemps},
                    new FurniturelTemp(){ TyepName = "产品",ChildBtnsList = btnTemps}
                }
            ));

            foreach (KeyValuePair<string, LibOptionInfo> kvp in mLibOpDict)
            {
                tempOpData = new Dropdown.OptionData();

                tempOpData.text = kvp.Key;                 
                tempOpData.image = kvp.Value.BgSprite;

                mDropDownNamesList.Add(kvp.Key);

                mLibDrd.options.Add(tempOpData);
            }

            mLibDrd.captionText.text = mDropDownNamesList[0];

            InitScrollView(mLibOpDict[mDropDownNamesList[mLibDrd.value]]);
        }

        void JumpToSelectLib(int mLibID)
        {
            ConfigScrollView.gameObject.Show();

            Debug.Log(mDropDownNamesList[mLibID]);
        }

        public void OnSelectDelegate(BaseEventData data)
        {
            if (mLibDrd.value != mCurrentDropValue)
            {
                InitScrollView(mLibOpDict[mDropDownNamesList[mLibDrd.value]]);
                JumpToSelectLib(mLibDrd.value);
                mCurrentDropValue = mLibDrd.value;
            }
        }

        private void AddFurniturelStyle(string typeName, List<FurniturelTemp> furniturelTemp, Sprite typeBg = null)
        {
            LibOptionInfo mLibOptionInfo = new LibOptionInfo();
            mLibOptionInfo.TypeNmae = typeName;
            mLibOptionInfo.BgSprite = typeBg;
            mLibOptionInfo.FurniturelTempList = furniturelTemp;

            if (mLibOpDict.ContainsKey(typeName))
            {
                mLibOpDict[typeName] = mLibOptionInfo;
            }
            else
            {
                mLibOpDict.Add(typeName, mLibOptionInfo);
            }
        }

        private void InitScrollView(LibOptionInfo initLibOptionInfo)
        {
            int childCount = ConfigScrollView.content.childCount;
            
            for (int i = 0; i < childCount; i++)
            {             
                Destroy(ConfigScrollView.content.GetChild(i).gameObject);
            }

            for (int i = 0; i < initLibOptionInfo.FurniturelTempList.Count; i++)
            {
                var configGrid_PreObj = Instantiate(mConfigGrid_Pre);
                configGrid_PreObj.SetActive(true);

                configGrid_PreObj.transform.SetParent(ConfigScrollView.content);
                configGrid_PreObj.transform.localPosition = Vector3.zero;
                configGrid_PreObj.transform.localScale = Vector3.one;
                configGrid_PreObj.transform.FindChild("ConfigName").GetComponent<Text>().text = initLibOptionInfo.FurniturelTempList[i].TyepName;

                for (int j = 0; j < initLibOptionInfo.FurniturelTempList[i].ChildBtnsList.Count; j++)
                {
                    var ConfigBtn_PreObj = Instantiate(mConfigBtn_Pre);

                    ConfigBtn_PreObj.SetActive(true);

                    ConfigBtn_PreObj.transform.SetParent(configGrid_PreObj.transform);
                    ConfigBtn_PreObj.transform.localPosition = Vector3.zero;
                    ConfigBtn_PreObj.transform.localScale = Vector3.one;
                    ConfigBtn_PreObj.transform.FindChild("BtnName").GetComponent<Text>().text = initLibOptionInfo.FurniturelTempList[i].ChildBtnsList[j].Name;

                    ConfigBtn_PreObj.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        //弹出框提示
                        UIMgr.OpenPanel<UIPopUpBox>(UILevel.Common, new UIPopUpBoxData()
                        {
                            BtnTexts = new string[] { "OK" },
                            TitleText = "家具展示：" + mDropDownNamesList[mCurrentDropValue],
                            HintText = "暂时还未添加，敬请期待。。。",
                            ShowBg = true
                        });
                    });
                }
            }

            ConfigScrollView.gameObject.Show();
        }
    }

}