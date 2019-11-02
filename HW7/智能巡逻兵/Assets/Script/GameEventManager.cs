using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class GameEventManager
{
    public static GameEventManager Instance = new GameEventManager();
    //计分委托 
    public delegate void ScoreEvent();
    public static event ScoreEvent ScoreChange;
    //游戏结束委托
    public delegate void GameoverEvent();
    public static event GameoverEvent GameoverChange;

    private GameEventManager() { }

    //玩家逃脱进入新区域
    public void PlayerEscape()
    {
        if (ScoreChange != null)
        {
            ScoreChange();
        }
    }
    //玩家被捕，游戏结束
    public void PlayerGameover()
    {
        if (GameoverChange != null)
        {
            GameoverChange();
        }
    }
}