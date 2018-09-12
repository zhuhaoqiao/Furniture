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
	public class UIPopUpBoxData : UIPanelData
	{
        // TODO: Query Mgr's Data
        public Sprite[] BtnBgImgs;
        public Sprite BgImg;

        public string[] BtnTexts;
        public string TitleText;
        public string HintText;
        public bool ShowBg;

        public Action OnBtnOKClick;
        public Action OnBtnYesClick;
        public Action OnBtnNoClick;
    }

	public partial class UIPopUpBox : UIPanel
	{
		protected override void InitUI(IUIData uiData = null)
		{
			mData = uiData as UIPopUpBoxData ?? new UIPopUpBoxData();
            //please add init code here

            Bg.gameObject.SetActive(mData.ShowBg);

            if (mData.BgImg != null)
            {
                PopUpBox.sprite = mData.BgImg;
            }

            ShowButton();

            if (!string.IsNullOrEmpty(mData.HintText))
            {
                Hint.text = mData.HintText;
            }

            if (!string.IsNullOrEmpty(mData.TitleText))
            {
                Title.text = mData.TitleText;
            }
        }

		protected override void ProcessMsg (int eventId,QMsg msg)
		{
			throw new System.NotImplementedException ();
		}

		protected override void RegisterUIEvent()
		{
            BtnYes.onClick.AddListener(delegate
            {
                CloseSelf();

                mData.OnBtnYesClick.InvokeGracefully();
            });

            BtnNo.onClick.AddListener(delegate
            {
                CloseSelf();

                mData.OnBtnNoClick.InvokeGracefully();
            });

            BtnOK.onClick.AddListener(delegate
            {
                CloseSelf();

                mData.OnBtnOKClick.InvokeGracefully();
            });
        }

		protected override void OnShow()
		{
			base.OnShow();

		    //transform.SetParent(GameObject.Find("CenterEyeAnchor").transform);
		    //transform.localPosition = new Vector3(0f, 0f, 30f);
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
			Debug.Log("[ UIPopUpBox:]" + content);
		}

        private void ShowButton()
        {
            if (mData.BtnTexts == null || mData.BtnTexts.Length < 1)
                return;

            if (mData.BtnTexts.Length == 1)
            {
                BtnNo.gameObject.SetActive(false);
                BtnYes.gameObject.SetActive(false);
                BtnOK.gameObject.SetActive(true);

                OKText.text = mData.BtnTexts[0];
            }
            else if (mData.BtnTexts.Length == 2)
            {
                BtnNo.gameObject.SetActive(true);
                BtnYes.gameObject.SetActive(true);
                BtnOK.gameObject.SetActive(false);

                YesText.text = mData.BtnTexts[0];
                NoText.text = mData.BtnTexts[1];
            }

            if (mData.BtnBgImgs == null || mData.BtnBgImgs.Length < 1)
                return;

            if (mData.BtnBgImgs.Length == 1)
            {
                BtnOK.GetComponent<Image>().sprite = mData.BtnBgImgs[0];
            }
            else if (mData.BtnBgImgs.Length == 2)
            {
                BtnYes.GetComponent<Image>().sprite = mData.BtnBgImgs[0];
                BtnNo.GetComponent<Image>().sprite = mData.BtnBgImgs[1];
            }
        }
    }
}