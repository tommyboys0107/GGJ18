using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMap : MonoBehaviour {
    public static Color Player1;
    public static Color Player1Ally;
    public static Color Player2;
    public static Color Player2Ally;

    public Color player1;
    public Color player1Ally;
    public Color player2;
    public Color player2Ally;

    void Awake()
    {
        Player1 = player1;
        Player1Ally = player1Ally;
        Player2 = player2;
        Player2Ally = player2Ally;
    }
}
