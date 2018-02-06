using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BtnUse : MonoBehaviour {
	public Animator anim;
	public float Detime;
	public GameStatus st;
    public GamePlayS GG;
	// Use this for initialization
	void Start () {
		
	}

    public void BtnEffect()
    {
        if (anim != null)
            anim.SetTrigger("DOWN");
        MusicControal.Instance.PlayerSounder(MusicTypeChose.ClickSound);
    }

	public void Change(){
        BtnEffect();
        StartCoroutine(GOOO(Detime));
    }

	IEnumerator GOOO(float time){
		yield return new WaitForSeconds (time);
        UIControl.Instance.GameSetting(GG);
		UIControl.Instance.ChangeUI (st);
       
	}

	
	// Update is called once per frame
	void Update () {
		
	}
}
