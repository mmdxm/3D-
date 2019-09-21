using UnityEngine;
using System.Collections;
using MySpace;
public class KernelController : MonoBehaviour,ISceneController,IUserAction
{
    private UserGUI userGUI;

    public CoastController rightCoastCtr;
    public CoastController leftCoasrCtr;
    public BoatController boatCtr;
    public MySpace.CharacterController[] characters;

    void Awake()
    {
        Director director = Director.GetInstance();
        director.CurrentSecnController = this;
        userGUI = gameObject.AddComponent<UserGUI>() as UserGUI;
        characters = new MySpace.CharacterController[6];
        LoadResources();
    }

    public void LoadResources()
    {
        GameObject river = (new River()).river;

        rightCoastCtr = new CoastController("right");
        leftCoasrCtr = new CoastController("left");

        boatCtr = new BoatController();

        for(int i = 0; i < 3; ++ i)
        {
            MySpace.CharacterController temp = new MySpace.CharacterController("Priest"+i);
            temp.SetPosition(rightCoastCtr.GetEmptyPosition());
            temp.GetOnCoast(rightCoastCtr);
            rightCoastCtr.GetOnCoast(temp);
            characters[i] = temp;
        }
        for(int i = 0; i < 3; ++ i)
        {
            MySpace.CharacterController temp = new MySpace.CharacterController("Devil" + i);
            temp.SetPosition(rightCoastCtr.GetEmptyPosition());
            temp.GetOnCoast(rightCoastCtr);
            rightCoastCtr.GetOnCoast(temp);
            characters[i + 3] = temp;
        }
    }

    private int CheckGameOver()
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

        if(boatCtr.boat.location == Location.right)
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

        return flag;

    }

    public void MoveBoat()
    {
        if (userGUI.status == -1 || userGUI.status == 1) return;
        if (boatCtr.isEmpty()) return;
        boatCtr.Move();
        userGUI.status = CheckGameOver();
    }

    public void CharacterClicked(MySpace.CharacterController characterCtr)
    {
        if (userGUI.status == -1 || userGUI.status == 1) return;
        if (characterCtr.character.OnBoat)
        {
            CoastController tempCoast = (boatCtr.boat.location == Location.right ? rightCoastCtr : leftCoasrCtr);
            boatCtr.GetOffBoat(characterCtr.character.Name);
            characterCtr.MoveTo(tempCoast.GetEmptyPosition());
            characterCtr.GetOnCoast(tempCoast);
            tempCoast.GetOnCoast(characterCtr);
        }
        else
        {
            CoastController tempCoast = characterCtr.character.coast;
            if (tempCoast.coast.location != boatCtr.boat.location) return; //不再同一边的人上不去
            if (boatCtr.GetEmptyIndex() == -1) return;//船上没人

            tempCoast.GetOffCoast(characterCtr.character.Name);
            characterCtr.MoveTo(boatCtr.GetEmptyPosition());
            characterCtr.GetOnBoat(boatCtr);
            boatCtr.GetOnBoat(characterCtr);
        }
        userGUI.status = CheckGameOver();
    }

    public void Restart()
    {
        boatCtr.Reset();
        rightCoastCtr.Reset();
        leftCoasrCtr.Reset();
        for(int i = 0;i < 6; ++ i)
        {
            characters[i].Reset();
        }
    }
}
