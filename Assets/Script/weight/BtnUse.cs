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
	public void Change(){
		Debug.Log("Tri");
		if(anim!=null)
			anim.SetTrigger("DOWN");
		StartCoroutine(GOOO(Detime));
        MusicControal.Instance.PlayerSounder(MusicTypeChose.ClickSound);
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
