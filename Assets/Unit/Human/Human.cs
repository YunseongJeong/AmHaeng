using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
//using static UnityEditor.PlayerSettings;

public class Human : MonoBehaviour
{
    private float speed;
    private float jumpSpeed;
    private Vector2 groundSensorPos;
    private Vector2 groundSensorhalfSize;
    Rigidbody2D rigid2D;
    Animator m_Animator;
    private int direction = 1;
    private bool mustStopMove = false;
    private bool AllStopBool = false;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //OnDrawGizmos();
    }

    public void jumpAndFallAnimation()
    {
        if (!isGrounded())
        {
            if (this.rigid2D.velocity.y > 0)
            {
                if (!m_Animator.GetBool("isJumping"))
                {
                    UnityEngine.Debug.Log("Human:jumpAndFallAnimation | isJumping");
                    m_Animator.SetBool("isJumping", true);
                    m_Animator.SetBool("isFalling", false);
                }
            }
            else
            {
                if (!m_Animator.GetBool("isFalling"))
                {
                    UnityEngine.Debug.Log("Human:jumpAndFallAnimation | isFalling");
                    m_Animator.SetBool("isFalling", true);
                    m_Animator.SetBool("isJumping", false);
                }
            }
        }
        else
        {
            m_Animator.SetBool("isFalling", false);
            m_Animator.SetBool("isJumping", false);
        }
        
    }

    public void setRigidbody2D(Rigidbody2D rigid2D)
    {
        this.rigid2D = rigid2D;
    }

    public void setGroundSensor(Vector2 pos, Vector2 halfSize)
    {
        this.groundSensorPos = pos;
        this.groundSensorhalfSize = halfSize;
    }

    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(new Vector3(this.rigid2D.position.x, this.rigid2D.position.y + this.groundSensorPos.y, 0),new Vector3(this.groundSensorhalfSize.x, this.groundSensorhalfSize.y, 1));
    }
    

    public bool isGrounded()
    { 
        Vector2 curGroundSensorPos = new Vector2(
            this.rigid2D.position.x + this.groundSensorPos.x,
            this.rigid2D.position.y + this.groundSensorPos.y);

        RaycastHit2D[] hits = Physics2D.BoxCastAll(
            curGroundSensorPos,
            this.groundSensorhalfSize,
            0,
            new Vector2(0.0f, 0.0f));
        foreach(RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag("Ground"))
            {
                
                return true;
            }
        }
        return false;
    }

    public void setAnimator(Animator animator)
    {
        this.m_Animator = animator;
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

    public void Defense()
    {
        UnityEngine.Debug.Log("Human:Defense | isDefensing");
        m_Animator.SetBool("isDefensing", true);
    }
    public void EndDefense()
    {
        UnityEngine.Debug.Log("Human:EndDefense | isNotDefensing");
        m_Animator.SetBool("isDefensing", false);
    }

    public void Attack()
    {

        UnityEngine.Debug.Log("Human:Attack | isAttacking");
        
        StartCoroutine(Attack1());
    }

    IEnumerator Attack1()
    {
        stopMove();
        mustStopMove = true;
        m_Animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.2f);
        /*if (isGrounded())
        {
            int count = 0;

            while (count <= 30)
            {
                this.rigid2D.velocity = new Vector2(this.speed * 10 * this.direction, this.rigid2D.velocity.y);
                yield return new WaitForSeconds(0.003f);
                count += 1;
            }
            
        }
        */
        mustStopMove = false;
    }

    public int moveTo(int direction, float deltaX, bool isRunning)
    {
        this.direction = direction;
        if (direction != 0)
        {
            transform.localScale = new Vector3(direction * 0.3f, 0.3f, 1);
        }

        if (mustStopMove)
        {
            return 0;
        }
        float moveSpeed;
        if (isRunning)
        {
            moveSpeed = this.speed * 4 * direction;
        }
        else
        {
            moveSpeed = this.speed * direction;
        }
        int time = Mathf.Abs((int)Mathf.Round(deltaX / moveSpeed * 100));

        StartCoroutine(moveToE(time, moveSpeed, isRunning));

        return time;
    }

    IEnumerator moveToE(int time, float moveSpeed, bool isRunning)
    {
        

        if (isRunning)
        {
            m_Animator.SetBool("isRunning", true);
        }
        else
        {
            m_Animator.SetBool("isWalking", true);
        }

        while (time >= 0)
        {
            if (AllStopBool)
            {
                yield break;
            }
            time -= 1;
            this.rigid2D.velocity = new Vector2(moveSpeed, this.rigid2D.velocity.y);
            yield return new WaitForSeconds(0.01f);
        }

        if (isRunning)
        {
            m_Animator.SetBool("isRunning", false);
        }
        else
        {
            m_Animator.SetBool("isWalking", false);
        }

    }

    public void move(int direction, bool isRunning)
    {
        this.direction = direction;
        
        if (direction != 0)
        {
            transform.localScale = new Vector3(direction * 0.3f, 0.3f, 1);
        }

        if (mustStopMove)
        {
            return;
        }
        
        float moveSpeed;
        if (isRunning && !m_Animator.GetBool("isDefensing"))
        {
            moveSpeed = this.speed * 4;
        }
        else
        {
            moveSpeed = this.speed;
        }

        this.rigid2D.velocity = new Vector2(direction * moveSpeed, this.rigid2D.velocity.y);
        if (isGrounded())
        {
            if (!isRunning)
            {
                if (!m_Animator.GetBool("isWalking"))
                {
                    m_Animator.SetBool("isWalking", true);
                    m_Animator.SetBool("isRunning", false);
                }

            }
            else
            {
                if (!m_Animator.GetBool("isRunning"))
                {
                    m_Animator.SetBool("isRunning", true);
                    m_Animator.SetBool("isWalking", false);
                }

            }
        }
        else
        {
            m_Animator.SetBool("isWalking", false);
            m_Animator.SetBool("isRunning", false);
        }
        
    }
    public void stopMove()
    {
        this.rigid2D.velocity = new Vector2(0, this.rigid2D.velocity.y);
        m_Animator.SetBool("isWalking", false);
        m_Animator.SetBool("isRunning", false);
    }

    public void AllStop()
    {
        AllStopBool = true;
        this.rigid2D.velocity = new Vector2(0, this.rigid2D.velocity.y);
        m_Animator.SetBool("isWalking", false);
        m_Animator.SetBool("isRunning", false);
        m_Animator.SetBool("isJumping", false);
        m_Animator.SetBool("isFalling", false);
        AllStopBool = false;
    }
}
