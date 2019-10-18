using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

public class FirstSceneController : MonoBehaviour, ISceneController, UserAction
{    
    public bool PhysicManager = false;
    bool ManagerofNow = false; //Manager's type of now
    int score = 0;
    int round = 1;
    int tral = 0;
    bool start = false;
    IActionManager Manager;
    DiskFactory DF;

    void Awake()
    {
        SSDirector director = SSDirector.getInstance();
        director.currentScenceController = this;
        DF = DiskFactory.DF;
    }

    // Use this for initialization
    void Start () {
        
    }

    // Update is called once per frame
    int count = 0;
	void Update () {
        if(start == true)
        {
            count++;
            if (count >= 80)
            {
                count = 0;

                if(DF == null)
                {
                    Debug.LogWarning("DF is NUll!");
                    return;
                }
                tral++;
                Disk d = DF.GetDisk(round);
                if (Manager == null)
                {
                    Debug.LogWarning("Manager is NULL!");
                }
                Manager.PlayDisk(d);
                if (tral == 10)
                {
                    round++;
                    tral = 0;
                }
            }
        }
	}

    public void LoadResources()
    {
        ManagerofNow = PhysicManager;
        if (PhysicManager)
        {
            Manager = this.gameObject.AddComponent<CCPhysisActionManager>() as IActionManager;
        }
        else
        {
            Manager = this.gameObject.AddComponent<CCActionManager>() as IActionManager;
        }
    }

    public void Hit(Vector3 pos)
    {
        Ray ray = Camera.main.ScreenPointToRay(pos);

        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray);
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];

            if (hit.collider.gameObject.GetComponent<Disk>() != null)
            {
                Color c = hit.collider.gameObject.GetComponent<Renderer>().material.color;
                if (c == Color.yellow) score += 1;
                if (c == Color.red) score += 2;
                if (c == Color.black) score += 3;
                
                hit.collider.gameObject.transform.position = new Vector3(0, -5, 0);
            }

        }
    }

    public int GetScore()
    {
        return score;
    }

    public void Restart()
    {
        score = 0;
        round = 1;
        start = true;
    }
    public bool RoundStop()
    {
        if (round > 3)
        {
            start = false;
            if (Manager.IsAllFinished())
            {
                if (ManagerofNow)
                {
                    Destroy(this.gameObject.GetComponent<CCPhysisActionManager>());
                }
                else
                {
                    Destroy(this.gameObject.GetComponent<CCActionManager>());
                }
                DF.FreeDisk();
                return true;
            }
            else return false;
        }
        else return false;
    }
    public int GetRound()
    {
        return round;
    }
}