using UnityEngine;
namespace MySpace
{
    public class CharacterController
    {
        public Character character;
        public UserGUI userGUI;
        public CharacterController(string _name)
        {
            character = new Character(_name);
            userGUI = character.Role.AddComponent(typeof(UserGUI)) as UserGUI;
            userGUI.SetCharacterCtrl(this);
        }
        public void SetPosition(Vector3 _pos)
        {
            character.Role.transform.position = _pos;
        }
        public void MoveTo(Vector3 _pos)
        {
            character.mScript.SetDestination(_pos);
        }
        public void GetOnBoat(BoatController boatCtr)
        {
            character.coast = null;
            character.Role.transform.parent = boatCtr.boat._Boat.transform;
            character.OnBoat = true;
        }
        public void GetOnCoast(CoastController coastCtr)
        {
            character.coast = coastCtr;
            character.Role.transform.parent = null;
            character.OnBoat = false;
        }

        public void Reset()
        {
            character.mScript.Reset();
            character.coast = (Director.GetInstance().CurrentSecnController as KernelController).rightCoastCtr;
            GetOnCoast(character.coast);
            SetPosition(character.coast.GetEmptyPosition());
            character.coast.GetOnCoast(this);
        }
    }
}