/****************************************************************************
 * 2018.9 DESKTOP-Q7KU4DV
 ****************************************************************************/

namespace QFramework.Example
{
	using UnityEngine;
	using UnityEngine.UI;

	public partial class UIInDoor
	{
		public const string NAME = "UIInDoor";

		[SerializeField] public Button DoorSwitchBtn;
		[SerializeField] public Button TimeAndSpaceSwitchBtn;
		[SerializeField] public Button AutoPutBtn;
		[SerializeField] public Button ReturnBtn;

		protected override void ClearUIComponents()
		{
			DoorSwitchBtn = null;
			TimeAndSpaceSwitchBtn = null;
			AutoPutBtn = null;
			ReturnBtn = null;
		}

		private UIInDoorData mPrivateData = null;

		public UIInDoorData mData
		{
			get { return mPrivateData ?? (mPrivateData = new UIInDoorData()); }
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
