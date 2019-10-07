using System;
using UnityEngine;
using MySpace;
using System.Collections.Generic;

namespace ActionSpace {

    public class SSActionManager:MonoBehaviour,ISSActionCallback {
        private Dictionary<int, SSAction> actions = new Dictionary<int, SSAction>();
        private List<SSAction> waitingAdd = new List<SSAction>();
        private List<int> waitingDel = new List<int>();

        public void Update() {
            //1 通过对应的动作ID，加入到字典中
            foreach(SSAction action in waitingAdd) {
                actions[action.GetInstanceID()] = action;
            }
            waitingAdd.Clear();

            //2 对于每一个动作，查看他们的完成情况，如果完成了那么就加入到删除队列
            //否则就完成这个动作
            foreach(KeyValuePair<int,SSAction> actionKV in actions) {
                SSAction action = actionKV.Value;
                if(action.destroy) {
                    waitingDel.Add(action.GetInstanceID());
                }
                else if(action.enable) {
                    action.Update();
                }
            }

            //3 对 在删除队列中的对象进行操作
            foreach(int actionID in waitingDel) {
                SSAction action = actions[actionID];
                actions.Remove(actionID);
                DestroyObject(action);
            }
            waitingDel.Clear();
        }


        public void AddAction(GameObject gameobject, SSAction action, ISSActionCallback callback) {
            action.gameobject = gameobject;
            action.transform = gameobject.transform;
            action.callback = callback;
            waitingAdd.Add(action);
            action.Start();
        }
        public void SSActionEvent(SSAction source) { 

        }
    }
}