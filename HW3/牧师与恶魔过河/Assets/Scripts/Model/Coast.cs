using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MySpace
{
    public enum Location {right,left}


    public class Coast : MonoBehaviour
    {
        public GameObject coast;
        public Vector3 right_side;
        public Vector3 left_side;
        public Location location { get; set; }
        public CharacterController [] characters;
        public Vector3[] positions;

        public Coast(string _name)
        {
            characters = new CharacterController[6];

            //这6个位置是用来做放人物的
            positions = new Vector3[]
            {
                    new Vector3(3.5f,-0.5f,0),
                    new Vector3(4.5f,-0.5f,0),
                    new Vector3(5.5f,-0.5f,0),
                    new Vector3(6.5f,-0.5f,0),
                    new Vector3(7.5f,-0.5f,0),
                    new Vector3(8.5f,-0.5f,0),
            };

            right_side = new Vector3(6, -3, 0);
            left_side = new Vector3(-6, -3, 0);

            if (_name == "right")
            {
                coast = Object.Instantiate(Resources.Load("Prefab/Coast", typeof(GameObject)), right_side, Quaternion.Euler(0, 0, 180f), null) as GameObject;
                coast.name = "RightCoast";
                location = Location.right;
            }
            else if(_name == "left")
            {
                coast = Object.Instantiate(Resources.Load("Prefab/Coast", typeof(GameObject)), left_side, Quaternion.Euler(0, 0, 180f), null) as GameObject;
                coast.name = "LeftCoast";
                location = Location.left;
            }
        }

    }
}