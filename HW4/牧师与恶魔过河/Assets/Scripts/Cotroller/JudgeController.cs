using System;
namespace MySpace
{
    //游戏判定主要是还是船上人物和岸上人物的数量及类型的判定
    public class JudgeController
    {
        public CoastController rightCoastCtr;
        public CoastController leftCoasrCtr;
        public BoatController boatCtr;
        public int Flag = 0;
        //0  表示正在游戏
        //1  表示win
        //-1 表示输了
        public JudgeController(CoastController rightCoastCtr, CoastController leftCoasrCtr, BoatController boatCtr)
        {
            this.rightCoastCtr = rightCoastCtr;
            this.leftCoasrCtr = leftCoasrCtr;
            this.boatCtr = boatCtr;
        }

        public int judgeGameOver()
        {
            int rightP = 0;
            int leftP = 0;
            int rightD = 0;
            int leftD = 0;
            int flag = 0;

            rightP = rightCoastCtr.GetCharacterNum()[0];
            rightD = rightCoastCtr.GetCharacterNum()[1];
            leftP = leftCoasrCtr.GetCharacterNum()[0];
            leftD = leftCoasrCtr.GetCharacterNum()[1];

            if (leftD + leftP == 6) flag = 1; //win

            if (boatCtr.boat.location == Location.right)
            {
                rightP += boatCtr.GetCharacterNum()[0];
                rightD += boatCtr.GetCharacterNum()[1];
            }
            else
            {
                leftP += boatCtr.GetCharacterNum()[0];
                leftD += boatCtr.GetCharacterNum()[1];
            }

            if ((rightP < rightD && rightP > 0) || (leftP < leftD && leftP > 0)) flag = -1; //lose
            Flag = flag;                                                                                //Debug.Log("rightP" + rightP + " rightD" + rightD + " leftP" +leftP+" leftD"+leftD+" flag"+flag);
            return flag;
        }

        public void Reset()
        {
            Flag = 0;
        }
    }
}