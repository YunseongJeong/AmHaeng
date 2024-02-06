using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    private float speed;
    private float jumpSpeed;
    Rigidbody2D rigid2D;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setRigidbody2D()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
    }

    public void setMass(int mass)
    {
        this.rigid2D.mass = mass;
    }

    public void setSpeed(float speed)
    {
        this.speed = speed;
    }
    public void setJumpSpeed(float jumpSpeed)
    {
        this.jumpSpeed = jumpSpeed;
    }

    public void jump()
    {
        this.rigid2D.velocity = new Vector2(this.rigid2D.velocity.x, this.jumpSpeed);
    }

    public void move(int direction, bool isRunning)
    {
        if (direction != 0)
        {
            transform.localScale = new Vector3(direction*0.3f, 0.3f, 1);
        }
        this.rigid2D.velocity = new Vector2(direction * this.speed, this.rigid2D.velocity.y);
    }
}
