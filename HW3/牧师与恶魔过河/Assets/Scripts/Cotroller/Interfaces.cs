using System;

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
}
