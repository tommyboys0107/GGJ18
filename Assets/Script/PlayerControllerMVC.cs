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

        Ball ball;
        PlayerCollisionHandler playerCollisionHandler;
        PlayerCanvas playerCanvas;
        float currentPushForce = 0.0f;

        /// <summary>
        /// Start is called once on the frame when a script is enabled.
        /// </summary>
        void Start()
        {
            if (playerCanvas == null)
            {
                playerCanvas = Instantiate(playerCanvasPrefab, playerTransform).GetComponent<PlayerCanvas>();
                playerCollisionHandler = playerTransform.gameObject.AddComponent<PlayerCollisionHandler>();
                playerCollisionHandler.Controller = this;
                ball = playerTransform.gameObject.GetComponent<Ball>();
            }
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
                currentPushForce = Mathf.Min((currentPushForce + model.gainForceSpeed * Time.deltaTime), model.maxPushForce);
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
        }

        public void HandleCollision2D(Collision2D col)
        {
            ChangePlayer(col.gameObject);
        }

        void ChangePlayer(GameObject obj)
        {
            Ball objBall = obj.GetComponent<Ball>();

            if (objBall.BallTypeProperty != Ball.BallType.PLAYER1 && objBall.BallTypeProperty != Ball.BallType.PLAYER2)
            {
                playerTransform = obj.transform;
                playerRigid = obj.GetComponent<Rigidbody2D>();
                playerCanvas.transform.SetParent(playerTransform);
                playerCanvas.transform.localPosition = Vector3.zero;
                Destroy(playerCollisionHandler);
                playerCollisionHandler = playerTransform.gameObject.AddComponent<PlayerCollisionHandler>();
                playerCollisionHandler.Controller = this;
            }

            if (model.id == 1)
                switch (objBall.BallTypeProperty)
                {
                    case Ball.BallType.NONE:
                    case Ball.BallType.PLAYER1ALLY:
                    case Ball.BallType.PLAYER2ALLY:
                        ball.BallTypeProperty = Ball.BallType.PLAYER1ALLY;
                        objBall.BallTypeProperty = Ball.BallType.PLAYER1;
                        ball = objBall;
                        break;
                    case Ball.BallType.PLAYER2:
                        break;
                    default:
                        break;
                }
            else if (model.id == 2)
                switch (objBall.BallTypeProperty)
                {
                    case Ball.BallType.NONE:
                    case Ball.BallType.PLAYER1ALLY:
                    case Ball.BallType.PLAYER2ALLY:
                        ball.BallTypeProperty = Ball.BallType.PLAYER2ALLY;
                        objBall.BallTypeProperty = Ball.BallType.PLAYER2;
                        ball = objBall;
                        break;
                    case Ball.BallType.PLAYER1:
                        break;
                    default:
                        break;
                }

            
        }
    }
}
