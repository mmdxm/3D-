using Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AutoMove
{
    public static AutoMove autoMove = new AutoMove();
    public FirstController firstScene;
    private int devilNum;
    private int priestNum;
    private int BoatCoast; // -1 -> left, 1 -> right.
    //P：船运载一个牧师
    //D：船运载一个恶魔
    //PP：船运载两个牧师
    //DD：船运载两个恶魔
    //PD：船运载一个牧师，一个恶魔
    private enum Boataction {empty, P, D, PP, DD, PD }
    private bool isFinished = true;
    private Boataction nextState;
    private int count = 0;
    private int num = 0;

    private AutoMove() { }

    public void move()
    {
        if (isFinished)
        {
            isFinished = false;
            Debug.Log(count);
            int[] fromCount = firstScene.fromCoast.GetobjectsNumber();
            priestNum = fromCount[0];
            devilNum = fromCount[1];
            BoatCoast = firstScene.boat.get_State();
            if (count == 0)
            {
                nextState = getNext();
                if ((int)nextState >= 3)
                {
                    num = 2;
                }
                else if ((int)nextState > 0) num = 1;
                else num = 0;
                count++;
            }
            Debug.Log("next state is " + nextState);
            DoAction();
        }
    }

    private void DoAction()
    {
        if (count == 1 && num != 0)
        {
            if (nextState == Boataction.D)
            {
                devilOnBoat();
            }
            else if (nextState == Boataction.DD)
            {
                devilOnBoat();
            }
            else if (nextState == Boataction.P)
            {
                priestOnBoat();
            }
            else if (nextState == Boataction.PP)
            {
                priestOnBoat();
            }
            else if (nextState == Boataction.PD)
            {
                priestOnBoat();
            }
            count++;
        }
        else if (num == 2 && count == 2)
        {
            if (nextState == Boataction.DD)
            {
                devilOnBoat();
            }
            else if (nextState == Boataction.PP)
            {
                priestOnBoat();
            }
            else if (nextState == Boataction.PD)
            {
                devilOnBoat();
            }
            count++;
        }
        else if((num == 1 && count == 2) || (num == 2 && count == 3) || (num == 0 && count == 1))
        {
            firstScene.MoveBoat();
            count++;
        }
        else if ((num == 1 && count >= 3) || (num == 2 && count >= 4) || (num == 0 && count>=2))
        {
            GetOffBoat();
        }
        isFinished = true;
    }

    private void GetOffBoat()
    {
        if((priestNum == 0 && devilNum == 2) || (priestNum == 0 && devilNum == 0))
        {
            if (firstScene.boat.get_State() == -1)
            {
                foreach (var x in firstScene.boat.passenger)
                {
                    if (x != null)
                    {
                        firstScene.ObjectIsClicked(x);
                        break;
                    }
                }
                if (firstScene.boat.isEmpty()) count = 0;
            }
            else count = 0;
        }
        else if (((priestNum == 0 && devilNum == 1)) && firstScene.boat.get_State() == 1)
        {
            count = 0;
        }
        else
        {
            foreach (var x in firstScene.boat.passenger)
            {
                if (x != null && x.getType() == 1)
                {
                    firstScene.ObjectIsClicked(x);
                    count = 0;
                    break;
                }
            }
            if (count != 0)
            {
                foreach (var x in firstScene.boat.passenger)
                {
                    if (x != null)
                    {
                        firstScene.ObjectIsClicked(x);
                        count = 0;
                        break;
                    }
                }
            }
        }
    }

    private void priestOnBoat()
    {
        if(BoatCoast == 1)
        {
            foreach(var x in firstScene.fromCoast.passengerPlaner)
            {
                if (x!=null && x.getType() == 0)
                {
                    firstScene.ObjectIsClicked(x);
                    return;
                }
            }
        }
        else
        {
            foreach (var x in firstScene.toCoast.passengerPlaner)
            {
                if (x != null && x.getType() == 0)
                {
                    firstScene.ObjectIsClicked(x);
                    return;
                }
            }
        }
    }

    private void devilOnBoat()
    {
        if (BoatCoast == 1)
        {
            foreach (var x in firstScene.fromCoast.passengerPlaner)
            {
                if (x != null && x.getType() == 1)
                {
                    firstScene.ObjectIsClicked(x);
                    return;
                }
            }
        }
        else
        {
            foreach (var x in firstScene.toCoast.passengerPlaner)
            {
                if (x != null && x.getType() == 1)
                {
                    firstScene.ObjectIsClicked(x);
                    return;
                }
            }
        }
    }

    private Boataction getNext()
    {
        Boataction next = Boataction.empty;
        if (BoatCoast == 1)
        {
            if (devilNum == 3 && priestNum == 3)//3P3DB
            {
                next = Boataction.PD;
            }
            else if (devilNum == 2 && priestNum == 3)//3P2DB
            {
                next = Boataction.DD;
            }
            else if (devilNum == 1 && priestNum == 3)//3P1DB
            {
                next = Boataction.PP;
            }
            else if (devilNum == 2 && priestNum == 2)//2P2DB
            {
                next = Boataction.PP;
            }
            else if (devilNum == 3 && priestNum == 0)//3DB
            {
                next = Boataction.DD;
            }
            else if (devilNum == 1 && priestNum == 1)//1P1DB
            {
                next = Boataction.PD;
            }
            else if (devilNum == 2 && priestNum == 0)//2DB
            {
                next = Boataction.D;
            }
            else if (devilNum == 1 && priestNum == 2)//2P1DB
            {
                next = Boataction.P;
            }
            else if (devilNum == 2 && priestNum == 1)//1P2DB
            {
                next = Boataction.P;
            }
            else if (devilNum == 1 && priestNum == 0)//1DB
            {
                next = Boataction.D;
            }
            else if(devilNum == 3 && priestNum == 2)//2P3DB
            {
                next = Boataction.D;
            }
            else next = Boataction.empty;
        }
        else
        {
            if (devilNum == 2 && priestNum == 2)//2P2D
            {
                next = Boataction.empty;
            }
            else if (devilNum == 1 && priestNum == 3)//3P1D
            {
                next = Boataction.empty;
            }
            else if (devilNum == 2 && priestNum == 3)//3P2D
            {
                next = Boataction.D;
            }
            else if (devilNum == 0 && priestNum == 3)//3P
            {
                next = Boataction.empty;
            }
            else if (devilNum == 1 && priestNum == 1)//1P1D
            {
                next = Boataction.D;
            }
            else if (devilNum == 2 && priestNum == 0)//2D
            {
                next = Boataction.D;
            }
            else if (devilNum == 1 && priestNum == 0)//1D
            {
                next = Boataction.empty;
            }
            else next = Boataction.empty;
        }
        return next;
    }

    public void restart()
    {
        count = 0;
        num = 0;
    }
}
