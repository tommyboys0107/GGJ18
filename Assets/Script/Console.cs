using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public enum MessageTypeLevel
{
    NormalLog = 0,
    Warning = 1,
    Exception = 2,
    Error=3,
    Assert=4,
    PlayerConsole = 5,
    AllMessage = 6,
}
public enum ScreenSize{
    Min=0,
    Max=1,
    Default=2,
}
public class MessageData{
    public MessageTypeLevel m_MessageTypeLevel = MessageTypeLevel.NormalLog;
    public string Message = "";
    public MessageData(string m_MessageString,MessageTypeLevel m_MessageTypeLevel=MessageTypeLevel.NormalLog){
        this.m_MessageTypeLevel = m_MessageTypeLevel;
        Message = m_MessageString;
    }
    public string getMessage(){
        return string.Format("指令等級：{0} ,訊息內容：{1}", m_MessageTypeLevel, Message);
    }
}

public class Console : MonoBehaviour {
    GUIContent m_MineWindows = new GUIContent("縮小", "Min Windows");
    GUIContent m_clearMessage = new GUIContent("清除", "Clean MessageLable");
    GUIContent m_PlayerMessage = new GUIContent("指令", "Call MessageLable");
    GUIContent m_closeMessage = new GUIContent("關閉", "Close MessageLable");
    // Use this for initialization
    //邊線距離
    public float m_SideDistance = 10f;
    //小黑窗距離底部距離倍數
    public float BottomDistanceMultiple = 2;
    Rect m_windowsRect;//= new Rect(m_Side,m_Side,500f,500f);
    Vector2 m_ScrollPos=new Vector2();
    public bool SettingNewRect = false;
    bool RectMine = false;
    ScreenSize _ScreenSize = ScreenSize.Default;
    bool MessageSwitch = false;

    string PlayerCall = "";
    MessageTypeLevel filterType = MessageTypeLevel.AllMessage;
    //Save data
    List<MessageData> MessageContainer = new List<MessageData>();
    //KeyCode Function
    Dictionary<KeyCode, Action> KeyFun = new Dictionary<KeyCode, Action>();
    GUIStyle LabelStyle=new GUIStyle();
	void Start () {
        KeyFun.Add(KeyCode.KeypadEnter,InputPlayerCall);
        KeyFun.Add(KeyCode.Return, InputPlayerCall);
	}

	private void init(){
		
	}

    private void OnEnable()
    {
        Application.logMessageReceived += HandLog;
        //m_windowsRect= new Rect(m_SideDistance,m_SideDistance,Screen.width-m_SideDistance*2,Screen.height-m_SideDistance*BottomDistanceMultiple);
        SettingNewRect = true;
        RectMine = false;
		LabelStyle.wordWrap = true;
		//BornUIObj.addCanvas ();
		//BornUIObj.addEventSystem ();
       // LabelStyle = new GUIStyle();
        //LabelStyle.fontSize = 30;
    }
    private void OnDisable()
    {
        Application.logMessageReceived -= HandLog;
    }

    // Update is called once per frame
    void Update () {
        if(SettingNewRect){
            SettingNewRect = false;

            if(RectMine)
                m_windowsRect= new Rect(m_SideDistance,m_SideDistance,Screen.width-m_SideDistance*2,Screen.height-m_SideDistance*BottomDistanceMultiple);
            else 
                m_windowsRect = new Rect(-m_SideDistance, -m_SideDistance, 150f, 45f);
        }
	}

    private void OnGUI()
    {
		Event e = Event.current;

		//_ScreenSize = (ScreenSize)GUILayout.Toolbar ((int)_ScreenSize, Enum.GetNames (typeof(ScreenSize)));
		GUILayout.BeginHorizontal ();
		//if (GUILayout.Button ("Min",GUILayout.Width(100f))) {
		//	_ScreenSize = ScreenSize.Min;
		//}
		//if (GUILayout.Button ("Max",GUILayout.Width(100f))) {
		//	_ScreenSize = ScreenSize.Max;
		//}
		GUILayout.EndHorizontal ();
		ChangeScreen ();

		m_windowsRect = GUILayout.Window (10, m_windowsRect, ShowMessage,"Debug Message");
		if (e != null && e.isKey && KeyFun != null && KeyFun.Count > 0 && KeyFun.ContainsKey (e.keyCode)) {
			KeyFun [e.keyCode] ();
		}

    }

	void ChangeScreen(){
		if (_ScreenSize.Equals (ScreenSize.Default))
			return;
		
		switch (_ScreenSize)
		{
			case ScreenSize.Max:
				RectMine = true;
				break;
			case ScreenSize.Min:
				RectMine = false;
				break;
			default:
				break;
		}
		SettingNewRect = true;
		_ScreenSize = ScreenSize.Default;
	}

