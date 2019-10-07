using UnityEngine;
namespace MySpace {
    public class Boat {
        //public readonly Move mScript;

        public readonly Vector3 rightSide;
        public readonly Vector3 leftSide;
        public readonly Vector3[] RightSides;
        public readonly Vector3[] LeftSides;
        public CharacterController[] passengers = new CharacterController[2];
        public GameObject _Boat { get; set; }
        public Location location { get; set; }
        public readonly float speed = 20;
        public Boat() {
            //船只停泊的位置,左岸和右岸
            rightSide = new Vector3(4, -2, 0);
            leftSide = new Vector3(-4, -2, 0);
            location = Location.right;
            //船上人物位置,左岸和右岸，注意两者顺序正好相反，以防两个人物同时坐到船的同一个位置上。
            RightSides = new Vector3[] {
               new Vector3(4.5f,-1f,0),
               new Vector3(3.5f,-1f,0)
            };
            LeftSides = new Vector3[] {
                new Vector3(-3.5f,-1f,0),
                new Vector3(-4.5f, -1f, 0)
            };
            _Boat = Object.Instantiate(Resources.Load("Prefab/Boat", typeof(GameObject)), rightSide, Quaternion.Euler(0, 0, 180f), null) as GameObject;
            _Boat.name = "boat";
            //mScript = _Boat.AddComponent(typeof(Move)) as Move;
            _Boat.AddComponent(typeof(UserGUI));
        }
    }
}
