using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CliffLeeCL
{
    public class PlayerSwapUI : MonoBehaviour
    {
        [SerializeField]
        Animator UISelect;
        [SerializeField]
        Transform floorTint;
        [SerializeField]
        Transform rootWall;

        // Use this for initialization
        void Start()
        {
            EventManager.Instance.onGameStart += OnPlayerSwapped;
            EventManager.Instance.onPlayerSwapped += OnPlayerSwapped;
        }

        void OnDisable()
        {
            EventManager.Instance.onGameStart -= OnPlayerSwapped;
            EventManager.Instance.onPlayerSwapped -= OnPlayerSwapped;
        }

        void OnPlayerSwapped()
        {
            Vector3 swappedScale = new Vector3(1.0f, -1.0f, 1.0f);

            if(UISelect.gameObject.activeInHierarchy)
                UISelect.SetBool("IS_SWAP", GameControl.Instance.IsPlayerSwapped);
            floorTint.localScale = GameControl.Instance.IsPlayerSwapped ? swappedScale : Vector3.one;
            rootWall.localScale = GameControl.Instance.IsPlayerSwapped ? swappedScale : Vector3.one;
        }
    }
}
