using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAlongAxeses : MonoBehaviour {
    
    [Header("Rotate Along : ")]

    [SerializeField] bool X;
    [SerializeField] bool Y;
    [SerializeField] bool Z;

    [SerializeField] float speed = 60f;

    
    Transform t;

    public float Speed { get => speed; set => speed = value; }

    // Use this for initialization
    void Start () {
        t = transform;
	}
	
	// Update is called once per frame
	void Update () {

        if (X)
            t.Rotate(t.right, Time.deltaTime * speed);
        if (Y)
            t.Rotate(t.up, Time.deltaTime * speed);
        if (Z)
            t.Rotate(t.forward, Time.deltaTime * speed);
    }

}
