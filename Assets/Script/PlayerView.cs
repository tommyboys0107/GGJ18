using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;
using UnityEngine;

namespace CliffLeeCL {
    public class PlayerView : BaseView<PlayerModel, PlayerControllerMVC>
    {

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
        }

        /// <summary>
        /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
        /// </summary>
        void FixedUpdate()
        {
        }

        public void InputTap()
        {
            if (model.isRotationSet)
                controller.Shoot();
            else
                controller.SetRotation();
        }
    }
}
