using UnityEngine;

namespace CliffLeeCL
{
    /// <summary>
    /// This singleton class manage all events in the game.
    /// </summary>
    /// <code>
    /// // Usage in other class example:\n
    /// void Start(){\n
    ///     EventManager.instance.onGameOver += LocalFunction;\n
    /// }\n
    /// \n
    /// // If OnEnable function will cause error, try listen to events in Start function.\n
    /// void OnEnable(){\n
    ///     EventManager.instance.onGameOver += LocalFunction;\n
    /// }\n
    /// \n
    /// void OnDisable(){\n
    ///     EventManager.instance.onGameOver -= LocalFunction;\n
    /// }\n
    /// \n
    /// void LocalFunction(){\n
    ///     //Do something here\n
    /// }
    /// </code>
    public class EventManager : Singleton<EventManager>
    {
        /// <summary>
        /// Define default event's function signature.
        /// </summary>
        public delegate void DefaultEventHandler();

        /// <summary>
        /// The event is called when game start.
        /// </summary>
        public event DefaultEventHandler onGameStart;
        /// <summary>
        /// The event is called when game over.
        /// </summary>
        /// <seealso cref="OnGameOver"/>
        public event DefaultEventHandler onGameOver;
        /// <summary>
        /// The event is called when a player scored. 
        /// </summary>
        /// <seealso cref="OnPlayerScored"/>
        public event DefaultEventHandler onPlayerScored;
        /// <summary>
        /// The event is called when the players swapped.
        /// </summary>
        public event DefaultEventHandler onPlayerSwapped;

        /// <summary>
        /// The function is called when game start.
        /// </summary>
        public void OnGameStart()
        {
            if (onGameStart != null)
                onGameStart();
            Debug.Log("OnGameStart event is invoked!");
        }

        /// <summary>
        /// The function is called when a player scored.
        /// </summary>
        /// <seealso cref="onGameOver"/>
        public void OnGameOver()
        {
            if (onGameOver != null)
                onGameOver();
            Debug.Log("OnGameOver event is invoked!");
        }

        /// <summary>
        /// The function is called when a player scored.
        /// </summary>
        /// <seealso cref="onPlayerScored"/>
        public void OnPlayerScored()
        {
            if (onPlayerScored != null)
                onPlayerScored();
        }

        /// <summary>
        /// The function is called when the players swapped.
        /// </summary>
        public void OnPlayerSwapped()
        {
            if (onPlayerSwapped != null)
                onPlayerSwapped();
        }
    }
}

