using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MySpace
{
    public class UserGUI : MonoBehaviour
    {
        private string guide = "游戏规则：\n小船仅可载1~2人\n让牧师(凯尔)\n和恶魔(莫甘娜)全部过河\n当任意一边恶魔数多于牧师时，游戏失败\n";
        private IUserAction action;
        private GUIStyle guideStyle;
        private GUIStyle buttonStyle;
        private GUIStyle textStyle;
        public CharacterController characterController;
        public int status;

        void Start()
        {
            status = 0;
            action = Director.GetInstance().CurrentSecnController as IUserAction;
        }

        void OnGUI()
        {
            GUIStyle winfont = new GUIStyle();
            winfont.normal.textColor = Color.green;
            winfont.fontSize = 60;
            winfont.alignment = TextAnchor.MiddleCenter;
            GUIStyle losefont = new GUIStyle();
            losefont.normal.textColor = Color.red;
            losefont.fontSize = 60;
            losefont.alignment = TextAnchor.MiddleCenter;

            textStyle = new GUIStyle
            {
                fontSize = 60,
                alignment = TextAnchor.MiddleCenter
            };
            guideStyle = new GUIStyle
            {
                fontSize = 30,
                fontStyle = FontStyle.Normal
            };
            guideStyle.normal.textColor = Color.yellow;
            buttonStyle = new GUIStyle("Button")
            {
                fontSize = 30
            };
            GUI.Label(new Rect(50, 100, 100, 50), guide, guideStyle);
            GUI.Label(new Rect(Screen.width / 2 - 20, Screen.height / 2 - 250, 100, 50), "Priest And Devil", textStyle);
            GUI.Label(new Rect(50, Screen.height / 2 - 400, 100, 50), "友情提示：\n立方形表示牧师\n球形表示恶魔", guideStyle);
            if (GUI.Button(new Rect(Screen.width / 2 - 50, 100, 150, 100), "Restart", buttonStyle))
            {
                status = 0;
                action.Restart();
            }
            if (status != 1 && status != 0)
            {
                //游戏结束
                GUI.Label(new Rect(Screen.width / 2 - 100+50, 450, 100, 50), "You Lose!", losefont);

            }
            if (status == 1)
            {
                GUI.Label(new Rect(Screen.width / 2 - 100+50, 450, 100, 50), "You Win!", winfont);

            }
        }
        public void SetCharacterCtrl(CharacterController _cc)
        {
            characterController = _cc;
        }

        public void OnMouseDown()
        {
            if (gameObject.name == "boat")
            {
                action.MoveBoat();
            }
            else
            {
                action.CharacterClicked(characterController);
            }
        }
    }
}