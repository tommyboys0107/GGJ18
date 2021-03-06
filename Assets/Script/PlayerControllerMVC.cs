﻿using UnityEngine;

namespace CliffLeeCL
{
    /// <summary>
    /// Control player's movement and rotation by user's input.
    /// </summary>
    public class PlayerControllerMVC : BaseController<PlayerModel>
    {   
        public Transform playerTransform;
        public Rigidbody2D playerRigid;
        public GameObject playerCanvasPrefab;
        public GameObject collisionParticlePrefab;

        Transform initialPlayerTransform;
        Rigidbody2D initialPlayerRigid;
        Ball ball;
        Ball oldBall;
        PlayerCollisionHandler playerCollisionHandler;
        PlayerCanvas playerCanvas;
        Timer collisionTimer;
        float currentPushForce = 0.0f;
        bool isForceRising = true;

        /// <summary>
        /// Start is called once on the frame when a script is enabled.
        /// </summary>
        void Start()
        {
            collisionTimer = gameObject.AddComponent<Timer>();
            initialPlayerTransform = playerTransform;
            initialPlayerRigid = playerRigid;
            Initialize();
            GameControl.Instance.GameTime.eventEndCallBck += Initialize;
        }

        void Initialize()
        {
            playerTransform = initialPlayerTransform;
            playerRigid = initialPlayerRigid;
            if (playerCanvas != null)
            {
                playerCanvas.transform.SetParent(playerTransform);
                playerCanvas.transform.localPosition = Vector3.zero;
            }
            else
            {
                playerCanvas = Instantiate(playerCanvasPrefab, playerTransform).GetComponent<PlayerCanvas>();
            }
            if (playerCollisionHandler != null)
                Destroy(playerCollisionHandler);
            playerCollisionHandler = playerTransform.gameObject.AddComponent<PlayerCollisionHandler>();
            playerCollisionHandler.Controller = this;
            ball = playerTransform.gameObject.GetComponent<Ball>();
            currentPushForce = 0.0f;
            model.isRotationSet = false;
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        void Update()
        {
            if (!model.isRotationSet)
                playerCanvas.arrowOrigin.transform.Rotate(0.0f, 0.0f, model.angularSpeed * Time.deltaTime, Space.Self);
            else
            {
                if (isForceRising)
                {
                    currentPushForce = currentPushForce + model.gainForceSpeed * Time.deltaTime;
                    if (currentPushForce >= model.maxPushForce)
                        isForceRising = false;
                }
                else
                {
                    currentPushForce = currentPushForce - model.gainForceSpeed * Time.deltaTime;
                    if (currentPushForce <= 0.01f)
                        isForceRising = true;
                }
                playerCanvas.arrowFilled.fillAmount = currentPushForce / model.maxPushForce;
            }
        }

        /// <summary>
        /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
        /// </summary>
        void FixedUpdate()
        {

        }
        
        public void SetRotation()
        {
            model.isRotationSet = true;
        }

        public void Shoot()
        {
            if (playerRigid != null)
            {
                playerRigid.velocity = Vector2.zero;
                playerRigid.AddForce(playerCanvas.arrowOrigin.right * currentPushForce, ForceMode2D.Impulse);
            }
            currentPushForce = 0.0f;
            model.isRotationSet = false;
            MusicControal.Instance.PlayerSounder(MusicTypeChose.SpeedShotSound);
        }

        public void HandleCollision2D(Collision2D col)
        {
            ChangePlayer(col.gameObject);
        }

        void ChangePlayer(GameObject obj)
        {
            Ball objBall = obj.GetComponent<Ball>();

            if (objBall != oldBall)
            {
                if (ball.BallTypeProperty == Ball.BallType.PLAYER1)
                    switch (objBall.BallTypeProperty)
                    {
                        case Ball.BallType.NONE:
                        case Ball.BallType.PLAYER1ALLY:
                        case Ball.BallType.PLAYER2ALLY:
                            ChangeOwner(obj);
                            ball.BallTypeProperty = Ball.BallType.PLAYER1ALLY;
                            objBall.BallTypeProperty = Ball.BallType.PLAYER1;
                            ball.UpdateBallFace();
                            objBall.UpdateBallFace();
                            ball.UpdateBallFaceCollision();
                            objBall.UpdateBallFaceCollision();
                            Instantiate(collisionParticlePrefab, ball.transform);
                            oldBall = ball;
                            ball = objBall;
                            break;
                        case Ball.BallType.PLAYER2:
                            /*ChangeOwner(obj);
                            oldBall = ball;
                            ball = objBall;*/
                            break;
                        default:
                            break;
                    }
                else if (ball.BallTypeProperty == Ball.BallType.PLAYER2)
                    switch (objBall.BallTypeProperty)
                    {
                        case Ball.BallType.NONE:
                        case Ball.BallType.PLAYER1ALLY:
                        case Ball.BallType.PLAYER2ALLY:
                            ChangeOwner(obj);
                            ball.BallTypeProperty = Ball.BallType.PLAYER2ALLY;
                            objBall.BallTypeProperty = Ball.BallType.PLAYER2;
                            ball.UpdateBallFace();
                            objBall.UpdateBallFace();
                            ball.UpdateBallFaceCollision();
                            objBall.UpdateBallFaceCollision();
                            Instantiate(collisionParticlePrefab, ball.transform);
                            oldBall = ball;
                            ball = objBall;
                            break;
                        case Ball.BallType.PLAYER1:
                            /*ChangeOwner(obj);
                            oldBall = ball;
                            ball = objBall;*/
                            break;
                        default:
                            break;

                    }
            }
            else
            {
                collisionTimer.StartCountDownTimer(0.1f, false, false, OnTimeIsUp);
            }
        }

        void ChangeOwner(GameObject obj)
        {
            playerTransform = obj.transform;
            playerRigid = obj.GetComponent<Rigidbody2D>();
            playerCanvas.transform.SetParent(playerTransform);
            playerCanvas.transform.localPosition = Vector3.zero;
            Destroy(playerCollisionHandler);
            playerCollisionHandler = playerTransform.gameObject.AddComponent<PlayerCollisionHandler>();
            playerCollisionHandler.Controller = this;
        }

        void OnTimeIsUp()
        {
            oldBall = null;
        }
    }
}
