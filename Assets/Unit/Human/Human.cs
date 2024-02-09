using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Human : MonoBehaviour
{
    private float speed;
    private float jumpSpeed;
    private Vector2 groundSensorPos;
    private Vector2 groundSensorhalfSize;
    Rigidbody2D rigid2D;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(new Vector3(this.rigid2D.position.x, this.rigid2D.position.y -1.5f, 0), new Vector3(1, 0.5f, 1));
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        OnDrawGizmos();
    }

    public void setRigidbody2D()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
    }

    public void setGroundSensor(Vector2 pos, Vector2 halfSize)
    {
        this.groundSensorPos = pos;
        this.groundSensorhalfSize = halfSize;
    }

    public bool isGrounded()
    { 
        Vector2 curGroundSensorPos = new Vector2(this.rigid2D.position.x + this.groundSensorPos.x, this.rigid2D.position.y + this.groundSensorPos.y);
        RaycastHit2D[] hits = Physics2D.BoxCastAll(curGroundSensorPos, this.groundSensorhalfSize, 0, new Vector2(0.0f, 0.0f));
        foreach(RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag("Ground"))
            {
                
                return true;
            }
        }
        return false;
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
