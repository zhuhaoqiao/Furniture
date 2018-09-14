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

		[SerializeField] public Image SelectPanel;
		[SerializeField] public RectTransform LibBtns;
		[SerializeField] public Button ReturnBtn;
		[SerializeField] public ScrollRect ConfigScrollView;
		[SerializeField] public Button TypeBtn_Pre;
		[SerializeField] public Image InfoPanel;
		[SerializeField] public ScrollRect InfoScrollView;
		[SerializeField] public Button BackBtn;

		protected override void ClearUIComponents()
		{
			SelectPanel = null;
			LibBtns = null;
			ReturnBtn = null;
			ConfigScrollView = null;
			TypeBtn_Pre = null;
			InfoPanel = null;
			InfoScrollView = null;
			BackBtn = null;
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
