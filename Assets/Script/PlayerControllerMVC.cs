using UnityEngine;

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
        bool isPlayerSwapping = false;

        /// <summary>
        /// Start is called once on the frame when a script is enabled.
        /// </summary>
        void Start()
        {
            collisionTimer = gameObject.AddComponent<Timer>();
            initialPlayerTransform = playerTransform;
            initialPlayerRigid = playerRigid;
            Initialize();
            EventManager.Instance.onGameStart += Initialize;
        }

        void OnDisable()
        {
            EventManager.Instance.onGameStart -= Initialize;
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

            if (model.id == 1)
                ball.BallTypeProperty = GameControl.Instance.IsPlayerSwapped ? Ball.BallType.PLAYER2 : Ball.BallType.PLAYER1;
            else if (model.id == 2)
                ball.BallTypeProperty = GameControl.Instance.IsPlayerSwapped ? Ball.BallType.PLAYER1 : Ball.BallType.PLAYER2;
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
            TransferControl(col.gameObject);
        }

        public void SetBall(Ball ball)
        {
            this.ball = ball;
        }

        public void SetOldBall(Ball ball)
        {
            oldBall = ball;
        }

        public void ChangeOwner(GameObject obj)
        {
            playerTransform = obj.transform;
            playerRigid = obj.GetComponent<Rigidbody2D>();
            playerCanvas.transform.SetParent(playerTransform);
            playerCanvas.transform.localPosition = Vector3.zero;
            Destroy(playerCollisionHandler);
            playerCollisionHandler = playerTransform.gameObject.AddComponent<PlayerCollisionHandler>();
            playerCollisionHandler.Controller = this;
        }

        void ChangePlayer(GameObject obj)
        {
            PlayerCollisionHandler playerCollision = obj.GetComponent<PlayerCollisionHandler>();

            isPlayerSwapping = true;
            playerCollision.Controller.isPlayerSwapping = true;
            ChangeOwner(obj);
            playerCollision.Controller.ChangeOwner(ball.gameObject);
            playerCollision.Controller.SetOldBall(playerCollision.Controller.ball);
            playerCollision.Controller.SetBall(ball);
            playerCollision.Controller.isPlayerSwapping = false;
            isPlayerSwapping = false;
        }

        bool IsPlayerSwapped()
        {
            if ((model.id == 1) && (ball.ballType == Ball.BallType.PLAYER1) ||
            (model.id == 2) && (ball.ballType == Ball.BallType.PLAYER2))
                return false;
            else
                return true;
        }

        void TransferControl(GameObject obj)
        {
            Ball objBall = obj.GetComponent<Ball>();

            if (objBall != oldBall && !isPlayerSwapping)
            {
                if (ball.BallTypeProperty == Ball.BallType.PLAYER1)
                    switch (objBall.BallTypeProperty)
                    {
                        case Ball.BallType.NONE:
                        case Ball.BallType.PLAYER1ALLY:
                        case Ball.BallType.PLAYER2ALLY:
                            Debug.Log("Collision: P1 (R) to non-player.");
                            ball.BallTypeProperty = Ball.BallType.PLAYER1ALLY;
                            objBall.BallTypeProperty = Ball.BallType.PLAYER1;
                            ball.UpdateBallFace();
                            objBall.UpdateBallFace();
                            ball.UpdateBallFaceCollision();
                            objBall.UpdateBallFaceCollision();
                            Instantiate(collisionParticlePrefab, ball.transform);
                            ChangeOwner(obj);
                            oldBall = ball;
                            ball = objBall;
                            break;
                        case Ball.BallType.PLAYER2:
                            Debug.Log("Collision: P1 (R) to P2 (B).");
                            Instantiate(collisionParticlePrefab, ball.transform);
                            ChangePlayer(obj);
                            oldBall = ball;
                            ball = objBall;
                            GameControl.Instance.IsPlayerSwapped = IsPlayerSwapped();
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
                            Debug.Log("Collision: P2 (B) to non-player.");
                            ball.BallTypeProperty = Ball.BallType.PLAYER2ALLY;
                            objBall.BallTypeProperty = Ball.BallType.PLAYER2;
                            ball.UpdateBallFace();
                            objBall.UpdateBallFace();
                            ball.UpdateBallFaceCollision();
                            objBall.UpdateBallFaceCollision();
                            Instantiate(collisionParticlePrefab, ball.transform);
                            ChangeOwner(obj);
                            oldBall = ball;
                            ball = objBall;
                            break;
                        case Ball.BallType.PLAYER1:
                            Debug.Log("Collision: P2 (B) to P1 (R).");
                            Instantiate(collisionParticlePrefab, ball.transform);
                            ChangePlayer(obj);
                            oldBall = ball;
                            ball = objBall;
                            GameControl.Instance.IsPlayerSwapped = IsPlayerSwapped();
                            break;
                        default:
                            break;

                    }
            }
            else
            {
                collisionTimer.StartCountDownTimer(0.05f, false, false, OnTimeIsUp);
            }
        }

        void OnTimeIsUp()
        {
            oldBall = null;
        }
    }
}
