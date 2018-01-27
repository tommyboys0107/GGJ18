using System.Collections;
using UnityEngine.Assertions;
using UnityEngine;
using UnityEngine.UI;

namespace CliffLeeCL
{
    /// <summary>
    /// Control player's movement and rotation by user's input.
    /// </summary>
    public class PlayerControllerMVC : BaseController<PlayerModel>
    {   
        public Transform playerTransform;
        public Rigidbody2D playerRigid;
        public GameObject playerCanvas;
        public RectTransform arrowOrigin;
        public Image arrowFilled;

        float currentPushForce = 0.0f;

        /// <summary>
        /// Start is called once on the frame when a script is enabled.
        /// </summary>
        void Start()
        {
            
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        void Update()
        {
            if (!model.isRotationSet)
                arrowOrigin.transform.Rotate(0.0f, 0.0f, model.angularSpeed * Time.deltaTime, Space.Self);
            else
            {
                currentPushForce = Mathf.Min((currentPushForce + model.gainForceSpeed * Time.deltaTime), model.maxPushForce);
                arrowFilled.fillAmount = currentPushForce / model.maxPushForce;
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
                playerRigid.AddForce(arrowOrigin.right * currentPushForce, ForceMode2D.Impulse);
            }
            currentPushForce = 0.0f;
            model.isRotationSet = false;
        }
    }
}
