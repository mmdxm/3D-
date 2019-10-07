using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MySpace {
    public enum Location {right,left}

    public class Coast {
        public GameObject coast;
        public Vector3 rightBank;
        public Vector3 leftBank;
        public Location location { get; set; }
        public CharacterController [] characters;
        public Vector3[] positions;

        public Coast(string _name) {
            characters = new CharacterController[6];

            //放置牧师或恶魔，岸上的六个位置
            positions = new Vector3[] {
                    new Vector3(5.5f,-0.5f,0),
                    new Vector3(6.5f,-0.5f,0),
                    new Vector3(7.5f,-0.5f,0),
                    new Vector3(8.5f,-0.5f,0),
                    new Vector3(9.5f,-0.5f,0),
                    new Vector3(10.5f,-0.5f,0),
            };
            //河岸位置
            rightBank = new Vector3(8, -3, 0);
            leftBank = new Vector3(-8, -3, 0);

            if (_name == "right") {
                coast = Object.Instantiate(Resources.Load("Prefab/Coast", typeof(GameObject)), rightBank, Quaternion.Euler(0, 0, 180f), null) as GameObject;
                coast.name = "RightCoast";
                location = Location.right;
            }
            else if(_name == "left") {
                coast = Object.Instantiate(Resources.Load("Prefab/Coast", typeof(GameObject)), leftBank, Quaternion.Euler(0, 0, 180f), null) as GameObject;
                coast.name = "LeftCoast";
                location = Location.left;
            }
        }

    }
}
