using UnityEngine;
using System.Collections;

namespace MySpace
{
    public class Director : System.Object
    {
        private static Director _instance;

        public ISceneController CurrentSecnController { get; set; }
        public static Director GetInstance()
        {
            return _instance ?? (_instance = new Director());
        }
    }
}