using System;
using System.Collections;
using UnityEngine;

namespace ActionSpace {
    public class CCMoveToAction:SSAction {
        public Vector3 target;
        public float speed;

        public static CCMoveToAction GetSSAction(Vector3 target, float speed) {
            CCMoveToAction action = ScriptableObject.CreateInstance<CCMoveToAction> ();
            action.speed = speed;
            action.target = target;
            return action;
        }


        public override void Start() {

        }

        public override void Update() {
            this.transform.position = Vector3.MoveTowards(this.transform.position, target, speed*Time.deltaTime);
            if(this.transform.position == target) {
                destroy = true;
                callback.SSActionEvent(this);
            }
        }
    }
}