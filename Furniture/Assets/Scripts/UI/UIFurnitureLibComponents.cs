/****************************************************************************
 * 2018.9 DESKTOP-S5L3H66
 ****************************************************************************/

namespace QFramework.Example
{
	using UnityEngine;
	using UnityEngine.UI;

	public partial class UIFurnitureLib
	{
		public const string NAME = "UIFurnitureLib";

		[SerializeField] public Image LibDrd;
		[SerializeField] public Button ReturnBtn;
		[SerializeField] public ScrollRect ConfigScrollView;

		protected override void ClearUIComponents()
		{
			LibDrd = null;
			ReturnBtn = null;
			ConfigScrollView = null;
		}

		private UIFurnitureLibData mPrivateData = null;

		public UIFurnitureLibData mData
		{
			get { return mPrivateData ?? (mPrivateData = new UIFurnitureLibData()); }
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
