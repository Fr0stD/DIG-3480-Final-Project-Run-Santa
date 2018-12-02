using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JeWoCameraController : MonoBehaviour {

    public GameObject player;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {

        transform.position = player.transform.position;
		
	}
}
