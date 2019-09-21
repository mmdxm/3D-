using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MySpace
{
    //这个脚本仅用于生成两个对象，但是他们并没有任何属性，之后还需要加上运动弄的属性
    //用于生成恶魔和牧师这两个人物
    //其中方型表示牧师
    //球形表示魔鬼

    public class Character
    {
        public Move mScript;
        public GameObject Role { get; set; }
        public CoastController coast { get; set; }
        public bool OnBoat { get; set; }
        public string Name
        {
            get
            {
                return Role.name;
            }
            set
            {
                Role.name = value;
            }
        }

        public Character(string _name)
        {
            if(_name.Contains("Priest"))
            {
                Role = Object.Instantiate(Resources.Load("Prefab/Priest", typeof(GameObject)), Vector3.zero, Quaternion.Euler(0,0,180f), null) as GameObject;
            }
            else
            {
                Role = Object.Instantiate(Resources.Load("Prefab/Devil", typeof(GameObject)), Vector3.zero, Quaternion.Euler(0, 100f, 0), null) as GameObject;
            }
            OnBoat = false;
            Name = _name;
            mScript = Role.AddComponent(typeof(Move)) as Move;  //增加可移动属性
        }
    }
}