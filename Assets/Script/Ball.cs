﻿using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour {
    public enum BallType
    {
        NONE = 0,
        PLAYER1 = 1,
        PLAYER1ALLY = 2,
        PLAYER2 = 3,
        PLAYER2ALLY = 4
    }
    public SpriteRenderer ballSprite;
    public BallType ballType = BallType.NONE;

    Rigidbody2D rigid;
    Animator animator = null;
    Vector3 initialPosition;
    Quaternion initialQuaternion;
    BallType initialBallType;

    public BallType BallTypeProperty
    {
        get
        {
            return ballType;
        }
        set
        {
            ballType = value;
            UpdateBallColor();
        }
    }

    void Awake()
    {
        if(animator == null)
        {
            animator = gameObject.GetComponentInChildren<Animator>();
        }

        if(rigid == null)
        {
            rigid = gameObject.GetComponent<Rigidbody2D>();
        }
    }

    void Start()
    {
        initialPosition = transform.localPosition;
        initialQuaternion = transform.localRotation;
        initialBallType = ballType;
        Initialize();
        GameControl.Instance.GameTime.eventEndCallBck += Initialize;
    }

    void Initialize()
    {
        transform.localPosition = initialPosition;
        transform.localRotation = initialQuaternion;
        ballType = initialBallType;
        rigid.velocity = Vector3.zero;
        UpdateBallColor();
        UpdateBallFace();
    }

    public void UpdateBallColor()
    {
        switch (ballType)
        {
            case BallType.NONE:
                ballSprite.color = ColorMap.None;
                break;
            case BallType.PLAYER1:
                ballSprite.color = ColorMap.Player1;
                break;
            case BallType.PLAYER1ALLY:
                ballSprite.color = ColorMap.Player1Ally;
                break;
            case BallType.PLAYER2:
                ballSprite.color = ColorMap.Player2;
                break;
            case BallType.PLAYER2ALLY:
                ballSprite.color = ColorMap.Player2Ally;
                break;
            default:
                Debug.LogError("Undefined ball type!");
                break;
        }
    }

    public void UpdateBallFace()
    {
        switch (ballType)
        {
            case BallType.NONE:
                animator.SetInteger("intFaceControl", 0);
                break;
            case BallType.PLAYER1:
                animator.SetInteger("intFaceControl", 1);
                transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                break;
            case BallType.PLAYER1ALLY:
                animator.SetInteger("intFaceControl", 0);
                transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                break;
            case BallType.PLAYER2:
                animator.SetInteger("intFaceControl", 2);
                transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);
                break;
            case BallType.PLAYER2ALLY:
                animator.SetInteger("intFaceControl", 0);
                transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);
                break;
            default:
                Debug.LogError("Undefined ball type!");
                break;
        }
    }

    public void UpdateBallFaceCollision()
    {
        animator.SetTrigger("collide");
    }
}
