using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LoopTexture : MonoBehaviour {

	public List<Sprite> LoopT;
	Image thisImage;
	public float changeTime=1f;
	[Header("Loop 次數 五為無限ＬＯＯＰ")]
	public int LoopTime;
	public bool bTloop=false;
	void Awake(){
		if(GetComponent<Image>()!=null)
		thisImage=this.GetComponent<Image>();
	}
	// Use this for initialization
	void OnEnable () {
		StartCoroutine (LoopStart());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	int TNumber=0;
	IEnumerator LoopStart(){
		while (thisImage!=null && LoopTime==5 && LoopTime>0)
		{
			if(bTloop){
				if(LoopT.Count<=TNumber+1){
					TNumber=0;
					if(LoopTime<5)
					LoopTime--;
				}else
				{
					TNumber++;

				}

				thisImage.sprite=LoopT[TNumber];
			}
			yield return new WaitForSeconds(changeTime);
		}
	}
}
