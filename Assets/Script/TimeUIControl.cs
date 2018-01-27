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
    bool fir = false;
	// Update is called once per frame
	void Update () {
        //if(fir){
        //    fir = false;
        //}
	}

    public void ResetUI(){
        CountDown.SetActive(true);
        image1.fillAmount = 1f;
        image2.fillAmount = 1f;
        image3.fillAmount = 1f;
        BG.SetActive(true);
        GoTitle.SetActive(false);
    }

    /// <summary>
    /// 累加時間
    /// </summary>
    /// <param name="Addtime">Addtime.</param>
    public void DownTime(float Addtime){
        //Debug.Log(Addtime);
        if(Addtime<=1){
            image1.fillAmount =1- Addtime;
        }else if(Addtime<=2){
            image1.fillAmount = 0;
            image2.fillAmount = 1 - (Addtime-1);
        }else if(Addtime<3 ){
            image1.fillAmount = 0;
            image2.fillAmount = 0;
            image3.fillAmount = 1 - (Addtime-2);
        }else{
            CountDown.SetActive(false);

            BG.SetActive(false);
            GoTitle.SetActive(true);
            StartCoroutine(timedown());
        }

    }
    private void OnEnable()
    {
        ResetUI();
    }
    IEnumerator timedown(){
        A.StMo();
        B.StMo();
        yield return new WaitForSeconds(1);

        GoTitle.SetActive(false);
        GameControl.Instance.Fight();
        //start game
        //ResetUI();
    }

    public void setTime(float Addtime,float MaxTimef){
        float time = (Addtime > MaxTimef)?MaxTimef :Addtime/ MaxTimef;
        TimeBar.fillAmount = 1-time;
        TimeBar2.fillAmount = 1-time;
    }
}
