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

        //用于和ActionCtr交互
        public Vector3 GetDestination()
        {
            if (boat.location == Location.right) return boat.leftSide;
            return boat.rightSide;
        }

        public void SetPos()
        {
            if (boat.location == Location.right) boat.location = Location.left;
            else boat.location = Location.right;
        }

        //修改
        public void Reset()
        {
            //boat.mScript.Reset();
            if (boat.location == Location.left) boat.location = Location.right;
            boat._Boat.transform.position = boat.rightSide;
            boat.passengers = new CharacterController[2];
        }
    }
}
