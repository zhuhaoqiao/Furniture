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
        private List<BtnTemp> mTypeNamesList = new List<BtnTemp>();

        private Dictionary<string, LibOptionInfo> mLibOpDict = new Dictionary<string, LibOptionInfo>();
        private List<GameObject> mBtnShowBoxList = new List<GameObject>();

        private GameObject mConfigBtn_Pre;
        private GameObject mConfigGrid_Pre;
        private GameObject mTypeBtn_Pre;

        private int mCurrentTypeSelect = 0;

        private int mBeforeFurSelect = 0;
        private int mCurrentFurSelect = 0;

        protected override void InitUI(IUIData uiData = null)
        {
            mConfigBtn_Pre = transform.Find("ConfigBtn_Pre").gameObject;
            mConfigGrid_Pre = transform.Find("ConfigGrid_Pre").gameObject;
            mTypeBtn_Pre = transform.Find("TypeBtn_Pre").gameObject;

            mData = uiData as UIFurnitureLibData ?? new UIFurnitureLibData();

            mCurrentTypeSelect = 0;

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
        }

        protected override void OnShow()
        {
            base.OnShow();

            QUIManager.Instance.transform.SetParent(GameObject.Find("LeftHandAnchor").transform);
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
            InitInfos();

            foreach (KeyValuePair<string, LibOptionInfo> kvp in mLibOpDict)
            {
                BtnTemp mBtnTemp = new BtnTemp();

                mBtnTemp.Name = kvp.Key;
                mBtnTemp.Bg = kvp.Value.BgSprite;

                mTypeNamesList.Add(mBtnTemp);
            }

            InitTypeBtns();
            InitScrollView(mLibOpDict[mTypeNamesList[0].Name]);

            FurnitureLibCtrl.Instance.LoadFurniture(mLibOpDict[mTypeNamesList[mCurrentTypeSelect].Name].Type, 
                mLibOpDict[mTypeNamesList[mCurrentTypeSelect].Name].FurnitureSetList[0].ChildBtnsList[0].AssetName);
        }

        void JumpToSelectLib(int mLibID)
        {
            ConfigScrollView.gameObject.Show();

            Debug.Log(mTypeNamesList[mLibID]);
        }

        private void AddFurniturelStyle(string typeName,string type ,List<FurnitureSet> furnitureSets, Sprite typeBg = null)
        {
            LibOptionInfo mLibOptionInfo = new LibOptionInfo();
            mLibOptionInfo.TypeName = typeName;
            mLibOptionInfo.Type = type;
            mLibOptionInfo.BgSprite = typeBg;
            mLibOptionInfo.FurnitureSetList = furnitureSets;

            if (mLibOpDict.ContainsKey(typeName))
            {
                mLibOpDict[typeName] = mLibOptionInfo;
            }
            else
            {
                mLibOpDict.Add(typeName, mLibOptionInfo);
            }
        }

        private void InitTypeBtns()
        {
            for (int i = 0; i < mTypeNamesList.Count; i++)
            {
                var TypeBtn_PreObj = Instantiate(mTypeBtn_Pre);

                TypeBtn_PreObj.SetActive(true);

                TypeBtn_PreObj.transform.SetParent(LibBtns.transform);
                TypeBtn_PreObj.transform.localPosition = Vector3.zero;
                TypeBtn_PreObj.transform.localScale = Vector3.one;
                TypeBtn_PreObj.GetComponent<Image>().sprite = mTypeNamesList[i].Bg;
                TypeBtn_PreObj.transform.Find("Text").GetComponent<Text>().text = mTypeNamesList[i].Name;

                string showTypeTemp = mTypeNamesList[i].Name;
                int selectId = i;

                TypeBtn_PreObj.GetComponent<Button>().onClick.AddListener(() =>
                {
                    if (selectId != mCurrentTypeSelect)
                    {                      
                        mCurrentTypeSelect = selectId;
                        JumpToSelectLib(mCurrentTypeSelect);
                        InitScrollView(mLibOpDict[mTypeNamesList[i].Name]);

                        FurnitureLibCtrl.Instance.LoadFurniture(mLibOpDict[mTypeNamesList[mCurrentTypeSelect].Name].Type,
                            mLibOpDict[mTypeNamesList[mCurrentTypeSelect].Name].FurnitureSetList[0].ChildBtnsList[0].AssetName);
                    }
                });
            }
        }

        private void InitScrollView(LibOptionInfo initLibOptionInfo)
        {
            mBtnShowBoxList.Clear();

            int childCount = ConfigScrollView.content.childCount;
            
            for (int i = 0; i < childCount; i++)
            {             
                Destroy(ConfigScrollView.content.GetChild(i).gameObject);
            }

            for (int i = 0; i < initLibOptionInfo.FurnitureSetList.Count; i++)
            {
                GameObject configGrid_PreObj = null;

                for (int j = 0; j < initLibOptionInfo.FurnitureSetList[i].ChildBtnsList.Count; j++)
                {
                    if (j % 6 == 0)
                    {
                        configGrid_PreObj = Instantiate(mConfigGrid_Pre);
                        configGrid_PreObj.SetActive(true);

                        configGrid_PreObj.transform.SetParent(ConfigScrollView.content);
                        configGrid_PreObj.transform.localPosition = Vector3.zero;
                        configGrid_PreObj.transform.localScale = Vector3.one;
                    }

                    var ConfigBtn_PreObj = Instantiate(mConfigBtn_Pre);

                    ConfigBtn_PreObj.SetActive(true);

                    ConfigBtn_PreObj.transform.SetParent(configGrid_PreObj.transform);
                    ConfigBtn_PreObj.transform.localPosition = Vector3.zero;
                    ConfigBtn_PreObj.transform.localScale = Vector3.one;
                    ConfigBtn_PreObj.transform.Find("BtnName").GetComponent<Text>().text = initLibOptionInfo.FurnitureSetList[i].ChildBtnsList[j].Name;
                    ConfigBtn_PreObj.GetComponent<Image>().sprite = initLibOptionInfo.FurnitureSetList[i].ChildBtnsList[j].Bg;

                    mBtnShowBoxList.Add(ConfigBtn_PreObj.transform.Find("SelectBox").gameObject);

                    string setTemp = initLibOptionInfo.FurnitureSetList[i].TyepName;
                    string furnitureNameTemp = initLibOptionInfo.FurnitureSetList[i].ChildBtnsList[j].AssetName;
                    int btnId = j;

                    ConfigBtn_PreObj.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        mBeforeFurSelect = mCurrentFurSelect;
                        mCurrentFurSelect = btnId;
                        ShowSelectBox(mCurrentFurSelect);
                        FurnitureLibCtrl.Instance.LoadFurniture(initLibOptionInfo.Type, furnitureNameTemp);
                    });
                }
            }

            ConfigScrollView.gameObject.Show();
            ShowSelectBox(0);
        }

        private void ShowSelectBox(int showId)
        {
            mBtnShowBoxList[mBeforeFurSelect].Hide();
            mBtnShowBoxList[showId].Show();      
        }

        private void InitInfos()
        {
            List<BtnTemp> chengjuBtnTemps = new List<BtnTemp>(new BtnTemp[]
               {
                    new BtnTemp(){Name = "餐桌_1",Bg =  FurnitureLibCtrl.Instance.LoadBgByName("chengjusprite" ,"chengju_canzhuo_N2D001"),AssetName = "chengju_canzhuo_N2D001"},
                    new BtnTemp(){Name = "餐桌_2",Bg =  FurnitureLibCtrl.Instance.LoadBgByName("chengjusprite" ,"chengju_canzhuo_N2D002"),AssetName = "chengju_canzhuo_N2D002"},
                    new BtnTemp(){Name = "餐桌_3",Bg =  FurnitureLibCtrl.Instance.LoadBgByName("chengjusprite" ,"chengju_canzhuo_N2D003"),AssetName = "chengju_canzhuo_N2D003"},
                    new BtnTemp(){Name = "茶几_1",Bg =  FurnitureLibCtrl.Instance.LoadBgByName("chengjusprite" ,"chengju_chaji_N2CT001"),AssetName = "chengju_chaji_N2CT001"},
                    new BtnTemp(){Name = "茶几_2",Bg =  FurnitureLibCtrl.Instance.LoadBgByName("chengjusprite" ,"chengju_chaji_N2CT002"),AssetName = "chengju_chaji_N2CT002"},
                    new BtnTemp(){Name = "角几_1",Bg =  FurnitureLibCtrl.Instance.LoadBgByName("chengjusprite" ,"chengju_jiaoji_001"),AssetName = "chengju_jiaoji_001"},
                    new BtnTemp(){Name = "角几_2",Bg =  FurnitureLibCtrl.Instance.LoadBgByName("chengjusprite" ,"chengju_jiaoji_002"),AssetName = "chengju_jiaoji_002"},
                    new BtnTemp(){Name = "梳妆台",Bg =  FurnitureLibCtrl.Instance.LoadBgByName("chengjusprite" ,"chengju_shuzhuangtai_N2T002"),AssetName = "chengju_shuzhuangtai_N2T002"},
                    new BtnTemp(){Name = "书桌_1",Bg =  FurnitureLibCtrl.Instance.LoadBgByName("chengjusprite" ,"chengju_shuzhuo_N2W001"),AssetName = "chengju_shuzhuo_N2W001"},
                    new BtnTemp(){Name = "书桌_2",Bg =  FurnitureLibCtrl.Instance.LoadBgByName("chengjusprite" ,"chengju_shuzhuo_N2W002"),AssetName = "chengju_shuzhuo_N2W002"},
                    new BtnTemp(){Name = "书桌_3",Bg =  FurnitureLibCtrl.Instance.LoadBgByName("chengjusprite" ,"chengju_shuzhuo_N2W003"),AssetName = "chengju_shuzhuo_N2W003"},
               }
           );

            List<BtnTemp> guijuBtnTemps = new List<BtnTemp>(new BtnTemp[]
               {
                    new BtnTemp(){Name = "边柜_1",Bg =  FurnitureLibCtrl.Instance.LoadBgByName("guijusprite" ,"guiju_biangui_N4S001"),AssetName = "guiju_biangui_N4S001"},
                    new BtnTemp(){Name = "边柜_2",Bg =  FurnitureLibCtrl.Instance.LoadBgByName("guijusprite" ,"guiju_biangui_N4S002"),AssetName = "guiju_biangui_N4S002"},
                    new BtnTemp(){Name = "边柜_3",Bg =  FurnitureLibCtrl.Instance.LoadBgByName("guijusprite" ,"guiju_biangui_N4S003"),AssetName = "guiju_biangui_N4S003"},
                    new BtnTemp(){Name = "边柜_4",Bg =  FurnitureLibCtrl.Instance.LoadBgByName("guijusprite" ,"guiju_biangui_N4S004"),AssetName = "guiju_biangui_N4S004"},
                    new BtnTemp(){Name = "餐边柜",Bg =  FurnitureLibCtrl.Instance.LoadBgByName("guijusprite" ,"guiju_canbiangui_N4C001"),AssetName = "guiju_canbiangui_N4C001"},
                    new BtnTemp(){Name = "陈列柜",Bg =  FurnitureLibCtrl.Instance.LoadBgByName("guijusprite" ,"guiju_chenliegui_N4E001"),AssetName = "guiju_chenliegui_N4E001"},
                    new BtnTemp(){Name = "床头柜_1",Bg =  FurnitureLibCtrl.Instance.LoadBgByName("guijusprite" ,"guiju_chuantougui_N4N001"),AssetName = "guiju_chuantougui_N4N001"},
                    new BtnTemp(){Name = "床头柜_2",Bg =  FurnitureLibCtrl.Instance.LoadBgByName("guijusprite" ,"guiju_chuantougui_N4N002"),AssetName = "guiju_chuantougui_N4N002"},
                    new BtnTemp(){Name = "电视柜_1",Bg =  FurnitureLibCtrl.Instance.LoadBgByName("guijusprite" ,"guiju_dianshigui_N4TV001"),AssetName = "guiju_dianshigui_N4TV001"},
                    new BtnTemp(){Name = "电视柜_2",Bg =  FurnitureLibCtrl.Instance.LoadBgByName("guijusprite" ,"guiju_dianshigui_N4TV002"),AssetName = "guiju_dianshigui_N4TV002"},
                    new BtnTemp(){Name = "电视柜_3",Bg =  FurnitureLibCtrl.Instance.LoadBgByName("guijusprite" ,"guiju_dianshigui_N4TV003"),AssetName = "guiju_dianshigui_N4TV003"},
                    new BtnTemp(){Name = "电视柜_4",Bg =  FurnitureLibCtrl.Instance.LoadBgByName("guijusprite" ,"guiju_dianshigui_N4TV004"),AssetName = "guiju_dianshigui_N4TV004"},
                    new BtnTemp(){Name = "电视柜_5",Bg =  FurnitureLibCtrl.Instance.LoadBgByName("guijusprite" ,"guiju_dianshigui_N4TV005"),AssetName = "guiju_dianshigui_N4TV005"},
                    new BtnTemp(){Name = "电视柜_6",Bg =  FurnitureLibCtrl.Instance.LoadBgByName("guijusprite" ,"guiju_dianshigui_N4TV006"),AssetName = "guiju_dianshigui_N4TV006"},
                    new BtnTemp(){Name = "斗柜_1",Bg =  FurnitureLibCtrl.Instance.LoadBgByName("guijusprite" ,"guiju_dougui_N4F001"),AssetName = "guiju_dougui_N4F001"},
                    new BtnTemp(){Name = "斗柜_2",Bg =  FurnitureLibCtrl.Instance.LoadBgByName("guijusprite" ,"guiju_dougui_N4F002"),AssetName = "guiju_dougui_N4F002"},
                    new BtnTemp(){Name = "斗柜_3",Bg =  FurnitureLibCtrl.Instance.LoadBgByName("guijusprite" ,"guiju_dougui_N4F003"),AssetName = "guiju_dougui_N4F003"},
                    new BtnTemp(){Name = "书柜",Bg =  FurnitureLibCtrl.Instance.LoadBgByName("guijusprite" ,"guiju_shugui_N4B001"),AssetName = "guiju_shugui_N4B001"},
                    new BtnTemp(){Name = "衣架",Bg =  FurnitureLibCtrl.Instance.LoadBgByName("guijusprite" ,"guiju_yijia_YIJIA"),AssetName = "guiju_yijia_YIJIA"},
               }
           );

            List<BtnTemp> jiajuBtnTemps = new List<BtnTemp>(new BtnTemp[]
               {
                    new BtnTemp(){Name = "置物架",Bg =  FurnitureLibCtrl.Instance.LoadBgByName("jiajusprite" ,"jiaju_zhiwujia"),AssetName = "jiaju_zhiwujia"},
                }
           );

            List<BtnTemp> wojuBtnTemps = new List<BtnTemp>(new BtnTemp[]
               {
                    new BtnTemp(){Name = "床_1",Bg =  FurnitureLibCtrl.Instance.LoadBgByName("wojusprite" ,"woju_chuan_N3B001"),AssetName = "woju_chuan_N3B001"},
                    new BtnTemp(){Name = "床_2",Bg =  FurnitureLibCtrl.Instance.LoadBgByName("wojusprite" ,"woju_chuan_N3B002"),AssetName = "woju_chuan_N3B002"},
                }
           );

            List<BtnTemp> zuojuBtnTemps = new List<BtnTemp>(new BtnTemp[]
               {
                    new BtnTemp(){Name = "餐椅_1",Bg =  FurnitureLibCtrl.Instance.LoadBgByName("zuojusprite" ,"zuoju_canyi_N1H001"),AssetName = "zuoju_canyi_N1H001"},
                    new BtnTemp(){Name = "餐椅_2",Bg =  FurnitureLibCtrl.Instance.LoadBgByName("zuojusprite" ,"zuoju_canyi_N1H002"),AssetName = "zuoju_canyi_N1H002"},
                    new BtnTemp(){Name = "餐椅_3",Bg =  FurnitureLibCtrl.Instance.LoadBgByName("zuojusprite" ,"zuoju_canyi_N1H003"),AssetName = "zuoju_canyi_N1H003"},
                    new BtnTemp(){Name = "凳子",Bg =  FurnitureLibCtrl.Instance.LoadBgByName("zuojusprite" ,"zuoju_dengzi"),AssetName = "zuoju_dengzi"},
                    new BtnTemp(){Name = "旋转椅",Bg =  FurnitureLibCtrl.Instance.LoadBgByName("zuojusprite" ,"zuoju_xuanzhaunmudeng"),AssetName = "zuoju_xuanzhaunmudeng"},
                    new BtnTemp(){Name = "椅子",Bg =  FurnitureLibCtrl.Instance.LoadBgByName("zuojusprite" ,"zuoju_yizi"),AssetName = "zuoju_yizi"},
                }
           );

            AddFurniturelStyle("承具", "chengju", new List<FurnitureSet>(new FurnitureSet[]{ new FurnitureSet(){ TyepName = "产品",ChildBtnsList = chengjuBtnTemps}}));
            AddFurniturelStyle("庋具", "guiju", new List<FurnitureSet>(new FurnitureSet[] { new FurnitureSet() { TyepName = "产品", ChildBtnsList = guijuBtnTemps } }));
            AddFurniturelStyle("家具", "jiaju", new List<FurnitureSet>(new FurnitureSet[] { new FurnitureSet() { TyepName = "产品", ChildBtnsList = jiajuBtnTemps } }));
            AddFurniturelStyle("卧具", "woju", new List<FurnitureSet>(new FurnitureSet[] { new FurnitureSet() { TyepName = "产品", ChildBtnsList = wojuBtnTemps } }));
            AddFurniturelStyle("坐具", "zuoju", new List<FurnitureSet>(new FurnitureSet[] { new FurnitureSet() { TyepName = "产品", ChildBtnsList = zuojuBtnTemps } }));
        }
    }

}