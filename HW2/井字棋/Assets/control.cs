using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class control : MonoBehaviour {
    
    private int sync = 1;
    private char[,] state =  new char[3,3];
    private int click = 1;

    // Use this for initialization
    void Start () {
        reset ();
    }

    void OnGUI() {
        int result = check();
        GUIStyle font = new GUIStyle();
        font.fontSize = 60;
        font.normal.textColor = Color.green;
        if (result == 'X') {
            GUI.Label (new Rect (140, 270, 100, 100), "X wins",font);
            sync = 0;
        }
        else if (result == 'O') {
            GUI.Label (new Rect (140, 270, 100, 100), "O wins",font);
            sync = 0;
        }
        if (GUI.Button (new Rect (190, 350, 100, 50), "Reset")) {
            Debug.Log ("Reset button clicked");
            reset ();
        }
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                if (state[i, j] == 'X') {
                    GUI.Button (new Rect (80 * (i + 1) + 40, 80 * (j + 1) - 60, 80, 80), "X");
                }
                if (state[i, j] == 'O') {
                    GUI.Button (new Rect (80 * (i + 1) + 40, 80 * (j + 1) - 60, 80, 80), "O");
                }
                if (GUI.Button (new Rect (80*(i+1) + 40, 80*(j+1) - 60, 80, 80), "")) {
                    if(sync == 1)
                    {
                        state[i,j] = click == 1? 'X' : 'O';
                        click = -click;
                    }
                    
                }
            }
        }
    }

    void reset() {
        sync = 1;
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                state [i, j] = ' ';
            }
        }
    }

    char check() {
        for (int i = 0; i < 3; i++) {
            if (state [i, 0] != ' ' && state [i, 0] == state [i, 1] && state [i, 1] == state [i, 2]) {
                return state [i, 0];
            }
        }
        for (int j = 0; j < 3; j++) {
            if (state [0, j] != ' ' && state [1, j] == state [0, j] && state [1, j] == state [2, j]) {
                return state [0, j];
            }
        }
        if (state [1, 1] != ' ' &&
            state [0, 0] == state [1, 1] && state [1, 1] == state [2, 2] ||
            state [0, 2] == state [1, 1] && state [1, 1] == state [2, 0]) {
            return state [1, 1];
        }
        return ' ';
    }

    // Update is called once per frame
    void Update () {

    }
}

