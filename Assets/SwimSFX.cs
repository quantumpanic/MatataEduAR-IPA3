using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimSFX : MonoBehaviour {

    public AudioClip countdown;
    public AudioClip crack;
    public AudioClip grow;

    public AudioSource genSource;
    public AudioSource splashSource;
    public AudioSource jumpSource;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		// check if high jump
        if (transform.position.y > 3 && !isJump)
        {
            isJump = true;
            JumpSFX();
        }

        else if (transform.position.y < 0 && !isSwimming)
        {
            isJump = false;
            isSwimming = true;
            SplashSFX();
        }

        if (transform.position.y > 1)
        {
            isSwimming = false;
        }
	}

    bool isJump;
    bool isSwimming = true;

    void PlaySFX()
    {
        genSource.Play();
    }

    public void CountSFX()
    {
        genSource.clip = countdown;
        PlaySFX();
    }

    public void JumpSFX()
    {
        if (jumpSource.isPlaying) return;
        jumpSource.Play();
    }

    public void SplashSFX()
    {
        splashSource.Play();
    }

    public void CrackSFX()
    {
        genSource.clip = crack;
        PlaySFX();
    }

    public void GrowSFX()
    {
        genSource.clip = grow;
        PlaySFX();
    }
}
