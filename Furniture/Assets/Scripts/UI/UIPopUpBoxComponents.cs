/****************************************************************************
 * 2018.9 DESKTOP-S5L3H66
 ****************************************************************************/

namespace QFramework.Example
{
	using UnityEngine;
	using UnityEngine.UI;

	public partial class UIPopUpBox
	{
		public const string NAME = "UIPopUpBox";

		[SerializeField] public Image Bg;
		[SerializeField] public Image PopUpBox;
		[SerializeField] public Button BtnYes;
		[SerializeField] public Text YesText;
		[SerializeField] public Button BtnNo;
		[SerializeField] public Text NoText;
		[SerializeField] public Button BtnOK;
		[SerializeField] public Text OKText;
		[SerializeField] public Text Title;
		[SerializeField] public Text Hint;

		protected override void ClearUIComponents()
		{
			Bg = null;
			PopUpBox = null;
			BtnYes = null;
			YesText = null;
			BtnNo = null;
			NoText = null;
			BtnOK = null;
			OKText = null;
			Title = null;
			Hint = null;
		}

		private UIPopUpBoxData mPrivateData = null;

		public UIPopUpBoxData mData
		{
			get { return mPrivateData ?? (mPrivateData = new UIPopUpBoxData()); }
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
