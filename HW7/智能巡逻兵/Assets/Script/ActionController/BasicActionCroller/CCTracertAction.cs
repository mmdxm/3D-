using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 

using System.Collections;
using UnityEngine;

public class CCTracertAction : SSAction
{
    public GameObject target;
    public float speed;

    private CCTracertAction() { }
    public static CCTracertAction getAction(GameObject target, float speed)
    {
        CCTracertAction action = ScriptableObject.CreateInstance<CCTracertAction>();
        action.target = target;
        action.speed = speed;
        return action;
    }

    public override void Update()
    {
        this.transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        Quaternion rotation = Quaternion.LookRotation(target.transform.position - gameObject.transform.position, Vector3.up);
        gameObject.transform.rotation = rotation;
        if (gameObject.GetComponent<Prop>().follow_player == false||transform.position == target.transform.position)
        {
            destroy = true;
            CallBack.SSActionCallback(this);
        }
    }

    public override void Start()
    {

    }
}

