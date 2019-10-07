using UnityEngine;
using System.Collections.Generic;
using MySpace;
namespace ActionSpace
{
    //通过使用SSactionManager中的函数
    //完成小船和人物的动作设计
    public class ActionController:SSActionManager
    {
        public void BoatMove(BoatController boatCtr)
        {
            CCMoveToAction action = CCMoveToAction.GetSSAction(boatCtr.GetDestination(), boatCtr.boat.speed);
            //Debug.Log("11");
            AddAction(boatCtr.boat._Boat, action, this);
        }

        public void CharacterMove(MySpace.CharacterController characterCtr, Vector3 destination)
        {
            Vector3 currentPos = characterCtr.character.Role.transform.position;
            Vector3 middlePos = currentPos;

            //终点如果Y坐标小，那么就是上船
            //如果大，那么就是下船
            if (destination.y > currentPos.y) middlePos.y = destination.y;
            else middlePos.x = destination.x;
            //Debug.Log("current"+currentPos);
            //Debug.Log("middle"+middlePos);
            //Debug.Log("destination"+destination);
            SSAction action1 = CCMoveToAction.GetSSAction(middlePos, characterCtr.character.speed);
            SSAction action2 = CCMoveToAction.GetSSAction(destination, characterCtr.character.speed);

            SSAction actions = CCMixedAction.GetSSAction(1, 0, new List<SSAction> { action1,action2});
            AddAction(characterCtr.character.Role, actions, this);
        }
    }
}
