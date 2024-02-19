using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Human
{
    int direction = 0;
    public Animator m_animator;
    public GameObject outerwear;
    private Animator outerwearAnimator;
    // Start is called before the first frame update
    void Start()
    {
        setRigidbody2D(this.GetComponent<Rigidbody2D>());
        setSpeed(2);
        setJumpSpeed(15);
        setMass(100);
        setGroundSensor(new Vector2(0, -2.2f), new Vector2(1, 0.2f));
        isGrounded();
        setAnimator(m_animator);
        outerwearAnimator = outerwear.GetComponent<Animator>();
        outerwearAnimator.SetBool("moveOuter", true);
        
    }
    

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //Attack
            Attack();
            StartCoroutine(Delay(5f));
        }
        else
        {
            //Walk and run, jump Part
            jumpAndFallAnimation();
            bool isRunning = !Input.GetKey(KeyCode.LeftShift);
            direction = 0;
            if (Input.GetKey(KeyCode.D))
            {
                direction = 1;
                move(direction, isRunning);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                direction = -1;
                move(direction, isRunning);
            }
            else
            {
                stopMove();
            }

            if (Input.GetKey(KeyCode.Space))
            {
                if (isGrounded())
                {
                    jump();
                }

            }
        }
    }

    IEnumerator Delay(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
