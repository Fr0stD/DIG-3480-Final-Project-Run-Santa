using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JeWoGettingHit : MonoBehaviour {

    public AudioClip GetHit;
    private AudioSource source;

    private float volLowRange = .5f;
    private float volHighRange = 1.0f;

	// Use this for initialization
	void Awake ()
    {

        source = GetComponent<AudioSource>();		
	}
	
	// Update is called once per frame
	private void OnCollisionEnter2D(Collision2D collision)
    {
	    if (collision.collider.tag == "Player")
        {
            float vol = Random.Range(volLowRange, volHighRange);
            source.PlayOneShot(GetHit);
            
        }	
	}
}
