using UnityEngine;

namespace CliffLeeCL
{
    /// <summary>
    /// Define each player's basic status.
    /// </summary>
    public class PlayerModel : BaseModel
    {
        /// <summary>
        /// Define how much the max push force is.
        /// </summary>
        public float maxPushForce = 10.0f;
        /// <summary>
        /// Define how fast the player rotates.
        /// </summary>
        [Tooltip("(Degree/Second)")]
        public float angularSpeed = 80.0f;
        /// <summary>
        /// The time the player will be stop after colliding with another player.
        /// </summary>
        public float stopTime = 0.5f;
        public bool isRotationSet = false;
    }
}
