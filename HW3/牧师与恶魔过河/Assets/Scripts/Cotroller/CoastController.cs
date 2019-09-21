using UnityEngine;

namespace MySpace
{
    //新建一个河岸，并且要将河岸与人物相绑定
    public class CoastController {
        public Coast coast;

        public CoastController(string _name)
        {
            coast = new Coast(_name);
        }

        //找到空的位置，方便插入人物
        public int GetEmptyIndex()
        {
            for(int i = 0; i < coast.characters.Length; ++ i)
            {
                if (coast.characters[i] == null) return i;
            }
            return -1;
        }

        public Vector3 GetEmptyPosition()
        {
            Vector3 pos = coast.positions[GetEmptyIndex()];
            pos.x *= (coast.location == Location.right ? 1 : -1);
            return pos;
        }

        public void GetOnCoast(CharacterController character)
        {
            int index = GetEmptyIndex();
            coast.characters[index] = character;
        }

        public void GetOffCoast(string passenger_name)
        {
            for(int i = 0; i < coast.characters.Length; ++ i)
            {
                if(coast.characters[i] != null && coast.characters[i].character.Name==passenger_name)
                {
                    coast.characters[i] = null;
                }
            }
        }
        public int[] GetCharacterNum()
        {
            int[] num = { 0, 0 };
            for(int i = 0; i < coast.characters.Length; ++ i)
            {
                if(coast.characters[i] != null)
                {
                    if (coast.characters[i].character.Name.Contains("Priest")) num[0]++;
                    else num[1]++;
                }
            }
            return num;
        }
        public void Reset()
        {
            coast.characters = new CharacterController[6];
        }
    }
}