/****************************************************************************
 * 2018.9 DESKTOP-S5L3H66
 ****************************************************************************/

namespace QFramework.Example
{
	using UnityEngine;
	using UnityEngine.UI;

	public partial class UIDoor
	{
		public const string NAME = "UIDoor";

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

		private UIDoorData mPrivateData = null;

		public UIDoorData mData
		{
			get { return mPrivateData ?? (mPrivateData = new UIDoorData()); }
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
