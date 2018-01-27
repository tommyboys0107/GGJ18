using UnityEngine;
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

    void Start()
    {
        UpdateBallColor();
    }

    void UpdateBallColor()
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
}
