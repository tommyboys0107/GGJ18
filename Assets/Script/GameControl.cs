﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CliffLeeCL;
using System;

public enum GameStatus{
	UIMainMenu,
	UISelect,
	//GameUI,
	//EndUI,
}

public class GameControl :  SingletonMono<GameControl>{

	[Header("BGM Music")]
	public AudioClip MainSound;
	public AudioClip GameStartSound;
	public AudioClip RomanceSound;
	public AudioClip EndGameSound;
	public AudioClip PeaceEndSound;
	
	[Header("buttom Music")]
	public AudioClip ClickSound;


	[Header("Talk Music")]
	public AudioClip ReadySound;
	public AudioClip ShareYouLove;
	public AudioClip YouWin;
	public AudioClip LovePeace; 

	[Header("Gmae Play Music")]
	public AudioClip SavePowerSound;
	public AudioClip SpeedShotSound;
	public AudioClip ColliderSound;
	public AudioClip ColliderKissSound;
	public AudioClip ColliderWallSound;
	public AudioClip PlayerHitPlayerSound;

	[Header("剩下五秒 Music")]
	public AudioClip FivesSound;

	[Header("倒數三秒開始 Music")]
	public AudioClip StartGameThreeSound;

	[Header("遊戲時間到 Music")]
	public AudioClip GameStopMusic;
	

	public Timer GameTime;

	void Awake(){
		UIControl.Instance._init ();
		Point.Instance._init();
		MusicControal.Instance._init();
		if (GameTime==null)
			this.gameObject.AddComponent<Timer> ();

		GameTime=this.gameObject.GetComponent<Timer> ();
	}



	// Use this for initialization
	void Start () {
		GameTime.eventStartCallBack += tt2;
		GameTime.eventEndCallBck += tt;
	}
	bool fir=true;
	bool startInto=false;

	float gamedataf=0;
	Timer.TimeIsUpHandler[] gamedatavoid;
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.G)) {
			UIControl.Instance.ChangeUI (GameStatus.UISelect);
		}
		if (fir) {
			fir = false;
			StartOnePlayer (3, new Timer.TimeIsUpHandler[]{tt});
		}
		if (startInto) {
			startInto = false;
			StartCoroutine (st (3, gamedataf, gamedatavoid));
		}
		//Debug.Log (GameTime.remainTime);
	}

	public void StartOnePlayer(float ftime,Timer.TimeIsUpHandler[] enffun){
		//UIControl.Instance.ChangeUI (GameStatus.GameUI);
		gamedataf=ftime;
		gamedatavoid = enffun;
		startInto = true;
		//StartCoroutine (st (3, ftime, enffun));

	}
	IEnumerator st(float waittime,float ftime,Timer.TimeIsUpHandler[] enffun){
		yield return new WaitForSeconds (waittime);
		GameTime.StartCountDownTimer (ftime, false, enffun);
	}
	public void IntoTitle(){
		UIControl.Instance.ChangeUI (GameStatus.UIMainMenu);
	}

	void tt2(){
		Debug.Log ("eff");
	}
	void tt(){
		Debug.Log ("endff");
	}
}


public class UIControl:Singleton<UIControl>{

	public  GameObject _rootUI;

	public  Dictionary<GameStatus,GameObject> ScreenUI=new Dictionary<GameStatus, GameObject>();

	private GameStatus nowpoen= GameStatus.UIMainMenu;



	public void _init (){
		if (GameObject.Find ("RootUI") != null)
			_rootUI = GameObject.Find ("RootUI");
		else
			_rootUI=GameObject.Instantiate (Resources.Load ("RootUI")as GameObject);

		foreach(GameStatus name in Enum.GetValues(typeof(GameStatus))){
			Debug.Log (name.ToString());
			if (_rootUI.transform.GetChild(0).Find (name.ToString()) != null)
				ScreenUI.Add (name,_rootUI.transform.GetChild(0).Find (name.ToString()).gameObject);
			else
				ScreenUI.Add (name,GameObject.Instantiate(Resources.Load("UI/"+name.ToString()),_rootUI.transform.GetChild(0))as GameObject);
		}
		foreach (KeyValuePair<GameStatus,GameObject> UI in ScreenUI) {
			UI.Value.SetActive (false);
		}
		ChangeUI (nowpoen);
		//GameControl.Instance.GameTime.UIStartCallBack 
		//GameControl.Instance.GameTime.UIEndCallBack += ChangeUI(GameStatus); 
	}

	public void ChangeUI(GameStatus _nextStatus){
		if (ScreenUI.ContainsKey (_nextStatus)) {
			ScreenUI [_nextStatus].SetActive(true);
			if(_nextStatus!=nowpoen && nowpoen!=null)
				ScreenUI [nowpoen].SetActive(false);
			//if(_nextStatus!=nowpoen)
				//nowpoen = _nextStatus;
			Debug.Log (string.Format("Open UI : {0}",_nextStatus.ToString()));
		}
	}

}

public class MusicControal:Singleton<MusicControal>{
	public void _init(){

	}
}

public class Point:Singleton<Point>{
	
	Dictionary<int,int> PlayerPoint= new Dictionary<int,int>();
	public void _init(){
		PlayerPoint= new Dictionary<int,int>();
	}

	public void ChangePoint(int PlayerID,int Point){
		if(!PlayerPoint.ContainsKey(PlayerID))
			PlayerPoint.Add(PlayerID,Point);
		else
			PlayerPoint[PlayerID]+=Point;

	}

	public int getPlayerPoint(int PlayerID){
		return (!PlayerPoint.ContainsKey(PlayerID))?0:PlayerPoint[PlayerID];
	}

}

//
//interface UIFnu{
//	//void UIInit();
//	void UIOpen ();
//	void UIClose();
//}