using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;
using UnityEngine.UI;

public class InterfaceGUI : MonoBehaviour {
    UserAction UserActionController;
    public GameObject t;
    bool ss = false;
    float S;
    float Now;
    int round = 1;
    // Use this for initialization
    void Start () {
        UserActionController = SSDirector.getInstance().currentScenceController as UserAction;
        S = Time.time;
    } 

    private void OnGUI()
    {
        if(!ss) S = Time.time;
        GUI.Label(new Rect(1000, 50, 500, 500),"Score: " + UserActionController.GetScore().ToString() + "  Time:  " + ((int)(Time.time - S)).ToString() + "  Round:  " + round );
        if (!ss && GUI.Button(new Rect(Screen.width / 2 - 30, Screen.height / 2 - 30, 100, 50), "Start"))
        {
            S = Time.time;
            ss = true;
            UserActionController.Restart();
        }
        if (ss)
        {
            round = UserActionController.GetRound();
            if (Input.GetButtonDown("Fire1"))
            {

                Vector3 pos = Input.mousePosition;
                UserActionController.Hit(pos);

            }
            if (round > 3)
            {
                round = 3;
                if (UserActionController.RoundStop())
                {
                    ss = false;
                }
            }
        }
    }
}
