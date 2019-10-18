using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskFactory { //开始时继承了MonoBehaviour后来发现不继承也可以
    public GameObject diskPrefab;
    public static DiskFactory DF = new DiskFactory();
   
    private Dictionary<int, Disk> used = new Dictionary<int, Disk>();//used是用来保存正在使用的飞碟 
    private List<Disk> free = new List<Disk>();//free是用来保存未激活的飞碟 

    private DiskFactory()
    {
        diskPrefab = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/disk"));//获取预制的游戏对象
        diskPrefab.AddComponent<Disk>();
        diskPrefab.SetActive(false);
    }

    public void FreeDisk()
    {
        foreach (Disk x in used.Values)
        {
            if (!x.gameObject.activeSelf)
            {
                free.Add(x);
                used.Remove(x.GetInstanceID());
                return;
            }
        }
    }

    public Disk GetDisk(int round)  
    {
        FreeDisk();
        GameObject newDisk = null;
        Disk diskdata;
        if (free.Count > 0)
        {
            //从之前生产的Disk中拿出可用的
            newDisk = free[0].gameObject;
            free.Remove(free[0]);
        }
        else
        {
            //克隆预制对象，生产新Disk
            newDisk = GameObject.Instantiate<GameObject>(diskPrefab, Vector3.zero, Quaternion.identity);
        }
        newDisk.SetActive(true);
        diskdata = newDisk.GetComponent<Disk>();

        int swith;

        /** 
         * 根据回合数来生成相应的飞碟,难度逐渐增加。
         */
        float s;
        if (round == 1)
        {
            swith = Random.Range(0, 3);
            s = Random.Range(30, 40);
        }
        else if (round == 2)
        {
            swith = Random.Range(0, 4);
            s = Random.Range(40, 50);
        }
        else {
            swith = Random.Range(0, 6);
            s = Random.Range(50, 60);
        } 
        
        switch (swith)  
        {  
             
            case 0:  
                {  
                    diskdata.color = Color.yellow;  
                    diskdata.speed = s;  
                    float RanX = UnityEngine.Random.Range(-1f, 1f) < 0 ? -1 : 1;  
                    diskdata.Direction = new Vector3(RanX, 1, 0);
                    diskdata.StartPoint = new Vector3(Random.Range(-130, -110), Random.Range(30,90), Random.Range(110,140));
                    break;  
                }  
            case 1:  
                {  
                    diskdata.color = Color.red;  
                    diskdata.speed = s + 10;  
                    float RanX = UnityEngine.Random.Range(-1f, 1f) < 0 ? -1 : 1;  
                    diskdata.Direction = new Vector3(RanX, 1, 0);
                    diskdata.StartPoint = new Vector3(Random.Range(-130, -110), Random.Range(30, 80), Random.Range(110, 130));
                    break;  
                }  
            case 2:  
                {  
                    diskdata.color = Color.black;  
                    diskdata.speed = s + 15;  
                    float RanX = UnityEngine.Random.Range(-1f, 1f) < 0 ? -1 : 1;  
                    diskdata.Direction = new Vector3(RanX, 1, 0);
                    diskdata.StartPoint = new Vector3(Random.Range(-130,-110), Random.Range(30, 70), Random.Range(90, 120));
                    break;  
                }
            case 3:
                {
                    diskdata.color = Color.yellow;
                    diskdata.speed = -s;
                    float RanX = UnityEngine.Random.Range(-1f, 1f) < 0 ? -1 : 1;
                    diskdata.Direction = new Vector3(RanX, 1, 0);
                    diskdata.StartPoint = new Vector3(Random.Range(130, 110), Random.Range(30, 90), Random.Range(110, 140));
                    break;
                }
            case 4:
                {
                    diskdata.color = Color.red;
                    diskdata.speed = -s - 10;
                    float RanX = UnityEngine.Random.Range(-1f, 1f) < 0 ? -1 : 1;
                    diskdata.Direction = new Vector3(RanX, 1, 0);
                    diskdata.StartPoint = new Vector3(Random.Range(130, 110), Random.Range(30, 80), Random.Range(110, 130));
                    break;
                }
            case 5:
                {
                    diskdata.color = Color.black;
                    diskdata.speed = -s - 15;
                    float RanX = UnityEngine.Random.Range(-1f, 1f) < 0 ? -1 : 1;
                    diskdata.Direction = new Vector3(RanX, 1, 0);
                    diskdata.StartPoint = new Vector3(Random.Range(130, 110), Random.Range(30, 70), Random.Range(90, 120));
                    break;
                }
        }
        used.Add(diskdata.GetInstanceID(), diskdata); //添加到使用中
        diskdata.name = diskdata.GetInstanceID().ToString();
        return diskdata;  
    }
}
