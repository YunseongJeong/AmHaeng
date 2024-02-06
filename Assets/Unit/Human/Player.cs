using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Human
{
    int direction = 0;

    // Start is called before the first frame update
    void Start()
    {
        setRigidbody2D();
        setSpeed(10);
        setJumpSpeed(15);
        setMass(10);
    }
    
    // Update is called once per frame
    void Update()
    {
        direction = 0;
        if(Input.GetKey(KeyCode.D))
        {
            direction = 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction = -1;
        }
        move(direction, true);
        if (Input.GetKey(KeyCode.Space))
        {
            jump();
        }
    }
}