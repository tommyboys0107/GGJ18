﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CliffLeeCL;
using System;

public enum GameStatus{
	UIMainMenu,
	UISelect,
	UIResults,
	UIScores,
	UITimer,
	//GameUI,
	//EndUI,
}

public enum GamePlayS{
    MainG,
    WaitG,
    PlayG,
    EndG,
    FightG,
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
    public TimeUIControl _TimeUIControl;
    public SourscUI _SourscUI;


	void Awake(){
		UIControl.Instance._init ();
		Point.Instance._init();
		MusicControal.Instance._init();
		if (GameTime==null)
			this.gameObject.AddComponent<Timer> ();

		GameTime=this.gameObject.GetComponent<Timer> ();
	}

    public void Fight(){
        GameTime.StartCountDownTimer(30f, false, gamedatavoid);
    }

	// Use this for initialization
	void Start () {
		GameTime.eventStartCallBack += tt2;
		GameTime.eventEndCallBck += tt;
	}
    public bool fir=false;
	//bool startInto=false;

	float gamedataf=0;
	Timer.TimeIsUpHandler[] gamedatavoid;
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.G)) {
			UIControl.Instance.ChangeUI (GameStatus.UISelect);
		}
		//if (startInto) {
		//	startInto = false;
		//	StartCoroutine (st (3, gamedataf, gamedatavoid));
		//}
		//Debug.Log (GameTime.remainTime);
	}
    public void C(){
        StartCoroutine(StartOnePlayer(3, new Timer.TimeIsUpHandler[] { tt }));
    }
    IEnumerator StartOnePlayer(float Detim,Timer.TimeIsUpHandler[] data ){
        gamedataf = Detim;
        gamedatavoid = data;
        float addtime=0f;
        Debug.Log("Detim");
        while (Detim >= addtime){
            yield return new WaitForEndOfFrame();
            addtime += Time.deltaTime;
            _TimeUIControl.DownTime(addtime);
            //addtime += Time.deltaTime;
        }

        yield return new WaitForEndOfFrame();
        //GameTime.StartCountDownTimer(30f, false, data);
    }

	IEnumerator st(float waittime,float ftime,Timer.TimeIsUpHandler[] enffun){
		yield return new WaitForSeconds (waittime);
		GameTime.StartCountDownTimer (ftime, false, enffun);
	}
	public void IntoTitle(){
		UIControl.Instance.ChangeUI (GameStatus.UIMainMenu);
	}

	void tt2(){
        Debug.Log("start");
	}
	void tt(){
		Debug.Log ("end");
	}

    public void GameEnd(int Player1Point,int Player2Point){
        UIControl.Instance.ChangeUI(GameStatus.UIScores);
        _SourscUI.SetTime(1,Player1Point);
        _SourscUI.SetTime(2, Player2Point);
    }
}


public class UIControl:Singleton<UIControl>{

	public  GameObject _rootUI;

	public  Dictionary<GameStatus,GameObject> ScreenUI=new Dictionary<GameStatus, GameObject>();

	private GameStatus nowpoen= GameStatus.UIMainMenu;

    private List<GameStatus> NotDefaultOpenUI = new List<GameStatus>();

	public void _init (){
		if (GameObject.Find ("RootUI") != null)
			_rootUI = GameObject.Find ("RootUI");
		else
			_rootUI=GameObject.Instantiate (Resources.Load ("RootUI")as GameObject);

        foreach (GameStatus name in Enum.GetValues(typeof(GameStatus)))
        {
           
            if (_rootUI.transform.GetChild(0).Find(name.ToString()) != null)
            {
                ScreenUI.Add(name, _rootUI.transform.GetChild(0).Find(name.ToString()).gameObject);
                Debug.Log(name.ToString()+" Fir");
            }
            else
            {
                ScreenUI.Add(name, GameObject.Instantiate(Resources.Load("UI/" + name.ToString()), _rootUI.transform.GetChild(0)) as GameObject);
                Debug.Log(name.ToString() + " Sec");
            }
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
            //Debug.Log("Open :"+_nextStatus );
			ScreenUI [_nextStatus].SetActive(true);
            if(NotDefaultOpenUI!=null && NotDefaultOpenUI.Count>0){
                foreach(var a in NotDefaultOpenUI){
                    ScreenUI[a].SetActive(false);
                }
                NotDefaultOpenUI.Clear();
            }
            if (_nextStatus != nowpoen)
            {
                ScreenUI[nowpoen].SetActive(false);
                //Debug.Log("Close :" + nowpoen);
                nowpoen = _nextStatus;
            }
			//if(_nextStatus!=nowpoen)
				//nowpoen = _nextStatus;
			Debug.Log (string.Format("Open UI : {0}",_nextStatus.ToString()));
		}

	}

