using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CliffLeeCL
{
    public class PlayerCollisionHandler : MonoBehaviour
    {
        PlayerControllerMVC controller;

        public PlayerControllerMVC Controller
        {
            set
            {
                controller = value;
            }
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Ball"))
                controller.HandleCollision2D(col);
        }
    }
}
