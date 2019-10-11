using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoostAnimScript : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        Invoke("ToggleFade", 0);
        Invoke("ToggleFade", 7);

        InvokeRepeating("ChickenShake", 1, 2);
        InvokeRepeating("ChickenShake", 1.5f, 2);
        InvokeRepeating("ChickenBob", 2, 2);

        Invoke("EggFall", 3);
        Invoke("NextScene", 8);

        GameObject.Find("MusicManager").GetComponent<MusicManager>().PlayClipAfterCurrent("in game_music2_loop");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void NextScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SwimmingChicken");
    }

    public Animator anim;

    void ChickenShake()
    {
        anim.Play("ChickenRoost1");
    }

    void ChickenBob()
    {
        anim.Play("ChickenRoost2");
    }

    public Rigidbody2D egg;

    void EggFall()
    {
        egg.simulated = true;
    }

    public Image black;
    bool isFaded = true;

    void ToggleFade()
    {
        Fade(!isFaded);
    }

    void Fade(bool doFade)
    {
        float alpha = doFade ? 1 : 0;
        black.CrossFadeAlpha(alpha, 1, false);
        isFaded = doFade;
    }
}
