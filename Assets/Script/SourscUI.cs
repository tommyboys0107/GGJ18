using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SourscUI : MonoBehaviour {
    public Sprite win;
    public Sprite Lose;
    public Text Scro1;
    public Text Scro2;
    public Image Scr1;
    public Image Scr2;
    public float scoreRollingSpeed = 10.0f;
    static string St = "Scro1";
    private void OnEnable()
    {
        Scro1.text = St;
        Scro2.text = St;
        Check1 = false;
        Check2 = false;
    }
    bool Check1 = false;
    bool Check2 = false;
    // Use this for initialization
    void Start () {
		
	}
    bool fir = true;
	// Update is called once per frame
	void Update () {
        //if(fir){
        //    fir = false;
        //    SetTime(1, 10);
        //    SetTime(2, 20);
        //}
	}

    public void SetTime(int who ,int point,bool bwin){
        switch (who)
        {
            case 1:
                Scro1.text = "0";
                StartCoroutine(PointUp(Scro1,0,(float)point));
                if(bwin)
                    Scr1.sprite = win;
                else 
                    Scr1.sprite = Lose;
                break;
            case 2:
                Scro2.text = "0";
                StartCoroutine(PointUp(Scro2,0,(float)point));
                if (bwin)
                    Scr2.sprite = win;
                else
                    Scr2.sprite = Lose;

                break;
            default:
                break;
        }
    }

    IEnumerator PointUp(Text who,float startPo,float point){
        yield return new WaitForSeconds(0.5f);

        while (startPo != point)
        {
            if (Mathf.Abs(point - startPo) < 1.2)
            {
                who.text = point.ToString("##");
                if (who == Scro1)
                    Check1 = true;
                else
                    Check2 = true;
                if(Check1 && Check2)
                UIControl.Instance.OpenOneUI(GameStatus.UIResults);
                //GameControl.Instance.AudioAll[0].Stop();
            }
            else
            {
                startPo += 1f * scoreRollingSpeed * Time.deltaTime;
                who.text = startPo.ToString("##");
            }
            yield return new WaitForEndOfFrame();
        }
    }

}
