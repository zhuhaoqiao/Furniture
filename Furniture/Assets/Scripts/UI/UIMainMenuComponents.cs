/****************************************************************************
 * 2018.9 DESKTOP-S5L3H66
 ****************************************************************************/

namespace QFramework.Example
{
	using UnityEngine;
	using UnityEngine.UI;

	public partial class UIMainMenu
	{
		public const string NAME = "UIMainMenu";

		[SerializeField] public Button ToDoorBtn;
		[SerializeField] public Button ToLibrBtn;

		protected override void ClearUIComponents()
		{
			ToDoorBtn = null;
			ToLibrBtn = null;
		}

		private UIMainMenuData mPrivateData = null;

		public UIMainMenuData mData
		{
			get { return mPrivateData ?? (mPrivateData = new UIMainMenuData()); }
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
