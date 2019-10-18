using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

public class CCActionManager : SSActionManager, SSActionCallback, IActionManager
{
    int count = 0;//记录所有在移动的碟子的数量
    public SSActionEventType Complete = SSActionEventType.Completed;
   
    public void PlayDisk(Disk disk)
    {
        count++;
        Complete = SSActionEventType.Started;
        CCMoveToAction action = CCMoveToAction.getAction(disk.speed);
        addAction(disk.gameObject, action, this);
    }

    public void SSActionCallback(SSAction source) //运动事件结束后的回调函数
    {
        count--;
        Complete = SSActionEventType.Completed;
        source.gameObject.SetActive(false);
    }

    public bool IsAllFinished() //主要为了防止游戏结束时场景还有对象但是GUI按钮已经加载出来
    {
        if (count == 0) return true;
        else return false;
    }
}