using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour {
    public AudioSource[] controalaudio;
    public GameObject audiowhat;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void changeaudio() {
        if (controalaudio == null || controalaudio.Length == 0)
            return;
        foreach (var _audio in controalaudio) {
            _audio.mute = !_audio.mute;
        }
        if (audiowhat != null)
            audiowhat.SetActive(!audiowhat.activeSelf);
    }
}
