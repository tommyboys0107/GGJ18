using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimeUIControl : MonoBehaviour {

    public Image image1;
    public Image image2;
    public Image image3;

    public Image TimeBar;
    public Image TimeBar2;

    public MoveGameOb A;
    public MoveGameOb B;
    public GameObject CountDown; 
    public GameObject GoTitle; 
    public GameObject BG; 
	// Use this for initialization
	void Start () {
		
	}
    public bool fir = false;
    float Addtime = 0;
	// Update is called once per frame
	void Update () {
        if (fir)
        {
            if (image1.fillAmount != 0)
            {
               // Debug.Log("F1 " + image1.fillAmount);
               // image1.fillAmount -=  0.01f;
              //  if (image2.fillAmount == 0)
               // {
              //      Debug.Log("F2: -");
               // }
            }
            else if (image2.fillAmount != 0)
            {
               // Debug.Log("F2 "+ image2.fillAmount);
               // image2.fillAmount -= 0.01f;
               // if (image2.fillAmount == 0)
               // {
                //    Debug.Log("F2: -");
              //  }
            }
            else if (image3.fillAmount != 0)
            {
               // Debug.Log("F3 " + image3.fillAmount);
                //image3.fillAmount -= 0.01f;
            }
            else if (image3.fillAmount == 0)
            {
                fir = false;
                CountDown.SetActive(false);
                First = true;
                BG.SetActive(false);
                GoTitle.SetActive(true);
                StartCoroutine(timedown());
            }
            
        }
    }
    
    public void ResetUI(){
        CountDown.SetActive(true);
        image1.fillAmount = 1f;
        image2.fillAmount = 1f;
        image3.fillAmount = 1f;
        BG.SetActive(true);
        GoTitle.SetActive(false);
    }
    //三次語音時間  => .6 1.5  2.3
    float timebase1 = .5f;
    float timebase2 = 1.0f;
    float timebase3 = 1.5f;
    /// <summary>
    /// 累加時間
    /// </summary>
    /// <param name="Addtime">Addtime.</param>
    public void DownTime(float Addtime){
        fir = true;
        //if(Addtime<= timebase1)
        //{
        //    Debug.Log("F1: "+Addtime);
        //    image1.fillAmount =1- 0.05f;
        //}else if(Addtime<= timebase2)
        //{
        //    Debug.Log("F2: " + Addtime);
        //    Addtime -= timebase1;
        //    //image1.fillAmount = 0;
        //    image2.fillAmount = 1 - 0.08f;
        //}else if(Addtime< timebase3)
        //{
        //    Debug.Log("F3: " + Addtime);
        //    Addtime -= timebase1;
        //    Addtime -= timebase2;
        //   // image1.fillAmount = 0;
        //   // image2.fillAmount = 0;
        //    image3.fillAmount = 1 - 0.05f;
        //}else if (image3.fillAmount==0)
        //{
        //    CountDown.SetActive(false);
        //    First = true;
        //    BG.SetActive(false);
        //    GoTitle.SetActive(true);
        //    StartCoroutine(timedown());
        //}
    }


    private void OnEnable()
    {
        ResetUI();
    }
    bool First = true;
    IEnumerator timedown(){
        if(A!=null)
        A.StMo();
        if(B!=null)
        B.StMo();
        yield return new WaitForSeconds(1);

        GoTitle.SetActive(false);
        if (First)
        {
            First = false;
            GameControl.Instance.Fight();

           
        }
        //start game
        //ResetUI();
    }
    bool firs = true;
    public void setTime(float Addtime,float MaxTimef){
        float time = (Addtime > MaxTimef)?MaxTimef :Addtime/ MaxTimef;
        //Debug.Log(MaxTimef+" "+Addtime);
        if(firs && MaxTimef-Addtime<5){
            MusicControal.Instance.PlayerSounder(MusicTypeChose.FivesSound);
            firs = false;
        }
        TimeBar.fillAmount = 1-time;
        TimeBar2.fillAmount = 1-time;
    }
}
