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
        private GameObject mInfoText_Pre;

        private int mCurrentTypeSelect = 0;

        private int mBeforeFurSelect = 0;
        private int mCurrentFurSelect = 0;

        protected override void InitUI(IUIData uiData = null)
        {
            mConfigBtn_Pre = transform.Find("SelectPanel/ConfigBtn_Pre").gameObject;
            mConfigGrid_Pre = transform.Find("SelectPanel/ConfigGrid_Pre").gameObject;
            mTypeBtn_Pre = transform.Find("SelectPanel/TypeBtn_Pre").gameObject;
            mInfoText_Pre = transform.Find("InfoPanel/InfoText_Pre").gameObject;

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

            BackBtn.onClick.AddListener(() =>
            {
                InfoPanel.Hide();
                InitSelectScrollView(mLibOpDict[mTypeNamesList[mCurrentTypeSelect].Name]);
                SelectPanel.Show();              
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
            InitSelectScrollView(mLibOpDict[mTypeNamesList[0].Name]);

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
                        InitSelectScrollView(mLibOpDict[mTypeNamesList[mCurrentTypeSelect].Name]);

                        FurnitureLibCtrl.Instance.LoadFurniture(mLibOpDict[mTypeNamesList[mCurrentTypeSelect].Name].Type,
                            mLibOpDict[mTypeNamesList[mCurrentTypeSelect].Name].FurnitureSetList[0].ChildBtnsList[0].AssetName);
                    }
                });
            }
        }

        private void InitSelectScrollView(LibOptionInfo initLibOptionInfo)
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
                    if (j % 7 == 0)
                    {
                        configGrid_PreObj = Instantiate(mConfigGrid_Pre);
                        configGrid_PreObj.SetActive(true);

                        configGrid_PreObj.transform.SetParent(ConfigScrollView.content);
                        configGrid_PreObj.transform.localPosition = Vector3.zero;
                        configGrid_PreObj.transform.localRotation = Quaternion.identity;
                        configGrid_PreObj.transform.localScale = Vector3.one;
                    }

                    var ConfigBtn_PreObj = Instantiate(mConfigBtn_Pre);

                    ConfigBtn_PreObj.SetActive(true);

                    ConfigBtn_PreObj.transform.SetParent(configGrid_PreObj.transform);
                    ConfigBtn_PreObj.transform.localPosition = Vector3.zero;
                    ConfigBtn_PreObj.transform.localScale = Vector3.one;

                    ConfigBtn_PreObj.GetComponent<Image>().sprite = initLibOptionInfo.FurnitureSetList[i].ChildBtnsList[j].Bg;

                    mBtnShowBoxList.Add(ConfigBtn_PreObj.transform.Find("SelectBox").gameObject);

                    string furnitureNameTemp = initLibOptionInfo.FurnitureSetList[i].ChildBtnsList[j].AssetName;
                    int setBtnId = i;
                    int btnId = j;

                    ConfigBtn_PreObj.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        mBeforeFurSelect = mCurrentFurSelect;
                        mCurrentFurSelect = btnId;
                        ShowSelectBox(mCurrentFurSelect);
                        FurnitureLibCtrl.Instance.LoadFurniture(initLibOptionInfo.Type, furnitureNameTemp);

                        SelectPanel.Hide();
                        InfoPanel.Show();
                        InitInfoScrollView(initLibOptionInfo.FurnitureSetList[setBtnId].FurnitureInfoDict[furnitureNameTemp]);
                    });
                }
            }

            ConfigScrollView.gameObject.Show();
            ShowSelectBox(0);
        }

        private void InitInfoScrollView(FurnitureInfo furnitureInfo)
        {
            GameObject infoText_PreObj = null;

            for (int i = 0; i < furnitureInfo.infoList.Count + 1; i++)
            {               
                infoText_PreObj = Instantiate(mInfoText_Pre);
                infoText_PreObj.SetActive(true);

                infoText_PreObj.transform.SetParent(InfoScrollView.content);
                infoText_PreObj.transform.localPosition = Vector3.zero;
                infoText_PreObj.transform.localRotation = Quaternion.identity;
                infoText_PreObj.transform.localScale = Vector3.one;

                if (i == 0)
                {
                    infoText_PreObj.GetComponent<Text>().text = furnitureInfo.name;
                }
                else
                {
                    infoText_PreObj.GetComponent<Text>().text = furnitureInfo.infoList[i - 1];
                }               
            }
        }

        private void ShowSelectBox(int showId)
        {
            mBtnShowBoxList[mBeforeFurSelect].Hide();
            mBtnShowBoxList[showId].Show();      
        }

        private void InitInfos()
        {
            Dictionary<string, FurnitureInfo> chengjuDict = new Dictionary<string, FurnitureInfo>()
            {
                {"chengju_canzhuo_N2D001", new FurnitureInfo(){name = "餐桌",infoList = new List<string>(){ "型号：N2D001" } } },
                {"chengju_canzhuo_N2D002", new FurnitureInfo(){name = "餐桌",infoList = new List<string>(){ "型号：N2D002" } } },
                {"chengju_canzhuo_N2D003", new FurnitureInfo(){name = "餐桌",infoList = new List<string>(){ "型号：N2D003" } } },
                {"chengju_chaji_N2CT001", new FurnitureInfo(){name = "茶几",infoList = new List<string>(){ "型号：N2CT001" } } },
                {"chengju_chaji_N2CT002", new FurnitureInfo(){name = "茶几",infoList = new List<string>(){ "型号：N2CT002" } } },
                {"chengju_jiaoji_001", new FurnitureInfo(){name = "角几" } },
                {"chengju_jiaoji_002", new FurnitureInfo(){name = "角几" } },
                {"chengju_shuzhuangtai_N2T002", new FurnitureInfo(){name = "梳妆台",infoList = new List<string>(){ "型号：N2T002" } } },
                {"chengju_shuzhuo_N2W001", new FurnitureInfo(){name = "书桌",infoList = new List<string>(){ "型号：N2W001" } } },
                {"chengju_shuzhuo_N2W002", new FurnitureInfo(){name = "书桌",infoList = new List<string>(){ "型号：N2W002" } } },
                {"chengju_shuzhuo_N2W003", new FurnitureInfo(){name = "书桌",infoList = new List<string>(){ "型号：N2W003" } } },
            };            
            List<BtnTemp> chengjuBtnTemps = new List<BtnTemp>(new BtnTemp[]
               {
                    new BtnTemp(){Bg =  FurnitureLibCtrl.Instance.LoadBgByName("chengjusprite" ,"chengju_canzhuo_N2D001"),AssetName = "chengju_canzhuo_N2D001"},
                    new BtnTemp(){Bg =  FurnitureLibCtrl.Instance.LoadBgByName("chengjusprite" ,"chengju_canzhuo_N2D002"),AssetName = "chengju_canzhuo_N2D002"},
                    new BtnTemp(){Bg =  FurnitureLibCtrl.Instance.LoadBgByName("chengjusprite" ,"chengju_canzhuo_N2D003"),AssetName = "chengju_canzhuo_N2D003"},
                    new BtnTemp(){Bg =  FurnitureLibCtrl.Instance.LoadBgByName("chengjusprite" ,"chengju_chaji_N2CT001"),AssetName = "chengju_chaji_N2CT001"},
                    new BtnTemp(){Bg =  FurnitureLibCtrl.Instance.LoadBgByName("chengjusprite" ,"chengju_chaji_N2CT002"),AssetName = "chengju_chaji_N2CT002"},
                    new BtnTemp(){Bg =  FurnitureLibCtrl.Instance.LoadBgByName("chengjusprite" ,"chengju_jiaoji_001"),AssetName = "chengju_jiaoji_001"},
                    new BtnTemp(){Bg =  FurnitureLibCtrl.Instance.LoadBgByName("chengjusprite" ,"chengju_jiaoji_002"),AssetName = "chengju_jiaoji_002"},
                    new BtnTemp(){Bg =  FurnitureLibCtrl.Instance.LoadBgByName("chengjusprite" ,"chengju_shuzhuangtai_N2T002"),AssetName = "chengju_shuzhuangtai_N2T002"},
                    new BtnTemp(){Bg =  FurnitureLibCtrl.Instance.LoadBgByName("chengjusprite" ,"chengju_shuzhuo_N2W001"),AssetName = "chengju_shuzhuo_N2W001"},
                    new BtnTemp(){Bg =  FurnitureLibCtrl.Instance.LoadBgByName("chengjusprite" ,"chengju_shuzhuo_N2W002"),AssetName = "chengju_shuzhuo_N2W002"},
                    new BtnTemp(){Bg =  FurnitureLibCtrl.Instance.LoadBgByName("chengjusprite" ,"chengju_shuzhuo_N2W003"),AssetName = "chengju_shuzhuo_N2W003"},
               });

            Dictionary<string, FurnitureInfo> guijuDict = new Dictionary<string, FurnitureInfo>()
            {
                {"guiju_biangui_N4S001", new FurnitureInfo(){name = "边柜",infoList = new List<string>(){ "型号：N4S001" } } },
                {"guiju_biangui_N4S002", new FurnitureInfo(){name = "边柜",infoList = new List<string>(){ "型号：N4S002" } } },
                {"guiju_biangui_N4S003", new FurnitureInfo(){name = "边柜",infoList = new List<string>(){ "型号：N4S003" } } },
                {"guiju_biangui_N4S004", new FurnitureInfo(){name = "边柜",infoList = new List<string>(){ "型号：N4S004" } } },
                {"guiju_canbiangui_N4C001", new FurnitureInfo(){name = "餐边柜",infoList = new List<string>(){ "型号：N4C001" } } },
                {"guiju_chenliegui_N4E001", new FurnitureInfo(){name = "陈列柜",infoList = new List<string>(){ "型号：N4E001" } } },
                {"guiju_chuantougui_N4N001", new FurnitureInfo(){name = "床头柜",infoList = new List<string>(){ "型号：N4N001" } } },
                {"guiju_chuantougui_N4N002", new FurnitureInfo(){name = "床头柜",infoList = new List<string>(){ "型号：N4N002" } } },
                {"guiju_chuantougui_N4N003", new FurnitureInfo(){name = "床头柜",infoList = new List<string>(){ "型号：N4N003" } } },
                {"guiju_chuantougui_N4N004", new FurnitureInfo(){name = "床头柜",infoList = new List<string>(){ "型号：N4N004" } } },
                {"guiju_chuantougui_N4N005", new FurnitureInfo(){name = "床头柜",infoList = new List<string>(){ "型号：N4N005" } } },
                {"guiju_chuantougui_N4N006", new FurnitureInfo(){name = "床头柜",infoList = new List<string>(){ "型号：N4N006" } } },
                {"guiju_dougui_N4F001", new FurnitureInfo(){name = "斗柜",infoList = new List<string>(){ "型号：N4F001" } } },
                {"guiju_dougui_N4F002", new FurnitureInfo(){name = "斗柜",infoList = new List<string>(){ "型号：N4F002" } } },
                {"guiju_dougui_N4F003", new FurnitureInfo(){name = "斗柜",infoList = new List<string>(){ "型号：N4F003" } } },
                {"guiju_shugui_N4B001", new FurnitureInfo(){name = "书柜",infoList = new List<string>(){ "型号：N4B001" } } },
                {"guiju_yijia_YIJIA", new FurnitureInfo(){name = "衣架" } },
            };
            List<BtnTemp> guijuBtnTemps = new List<BtnTemp>(new BtnTemp[]
               {
                    new BtnTemp(){Bg =  FurnitureLibCtrl.Instance.LoadBgByName("guijusprite" ,"guiju_biangui_N4S001"),AssetName = "guiju_biangui_N4S001"},
                    new BtnTemp(){Bg =  FurnitureLibCtrl.Instance.LoadBgByName("guijusprite" ,"guiju_biangui_N4S002"),AssetName = "guiju_biangui_N4S002"},
                    new BtnTemp(){Bg =  FurnitureLibCtrl.Instance.LoadBgByName("guijusprite" ,"guiju_biangui_N4S003"),AssetName = "guiju_biangui_N4S003"},
                    new BtnTemp(){Bg =  FurnitureLibCtrl.Instance.LoadBgByName("guijusprite" ,"guiju_biangui_N4S004"),AssetName = "guiju_biangui_N4S004"},
                    new BtnTemp(){Bg =  FurnitureLibCtrl.Instance.LoadBgByName("guijusprite" ,"guiju_canbiangui_N4C001"),AssetName = "guiju_canbiangui_N4C001"},
                    new BtnTemp(){Bg =  FurnitureLibCtrl.Instance.LoadBgByName("guijusprite" ,"guiju_chenliegui_N4E001"),AssetName = "guiju_chenliegui_N4E001"},
                    new BtnTemp(){Bg =  FurnitureLibCtrl.Instance.LoadBgByName("guijusprite" ,"guiju_chuantougui_N4N001"),AssetName = "guiju_chuantougui_N4N001"},
                    new BtnTemp(){Bg =  FurnitureLibCtrl.Instance.LoadBgByName("guijusprite" ,"guiju_chuantougui_N4N002"),AssetName = "guiju_chuantougui_N4N002"},
                    new BtnTemp(){Bg =  FurnitureLibCtrl.Instance.LoadBgByName("guijusprite" ,"guiju_dianshigui_N4TV001"),AssetName = "guiju_dianshigui_N4TV001"},
                    new BtnTemp(){Bg =  FurnitureLibCtrl.Instance.LoadBgByName("guijusprite" ,"guiju_dianshigui_N4TV002"),AssetName = "guiju_dianshigui_N4TV002"},
                    new BtnTemp(){Bg =  FurnitureLibCtrl.Instance.LoadBgByName("guijusprite" ,"guiju_dianshigui_N4TV003"),AssetName = "guiju_dianshigui_N4TV003"},
                    new BtnTemp(){Bg =  FurnitureLibCtrl.Instance.LoadBgByName("guijusprite" ,"guiju_dianshigui_N4TV004"),AssetName = "guiju_dianshigui_N4TV004"},
                    new BtnTemp(){Bg =  FurnitureLibCtrl.Instance.LoadBgByName("guijusprite" ,"guiju_dianshigui_N4TV005"),AssetName = "guiju_dianshigui_N4TV005"},
                    new BtnTemp(){Bg =  FurnitureLibCtrl.Instance.LoadBgByName("guijusprite" ,"guiju_dianshigui_N4TV006"),AssetName = "guiju_dianshigui_N4TV006"},
                    new BtnTemp(){Bg =  FurnitureLibCtrl.Instance.LoadBgByName("guijusprite" ,"guiju_dougui_N4F001"),AssetName = "guiju_dougui_N4F001"},
                    new BtnTemp(){Bg =  FurnitureLibCtrl.Instance.LoadBgByName("guijusprite" ,"guiju_dougui_N4F002"),AssetName = "guiju_dougui_N4F002"},
                    new BtnTemp(){Bg =  FurnitureLibCtrl.Instance.LoadBgByName("guijusprite" ,"guiju_dougui_N4F003"),AssetName = "guiju_dougui_N4F003"},
                    new BtnTemp(){Bg =  FurnitureLibCtrl.Instance.LoadBgByName("guijusprite" ,"guiju_shugui_N4B001"),AssetName = "guiju_shugui_N4B001"},
                    new BtnTemp(){Bg =  FurnitureLibCtrl.Instance.LoadBgByName("guijusprite" ,"guiju_yijia_YIJIA"),AssetName = "guiju_yijia_YIJIA"},
               });

            Dictionary<string, FurnitureInfo> jiajuDict = new Dictionary<string, FurnitureInfo>()
            {
                {"jiaju_zhiwujia", new FurnitureInfo(){name = "置物架" } },
            };
            List<BtnTemp> jiajuBtnTemps = new List<BtnTemp>(new BtnTemp[]
               {
                    new BtnTemp(){Bg =  FurnitureLibCtrl.Instance.LoadBgByName("jiajusprite" ,"jiaju_zhiwujia"),AssetName = "jiaju_zhiwujia"},
                });

            Dictionary<string, FurnitureInfo> wojuDict = new Dictionary<string, FurnitureInfo>()
            {
                {"woju_chuan_N3B001", new FurnitureInfo(){name = "床",infoList = new List<string>(){ "型号：N3B001" } } },
                {"woju_chuan_N3B002", new FurnitureInfo(){name = "床",infoList = new List<string>(){ "型号：N3B002" } } },
            };
            List<BtnTemp> wojuBtnTemps = new List<BtnTemp>(new BtnTemp[]
               {
                    new BtnTemp(){Bg =  FurnitureLibCtrl.Instance.LoadBgByName("wojusprite" ,"woju_chuan_N3B001"),AssetName = "woju_chuan_N3B001"},
                    new BtnTemp(){Bg =  FurnitureLibCtrl.Instance.LoadBgByName("wojusprite" ,"woju_chuan_N3B002"),AssetName = "woju_chuan_N3B002"},
                });

            Dictionary<string, FurnitureInfo> zuojuDict = new Dictionary<string, FurnitureInfo>()
            {
                {"zuoju_canyi_N1H001", new FurnitureInfo(){name = "餐椅",infoList = new List<string>(){ "型号：N1H001" } } },
                {"zuoju_canyi_N1H002", new FurnitureInfo(){name = "餐椅",infoList = new List<string>(){ "型号：N1H002" } } },
                {"zuoju_canyi_N1H003", new FurnitureInfo(){name = "餐椅",infoList = new List<string>(){ "型号：N1H003" } } },
                {"zuoju_dengzi", new FurnitureInfo(){name = "凳子" } },
                {"zuoju_xuanzhaunmudeng", new FurnitureInfo(){name = "旋转椅" } },
                {"zuoju_yizi", new FurnitureInfo(){name = "椅子" } },
            };
            List<BtnTemp> zuojuBtnTemps = new List<BtnTemp>(new BtnTemp[]
               {
                    new BtnTemp(){Bg =  FurnitureLibCtrl.Instance.LoadBgByName("zuojusprite" ,"zuoju_canyi_N1H001"),AssetName = "zuoju_canyi_N1H001"},
                    new BtnTemp(){Bg =  FurnitureLibCtrl.Instance.LoadBgByName("zuojusprite" ,"zuoju_canyi_N1H002"),AssetName = "zuoju_canyi_N1H002"},
                    new BtnTemp(){Bg =  FurnitureLibCtrl.Instance.LoadBgByName("zuojusprite" ,"zuoju_canyi_N1H003"),AssetName = "zuoju_canyi_N1H003"},
                    new BtnTemp(){Bg =  FurnitureLibCtrl.Instance.LoadBgByName("zuojusprite" ,"zuoju_dengzi"),AssetName = "zuoju_dengzi"},
                    new BtnTemp(){Bg =  FurnitureLibCtrl.Instance.LoadBgByName("zuojusprite" ,"zuoju_xuanzhaunmudeng"),AssetName = "zuoju_xuanzhaunmudeng"},
                    new BtnTemp(){Bg =  FurnitureLibCtrl.Instance.LoadBgByName("zuojusprite" ,"zuoju_yizi"),AssetName = "zuoju_yizi"},
                });

            AddFurniturelStyle("承具", "chengju", new List<FurnitureSet>(new FurnitureSet[]{ new FurnitureSet(){ TyepName = "产品", ChildBtnsList = chengjuBtnTemps, FurnitureInfoDict = chengjuDict} }));
            AddFurniturelStyle("庋具", "guiju", new List<FurnitureSet>(new FurnitureSet[] { new FurnitureSet() { TyepName = "产品", ChildBtnsList = guijuBtnTemps, FurnitureInfoDict = guijuDict} }));
            AddFurniturelStyle("家具", "jiaju", new List<FurnitureSet>(new FurnitureSet[] { new FurnitureSet() { TyepName = "产品", ChildBtnsList = jiajuBtnTemps, FurnitureInfoDict = jiajuDict} }));
            AddFurniturelStyle("卧具", "woju", new List<FurnitureSet>(new FurnitureSet[] { new FurnitureSet() { TyepName = "产品", ChildBtnsList = wojuBtnTemps,FurnitureInfoDict = wojuDict } }));
            AddFurniturelStyle("坐具", "zuoju", new List<FurnitureSet>(new FurnitureSet[] { new FurnitureSet() { TyepName = "产品", ChildBtnsList = zuojuBtnTemps,FurnitureInfoDict = zuojuDict } }));
        }

    }

}