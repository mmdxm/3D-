using UnityEngine;
using System.Collections;
using MySpace;
using ActionSpace;
public class KernelController : MonoBehaviour,ISceneController,IUserAction {
    private UserGUI userGUI;
    private ActionController actionController;
    public CoastController rightCoastCtr;
    public CoastController leftCoasrCtr;
    public BoatController boatCtr;
    public MySpace.CharacterController[] characters;
    public JudgeController judgeCtr;
    void Start() {
        actionController = GetComponent<ActionController>();
    }

    void Awake() {
        Director director = Director.GetInstance();
        director.CurrentSecnController = this;
        userGUI = gameObject.AddComponent<UserGUI>() as UserGUI;
        characters = new MySpace.CharacterController[6];
        //judgeCtr.leftCoasrCtr = leftCoasrCtr;
        //judgeCtr.rightCoastCtr = rightCoastCtr;
        //judgeCtr.boatCtr = boatCtr;
        LoadResources();
    }


    public void LoadResources() {
        GameObject river = (new River()).river;

        rightCoastCtr = new CoastController("right");
        leftCoasrCtr = new CoastController("left");
        
        boatCtr = new BoatController();

        judgeCtr = new JudgeController(rightCoastCtr, leftCoasrCtr, boatCtr);
        for (int i = 0; i < 3; ++ i) {
            MySpace.CharacterController temp = new MySpace.CharacterController("Priest" + i);
            temp.SetPosition(rightCoastCtr.GetEmptyPosition());
            temp.GetOnCoast(rightCoastCtr);
            rightCoastCtr.GetOnCoast(temp);
            characters[i] = temp;
        }
        for(int i = 0; i < 3; ++ i) {
            MySpace.CharacterController temp = new MySpace.CharacterController("Devil" + i);
            temp.SetPosition(rightCoastCtr.GetEmptyPosition());
            temp.GetOnCoast(rightCoastCtr);
            rightCoastCtr.GetOnCoast(temp);
            characters[i + 3] = temp;
        }
    }

    public void MoveBoat() {
        if (judgeCtr.Flag == -1 || judgeCtr.Flag == 1) return; //lock
        if (boatCtr.isEmpty()) return;
        //boatCtr.Move();
        actionController.BoatMove(boatCtr);
        boatCtr.SetPos();
        userGUI.status = judgeCtr.judgeGameOver();
    }

    public void CharacterClicked(MySpace.CharacterController characterCtr) {
        if (judgeCtr.Flag == -1 || judgeCtr.Flag == 1) return; //lock

        if (characterCtr.character.OnBoat) {
            CoastController tempCoast = (boatCtr.boat.location == Location.right ? rightCoastCtr : leftCoasrCtr);
            boatCtr.GetOffBoat(characterCtr.character.Name);
            //characterCtr.MoveTo(tempCoast.GetEmptyPosition());
            actionController.CharacterMove(characterCtr, tempCoast.GetEmptyPosition());

            characterCtr.GetOnCoast(tempCoast);
            tempCoast.GetOnCoast(characterCtr);
        }
        else {
            CoastController tempCoast = characterCtr.character.coast;
            if (tempCoast.coast.location != boatCtr.boat.location) return;
            if (boatCtr.GetEmptyIndex() == -1) return;

            tempCoast.GetOffCoast(characterCtr.character.Name);
            //characterCtr.MoveTo(boatCtr.GetEmptyPosition());
            actionController.CharacterMove(characterCtr, boatCtr.GetEmptyPosition());
            //Debug.Log("boat" + boatCtr.GetEmptyPosition());
            characterCtr.GetOnBoat(boatCtr);
            boatCtr.GetOnBoat(characterCtr);
        }
        userGUI.status = judgeCtr.judgeGameOver();
    }

    public void Restart() {
        boatCtr.Reset();
        rightCoastCtr.Reset();
        leftCoasrCtr.Reset();
        judgeCtr.Reset();
        for(int i = 0;i < 6; ++ i) {
            characters[i].Reset();
        }
    }
}
