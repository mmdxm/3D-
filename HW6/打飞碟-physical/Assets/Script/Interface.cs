using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interfaces
{
    public interface ISceneController
    { 
        void LoadResources();
    }
 
    public interface UserAction
    {
        void Hit(Vector3 pos);
        void Restart();
        int GetScore();
        bool RoundStop();
        int GetRound();
    }

    public enum SSActionEventType : int { Started, Completed }

    public interface SSActionCallback
    {
        void SSActionCallback(SSAction source);
    }

    public interface IActionManager
    {
        void PlayDisk(Disk disk);
        bool IsAllFinished(); //主要为了防止游戏结束时场景还有对象但是GUI按钮已经加载出来
    }
}