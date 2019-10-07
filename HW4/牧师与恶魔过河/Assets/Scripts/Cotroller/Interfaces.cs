using System;
using ActionSpace;
namespace MySpace
{
    public interface ISceneController
    {
        void LoadResources();
    }

    public interface IUserAction
    {
        void MoveBoat();
        void CharacterClicked(CharacterController characterCtr);
        void Restart();
    }

    //public enum SSActionEventType { Started, Competeted };
    public interface ISSActionCallback
    {
        //void ActionDone(SSAction source);
        //下面的接口确实可以简化成上面的
        void SSActionEvent(SSAction source);

    }
}
