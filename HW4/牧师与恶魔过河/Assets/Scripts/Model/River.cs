using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MySpace {
    public class River {
        public GameObject river;

        public River() {
            Vector3 position = new Vector3(0, -4, 0);
            river = Object.Instantiate(Resources.Load("Prefab/River", typeof(GameObject)), position, Quaternion.Euler(0, 0, 180f), null) as GameObject;
            river.name = "River";
        }
    }
}