    void ShowMessage(int mMessageID){
		
		if (RectMine) {
            if (GUILayout.Button ("Min",GUILayout.Width(100f))) {
              _ScreenSize = ScreenSize.Min;
            }
			GUILayout.Label (string.Format("Now Show Type {0}",filterType.ToString()));
			GUILayout.BeginHorizontal();
			m_ScrollPos = GUILayout.BeginScrollView (m_ScrollPos,true,true);
			if (MessageContainer.Count > 0) {
				foreach (var Allmessage in MessageContainer) {
					if (filterType.Equals (MessageTypeLevel.AllMessage) || filterType.Equals (Allmessage.m_MessageTypeLevel)) {
						switch (Allmessage.m_MessageTypeLevel) {
						case MessageTypeLevel.Error:
						case MessageTypeLevel.Warning:
							LabelStyle.normal.textColor = Color.red;
							break;
						case MessageTypeLevel.Exception:
							LabelStyle.normal.textColor = Color.yellow;
							break;
						case MessageTypeLevel.PlayerConsole:
							LabelStyle.normal.textColor = Color.green;
							break;
						default:
							LabelStyle.normal.textColor = Color.white;
							break;
						}
						GUILayout.Label (Allmessage.getMessage (), LabelStyle);
					}
				}
			}
			GUILayout.EndScrollView ();
			GUILayout.BeginVertical (GUILayout.Width (100f));
			GUILayout.Label ("篩選");
			foreach (MessageTypeLevel chosetype in Enum.GetValues(typeof(MessageTypeLevel))) {
				if (GUILayout.Button (chosetype.ToString ())) {
					filterType = chosetype;
				}
			}
			GUILayout.EndVertical ();
			GUILayout.EndHorizontal ();
			GUILayout.BeginHorizontal ();
			GUILayout.Label ("指令： ", GUILayout.Width (50f));
			PlayerCall = GUILayout.TextField (PlayerCall);
			GUILayout.EndHorizontal ();
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button (m_clearMessage)) {
				MessageContainer.Clear ();
			}
			if (GUILayout.Button (m_PlayerMessage)) {
				InputPlayerCall ();
			}
			if (GUILayout.Button (m_closeMessage)) {
				_ScreenSize = ScreenSize.Min;
                pointadd = 0;
			}
			GUILayout.EndHorizontal();   

		}
         
    }
    int pointadd = 0;
    public void ChickAdd(){
        pointadd++;
        if(pointadd>=11){
            _ScreenSize = ScreenSize.Max;
            pointadd = 0;
        }
    }
    void InputPlayerCall(){
        if (PlayerCall.Equals(""))
            return;
        MessageContainer.Add(new MessageData(PlayerCall, MessageTypeLevel.PlayerConsole));
        PlayerCall = "";
    }

    void HandLog(string logstring,string stackTrace,LogType logetype){
        switch (logetype)
        {
            case LogType.Log:
                MessageContainer.Add(new MessageData(logstring, MessageTypeLevel.NormalLog));
                break;
            case LogType.Assert:
                MessageContainer.Add(new MessageData(logstring, MessageTypeLevel.Assert));
                break;
            case LogType.Error:
                MessageContainer.Add(new MessageData(string.Format("Error:{0} , {1}",logstring,stackTrace), MessageTypeLevel.Error));
                break;
            case LogType.Exception:
                MessageContainer.Add(new MessageData(string.Format("Exception:{0} , {1}", logstring, stackTrace), MessageTypeLevel.Exception));
                break;
            case LogType.Warning:
                MessageContainer.Add(new MessageData(string.Format("Warring:{0} , {1}", logstring, stackTrace), MessageTypeLevel.Warning));
                break;
            default:
                break;
        }
    }
}

//public class BornUIObj{
//	public static GameObject addCanvas(){
//		GameObject ObjCanvas = new GameObject ("BlackCanvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
//		//ObjCanvas.name = "BlackCanvas";
//		ObjCanvas.layer=5;
//		ObjCanvas.GetComponent<Canvas> ().renderMode = RenderMode.ScreenSpaceOverlay;
//		return ObjCanvas;
//	}
//	public static GameObject addEventSystem(){
//		GameObject ObjCanvas = new GameObject ("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule), typeof(BaseInput));
//		return ObjCanvas;
//	}
//	public static GameObject addScrollbar(){
//		GameObject ObjCanvas = new GameObject ("Scrollbar", typeof(EventSystem), typeof(StandaloneInputModule), typeof(BaseInput));
//		return ObjCanvas;
//	}
//}
