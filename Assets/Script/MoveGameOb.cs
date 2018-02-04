using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class MoveGameOb : MonoBehaviour {
    public GameObject MoveOne;
    Vector3 StartPos;
    public Vector3 DirV;
    public float MoveTime = 0.6f;
    void Awake(){
        StartPos = MoveOne.transform.localPosition;
    }
    public void OnEnable()
    {
        MoveOne.transform.localPosition = StartPos;
        MoveTime = 0.8f;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StMo(){
        StartCoroutine(moveObj());
    }

    IEnumerator moveObj(){
        while(MoveTime>0){
            MoveOne.transform.localPosition += DirV;

            MoveTime -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();

    }
}
