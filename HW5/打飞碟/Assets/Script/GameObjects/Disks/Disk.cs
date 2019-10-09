using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disk : MonoBehaviour {
    public Vector3 StartPoint { get { return gameObject.transform.position; } set { gameObject.transform.position = value; } }
    public Color color { get { return gameObject.GetComponent<Renderer>().material.color; } set { gameObject.GetComponent<Renderer>().material.color = value; } }
    public float speed { get;set; }
    public Vector3 Direction { get { return Direction; } set { gameObject.transform.Rotate(value); } }
}
 