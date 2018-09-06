using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Example
{
    //产品库选项的信息
    public class LibOptionInfo
    {
        public string TypeNmae;
        public Sprite BgSprite = null;

        public List<FurniturelTemp> FurniturelTempList = new List<FurniturelTemp>();
    }

    //室内户型的信息
    public class InDoorInfo
    {
        public int id;
        public string name;
    }

    //家具产品的样式选择
    public class FurniturelTemp
    {
        public string TyepName;

        public List<BtnTemp> ChildBtnsList = new List<BtnTemp>();
    }

    //按钮的样板
    public class BtnTemp
    {
        public string Name;
        public Sprite Bg = null;
    }
}
