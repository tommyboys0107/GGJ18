using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CliffLeeCL;
public class Monster : MonoBehaviour {

    GameObject floot;

    private void Awake()
    {
        
    }
    // Use this for initialization
    void Start () {
        floot = GameObject.Find("objGround");

	}
    bool fir = true;
	// Update is called once per frame
	void Update () {
        if(fir){
            StartCoroutine(shinMove());
            fir = false;
        }
	}
    Vector3 shinPos=Vector3.zero;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.GetComponent<PlayerCollisionHandler>()!=null){
            shinPos = collision.transform.position;
        }
    }

    IEnumerator shinMove(){
        while(true){
            if(shinPos != Vector3.zero){
            Debug.Log("ss");
            yield return new WaitForEndOfFrame();
           
            this.transform.position = floot.transform.position +new Vector3(UnityEngine.Random.Range(-floot.transform.localScale.x*(2/3),floot.transform.localScale.x * (2 / 3)),UnityEngine.Random.Range(-floot.transform.localScale.y * (2 / 3), floot.transform.localScale.y * (2 / 3)),0);
            shinPos = Vector3.zero;
            }
        }
       
    }
}
