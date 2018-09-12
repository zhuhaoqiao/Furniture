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

            transform.SetParent(GameObject.Find("CenterEyeAnchor").transform);
            transform.localPosition = new Vector3(0f, 0f, 30f);
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
                    new BtnTemp(){Name = "jiaoji_1",Bg = null},
                    new BtnTemp(){Name = "jiaoji_2",Bg = null},
                }
            );

            List<BtnTemp> btnMatTemps = new List<BtnTemp>(new BtnTemp[]
                {
                    new BtnTemp(){Name = "111",Bg = null},
                    new BtnTemp(){Name = "222",Bg = null},
                }
            );

            AddFurniturelStyle("承具", new List<FurnitureSet>(new FurnitureSet[]
                {
                    new FurnitureSet(){ TyepName = "材质",ChildBtnsList = btnMatTemps},
                    new FurnitureSet(){ TyepName = "产品",ChildBtnsList = btnTemps}
                }),new List<string>() { "jiaoji_1" }
           );

            AddFurniturelStyle("庋具", new List<FurnitureSet>(new FurnitureSet[]
                {
                    new FurnitureSet(){ TyepName = "材质",ChildBtnsList = btnMatTemps},
                    new FurnitureSet(){ TyepName = "产品",ChildBtnsList = btnTemps}
                }), new List<string>() { "jiaoji_1" }
            );

            AddFurniturelStyle("椅子", new List<FurnitureSet>(new FurnitureSet[]
                {
                    new FurnitureSet(){ TyepName = "材质",ChildBtnsList = btnMatTemps},
                    new FurnitureSet(){ TyepName = "产品",ChildBtnsList = btnTemps}
                }), new List<string>() { "jiaoji_1" }
            );

            AddFurniturelStyle("沙发", new List<FurnitureSet>(new FurnitureSet[]
                {
                    new FurnitureSet(){ TyepName = "材质",ChildBtnsList = btnMatTemps},
                    new FurnitureSet(){ TyepName = "产品",ChildBtnsList = btnTemps}
               }), new List<string>() { "jiaoji_1" }
            );

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

            FurnitureLibCtrl.Instance.LoadFurniture(mLibOpDict[mDropDownNamesList[mLibDrd.value]].TypeNmae,
                mLibOpDict[mDropDownNamesList[mLibDrd.value]].FurnitureTempList[0]);
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

                FurnitureLibCtrl.Instance.LoadFurniture(mLibOpDict[mDropDownNamesList[mLibDrd.value]].TypeNmae,
                    mLibOpDict[mDropDownNamesList[mLibDrd.value]].FurnitureTempList[0]);
            }
        }

        private void AddFurniturelStyle(string typeName, List<FurnitureSet> furnitureSets,List<string> furnitures, Sprite typeBg = null)
        {
            LibOptionInfo mLibOptionInfo = new LibOptionInfo();
            mLibOptionInfo.TypeNmae = typeName;
            mLibOptionInfo.BgSprite = typeBg;
            mLibOptionInfo.FurnitureSetList = furnitureSets;
            mLibOptionInfo.FurnitureTempList = furnitures;

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

            for (int i = 0; i < initLibOptionInfo.FurnitureSetList.Count; i++)
            {
                var configGrid_PreObj = Instantiate(mConfigGrid_Pre);
                configGrid_PreObj.SetActive(true);

                configGrid_PreObj.transform.SetParent(ConfigScrollView.content);
                configGrid_PreObj.transform.localPosition = Vector3.zero;
                configGrid_PreObj.transform.localScale = Vector3.one;
                configGrid_PreObj.transform.Find("ConfigName").GetComponent<Text>().text = initLibOptionInfo.FurnitureSetList[i].TyepName;

                for (int j = 0; j < initLibOptionInfo.FurnitureSetList[i].ChildBtnsList.Count; j++)
                {
                    var ConfigBtn_PreObj = Instantiate(mConfigBtn_Pre);

                    ConfigBtn_PreObj.SetActive(true);

                    ConfigBtn_PreObj.transform.SetParent(configGrid_PreObj.transform);
                    ConfigBtn_PreObj.transform.localPosition = Vector3.zero;
                    ConfigBtn_PreObj.transform.localScale = Vector3.one;
                    ConfigBtn_PreObj.transform.Find("BtnName").GetComponent<Text>().text = initLibOptionInfo.FurnitureSetList[i].ChildBtnsList[j].Name;

                    string setTemp = initLibOptionInfo.FurnitureSetList[i].TyepName;
                    string btnSetTemp = initLibOptionInfo.FurnitureSetList[i].ChildBtnsList[j].Name;

                    ConfigBtn_PreObj.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        if (setTemp == "材质")
                        {
                            FurnitureLibCtrl.Instance.ChangeMat(btnSetTemp);
                        }
                        else if (setTemp == "产品")
                        {
                            FurnitureLibCtrl.Instance.LoadFurniture(initLibOptionInfo.TypeNmae, btnSetTemp);
                        }
                    });
                }
            }

            ConfigScrollView.gameObject.Show();
        }
        
    }

}