using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MySpace
{
    public class River : MonoBehaviour
    {
        public GameObject river;

        public River()
        {
            Vector3 pos = new Vector3(0, -4, 0);
            river = Object.Instantiate(Resources.Load("Prefab/River", typeof(GameObject)), pos, Quaternion.Euler(0, 0, 180f), null) as GameObject;
            river.name = "River";
        }
    }
}