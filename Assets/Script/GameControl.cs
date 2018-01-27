using System.Collections;
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

	public Timer GameTime;

	void Awake(){
		UIControl.Instance._init ();
		if (GameTime==null)
			this.gameObject.AddComponent<Timer> ();

		GameTime=this.gameObject.GetComponent<Timer> ();
	}



	// Use this for initialization
	void Start () {
		GameTime.StartCallBack += tt2;

	}
	bool fir=true;
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.G)) {
			UIControl.Instance.ChangeUI (GameStatus.UISelect);
		}
		if (fir) {
			fir = false;
			StartOnePlayer (3, new Timer.TimeIsUpHandler[]{tt});
		}
	}

	public void StartOnePlayer(float ftime,Timer.TimeIsUpHandler[] enffun){
		//UIControl.Instance.ChangeUI (GameStatus.GameUI);
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
			if (_rootUI.transform.Find (name.ToString()) != null)
				ScreenUI.Add (name,_rootUI.transform.Find (name.ToString()).gameObject);
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



//
//interface UIFnu{
//	//void UIInit();
//	void UIOpen ();
//	void UIClose();
//}