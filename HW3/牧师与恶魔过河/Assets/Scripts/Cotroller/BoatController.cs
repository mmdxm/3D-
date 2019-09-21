using UnityEngine;

namespace MySpace
{
    public class BoatController
    {
        public Boat boat;

        public BoatController()
        {
            boat = new Boat();
        }

        public void Move()
        {
            if (boat.location == Location.right)
            {
                boat.mScript.SetDestination(boat.otherSide);
                boat.location = Location.left;
            }
            else
            {
                boat.mScript.SetDestination(boat.selfSide);
                boat.location = Location.right;
            }
        }

        public int GetEmptyIndex()
        {
            for (int i = 0; i < boat.passengers.Length; ++i)
            {
                if (boat.passengers[i] == null) return i;
            }
            return -1;
        }

        public Vector3 GetEmptyPosition()
        {
            int index = GetEmptyIndex();
            if(boat.location == Location.right)
            {
                return boat.RightSides[index];
            }
            return boat.LeftSides[index];
        }

        public bool isEmpty()
        {
            for (int i = 0; i < boat.passengers.Length; ++i)
            {
                if (boat.passengers[i] != null) return false;
            }
            return true;
        }

        public void GetOnBoat(CharacterController character)
        {
            boat.passengers[GetEmptyIndex()] = character;
        }

        public void GetOffBoat(string _name)
        {
           for(int i = 0; i < boat.passengers.Length; ++ i)
            {
                if(boat.passengers[i] != null && boat.passengers[i].character.Name == _name)
                {
                    boat.passengers[i] = null;
                }
            }
        }

        public int[] GetCharacterNum()
        {
            int[] num = { 0, 0 };
            for(int i = 0; i < boat.passengers.Length; ++ i)
            {
                if (boat.passengers[i] != null)
                {
                    if (boat.passengers[i].character.Name.Contains("Priest")) num[0]++;
                    else num[1]++;
                }
            }
            return num;
        }

        public void Reset()
        {
            boat.mScript.Reset();
            if (boat.location == Location.left) Move();
            boat.passengers = new CharacterController[2];
        }
    }
}