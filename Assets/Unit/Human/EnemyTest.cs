using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : Human
{

    int direction = 0;
    public Animator m_animator;

    float attackDelay;

    private int nextPattern = MOVE;
    private int speed = 1;
    private static readonly int NONE = 0;
    private static readonly int MOVE = 1;
    private static readonly int ATTACK = 2;
    private static readonly int DEFENSE = 3;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        m_animator = this.GetComponent<Animator>();
        setRigidbody2D(this.GetComponent<Rigidbody2D>());
        setSpeed(speed);
        setJumpSpeed(15);
        setMass(100);
        setGroundSensor(new Vector2(0, 0.5f), new Vector2(1, 0.2f));
        isGrounded();
        setAnimator(m_animator);
        //nextPatternPlay();
    }

    private float CheckPlayerPos()
    {
        float playerX = player.GetComponent<Transform>().position.x;
        float myX = this.GetComponent<Transform>().position.x;

        float deltaX = playerX - myX;
        if (Mathf.Abs(deltaX) >= 10)
        {
            deltaX = float.MaxValue;
        }

        return deltaX;
    }

    IEnumerator move()
    {
        Debug.Log("EnemyTest:walk | move start");
        moveTo(-1, 10, false);
        int time = Mathf.Abs((int)Mathf.Round(10 / speed));
        yield return new WaitForSeconds(time + 1);
        moveTo(1, 10, false);
        yield return new WaitForSeconds(time + 1);
        Debug.Log("EnemyTest:walk | move end");
        nextPatternPlay();
    }

    IEnumerator attack()
    {
        Attack();
        yield return new WaitForSeconds(0.5f);
        nextPatternPlay();
    }

    IEnumerator defense()
    {
        Defense();
        yield return new WaitForSeconds(1f);
        EndDefense();
        nextPatternPlay();
    }
    void nextPatternPlay()
    {
        switch (nextPattern)
        {
            case 1:
                StartCoroutine(move());
                break;
            case 2:
                StartCoroutine(attack());
                break;
            case 3:
                StartCoroutine(defense());
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
       
        attackDelay -= Time.deltaTime;
        if (attackDelay < 0) attackDelay = 0;

        float distance = CheckPlayerPos();

        if (Mathf.Abs(distance) < 10 && Mathf.Abs(distance) > 3)
        {
            int direction;
            if (distance < 0)
            {
                direction = -1;
            }
            else
            {
                direction = 1;
            }

            move(direction, false);

        }
        else if (Mathf.Abs(distance) < 3 && attackDelay == 0)
        {
            Attack();
            attackDelay = 3f;
        }

    }
}
