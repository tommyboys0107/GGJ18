using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceControl : MonoBehaviour {

    public GameObject Face;
    public GameObject Mad;
    public float f_faceHitTime = 2f;

	// Use this for initialization
	void Start () {
        

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)){
            if (!Mad.activeInHierarchy)
                hit();
        }
	}


    void hit(){
        Face.GetComponent<Animator>().SetBool("faceInit", false);
        Face.GetComponent<Animator>().SetBool("hitTrigger", true);
        Invoke("FaceInit",f_faceHitTime);
    }

    void FaceInit(){
        Face.GetComponent<Animator>().SetBool("hitTrigger", false);
        Face.GetComponent<Animator>().SetBool("faceInit", true);
    }

}
