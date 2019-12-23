    
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class VitrualButtonEventHandler : MonoBehaviour, IVirtualButtonEventHandler
{
    //public GameObject vb;
    public Animator ani;
    public VirtualButtonBehaviour[] vbs;
    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        if (vb.VirtualButtonName == "VirtualButton")
        {
            ani.gameObject.transform.Rotate(Vector3.forward * 180);
        }
        Debug.Log(vb.VirtualButtonName);
        //throw new System.NotImplementedException();
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        ani.gameObject.transform.Rotate(Vector3.forward * 180);
    }

    // Start is called before the first frame update
    void Start()
    {
        vbs = GetComponentsInChildren<VirtualButtonBehaviour>();
        //VirtualButtonBehaviour vbb = vb.GetComponent<VirtualButtonBehaviour>();
        for (int i = 0; i < vbs.Length; i++)
        {
            vbs[i].RegisterEventHandler(this);
        }
    }

}