    public void OpenOneUI(GameStatus _nextStatus){
        if (ScreenUI.ContainsKey(_nextStatus))
        {
            ScreenUI[_nextStatus].SetActive(true);
            NotDefaultOpenUI.Add(_nextStatus);
        }
    }

    public void GameSetting(GamePlayS Ga){
        Debug.Log(Ga);
        switch (Ga)
        {
            case GamePlayS.MainG:
                
                break;
            case GamePlayS.WaitG:
                
                break;
            case GamePlayS.PlayG:
                GameControl.Instance.C();
                break;
            case GamePlayS.EndG:
                
                break;
            case GamePlayS.FightG:
               
                break;
            default:
                break;
        }
    }

    public void GamePlayStart(){
        
    }
}
public enum MusicTypeChose{
	MainSound,
	GameStartSound,
	RomanceSound,
	EndGameSound,
	PeaceEndSound,

	
	ClickSound,

	ReadySound,
	ShareYouLove,
	YouWin,
	LovePeace, 

	SavePowerSound,
	SpeedShotSound,
	ColliderSound,
	ColliderKissSound,
	ColliderWallSound,
	PlayerHitPlayerSound,

	FivesSound,

	StartGameThreeSound,

	GameStopMusic,
}

public class MusicControal:Singleton<MusicControal>{

	Dictionary<MusicTypeChose,AudioClip> GameAllMusic=new Dictionary<MusicTypeChose,AudioClip>();
	public void _init(){
		GameAllMusic.Add(MusicTypeChose.MainSound,GameControl.Instance.MainSound);
		GameAllMusic.Add(MusicTypeChose.GameStartSound,GameControl.Instance.GameStartSound);
		GameAllMusic.Add(MusicTypeChose.RomanceSound,GameControl.Instance.RomanceSound);
		GameAllMusic.Add(MusicTypeChose.EndGameSound,GameControl.Instance.EndGameSound);
		GameAllMusic.Add(MusicTypeChose.PeaceEndSound,GameControl.Instance.PeaceEndSound);
		
		GameAllMusic.Add(MusicTypeChose.ClickSound,GameControl.Instance.ClickSound);
		
		GameAllMusic.Add(MusicTypeChose.ReadySound,GameControl.Instance.ReadySound);
		GameAllMusic.Add(MusicTypeChose.ShareYouLove,GameControl.Instance.ShareYouLove);
		GameAllMusic.Add(MusicTypeChose.YouWin,GameControl.Instance.YouWin);
		GameAllMusic.Add(MusicTypeChose.LovePeace,GameControl.Instance.LovePeace);
	
		GameAllMusic.Add(MusicTypeChose.SavePowerSound,GameControl.Instance.SavePowerSound);
		GameAllMusic.Add(MusicTypeChose.SpeedShotSound,GameControl.Instance.SpeedShotSound);
		GameAllMusic.Add(MusicTypeChose.ColliderSound,GameControl.Instance.ColliderSound);
		GameAllMusic.Add(MusicTypeChose.ColliderKissSound,GameControl.Instance.ColliderKissSound);
		GameAllMusic.Add(MusicTypeChose.ColliderWallSound,GameControl.Instance.ColliderWallSound);
		GameAllMusic.Add(MusicTypeChose.PlayerHitPlayerSound,GameControl.Instance.PlayerHitPlayerSound);
	
		GameAllMusic.Add(MusicTypeChose.FivesSound,GameControl.Instance.FivesSound);
	
		GameAllMusic.Add(MusicTypeChose.StartGameThreeSound,GameControl.Instance.StartGameThreeSound);
	
		GameAllMusic.Add(MusicTypeChose.GameStopMusic,GameControl.Instance.GameStopMusic);
		
	}
	public void PlayerSounder(MusicTypeChose mosiutype){
		switch (mosiutype)
		{
			case MusicTypeChose.MainSound:
			case MusicTypeChose.GameStartSound:
			case MusicTypeChose.RomanceSound:
			case MusicTypeChose.EndGameSound:
			case MusicTypeChose.PeaceEndSound:
			break;
			case MusicTypeChose.ClickSound:
			break;
			case MusicTypeChose.ReadySound:
			case MusicTypeChose.ShareYouLove:
			case MusicTypeChose.YouWin:
			case MusicTypeChose.LovePeace:
			break;
			case MusicTypeChose.SavePowerSound:
			case MusicTypeChose.SpeedShotSound:
			case MusicTypeChose.ColliderSound:
			case MusicTypeChose.ColliderKissSound:
			case MusicTypeChose.ColliderWallSound:
			case MusicTypeChose.PlayerHitPlayerSound:
			break;
			case MusicTypeChose.FivesSound:
			break;
			case MusicTypeChose.StartGameThreeSound:
			break;
			case MusicTypeChose.GameStopMusic:
			break;
			default:
			break;
		}
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