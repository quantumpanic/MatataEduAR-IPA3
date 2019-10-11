using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    public List<SceneMusic> musicTracks = new List<SceneMusic>();
    public AudioSource currentMusic;
    public AudioSource queueMusic;

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        // ignore transition scene
        if (scene.name == "SceneTransit") return;

        foreach (SceneMusic sc in musicTracks)
        {
            if (sc.scene == scene.buildIndex)
            {
                // if same track, do nothing
                if (currentMusic.clip == sc.music) return;

                // play the coresponding music
                currentMusic.clip = sc.music;

                currentMusic.loop = sc.loop;

                currentMusic.volume = 1;
                currentMusic.Play();
                queueMusic.Stop();

                return;
            }
        }

        // not track found, stop music
        currentMusic.Stop();
    }

    void FadeMusic(bool fade)
    {

    }

    public List<AudioClip> tracks = new List<AudioClip>();

    public void PlayMusic(string trackName, bool loop = true)
    {
        SetCurrentTrack(trackName);
        currentMusic.loop = loop;
        currentMusic.Play();
    }

    void SetCurrentTrack(string trackName)
    {
        foreach (AudioClip clip in tracks)
        {
            if (clip.name == trackName)
            {
                currentMusic.clip = clip;
            }
        }
    }

    void SetTrackSwitch(string trackName)
    {

        foreach (AudioClip clip in tracks)
        {
            if (clip.name == trackName)
            {
                queueMusic.clip = clip;
                queueMusic.loop = true;
                //queueMusic.Play();
                //queueMusic.Pause();
                //queueMusic.volume = 0;
            }
        }
    }

    void SwitchTrack(bool back = false)
    {
        if (back)
        {
            currentMusic.Play();
            currentMusic.volume = .5f;
            //currentMusic.UnPause();
        }

        else
        {
            queueMusic.Play();
            queueMusic.volume = .5f;
            //queueMusic.UnPause();
        }
    }

    void FadeIn(AudioSource music)
    {
        StartCoroutine(DoFadeIn(music));
    }

    IEnumerator DoFadeIn(AudioSource music)
    {
        //music.UnPause();

        while (music.volume < 1)
        {
            yield return new WaitForEndOfFrame();
            music.volume += 0.05f;
        }
    }

    void FadeOut(AudioSource music)
    {
        StartCoroutine(DoFadeOut(music));
    }

    IEnumerator DoFadeOut(AudioSource music)
    {
        while (music.volume > 0)
        {
            music.volume -= 0.02f;
            yield return new WaitForEndOfFrame();
        }

        //music.time = 0;
        music.Stop();
        music.volume = 0.5f;
    }

    public void PlayClipAfterCurrent(string trackName, bool loop = true)
    {
        currentMusic.loop = loop;
        StartCoroutine(DoPlayClipAfterCurrent(trackName));
    }

    IEnumerator DoPlayClipAfterCurrent(string trackName)
    {
        // code here to simulate music change without hiccup
        // play next track at zero volume and then raise volume
        // when track is supposed to play

        float transitionWindow = 0.11f;

        double startTime = AudioSettings.dspTime;
        double trackLength = currentMusic.clip.length - transitionWindow;

        SetTrackSwitch(trackName);

        yield return new WaitUntil(() => (float)AudioSettings.dspTime >= (float)startTime + (float)trackLength);

        FadeOut(currentMusic);
        SwitchTrack();
        FadeIn(queueMusic);

        // loop to original track again, this time the same track

        //SetCurrentTrack(trackName);
        PlayMusic(trackName);
        currentMusic.Stop();

        transitionWindow = 0.08f;
        startTime = AudioSettings.dspTime;
        trackLength = queueMusic.clip.length - transitionWindow;

        yield return new WaitUntil(() => (float)AudioSettings.dspTime >= (float)startTime + (float)trackLength);

        FadeOut(queueMusic);
        SwitchTrack(true);
        FadeIn(currentMusic);

        // repeat
        //PlayClipAfterCurrent(trackName);
        StartCoroutine(ContinuousLoop(trackName));
    }

    IEnumerator ContinuousLoop(string trackName)
    {
        float transitionWindow = 0.08f;

        double startTime = AudioSettings.dspTime;
        double trackLength = currentMusic.clip.length - transitionWindow;

        SetTrackSwitch(trackName);

        yield return new WaitUntil(() => (float)AudioSettings.dspTime >= (float)startTime + (float)trackLength);

        FadeOut(currentMusic);
        SwitchTrack();
        FadeIn(queueMusic);

        // loop to original track again, this time the same track

        //SetCurrentTrack(trackName);
        PlayMusic(trackName);
        currentMusic.Stop();

        transitionWindow = 0.08f;
        startTime = AudioSettings.dspTime;
        trackLength = queueMusic.clip.length - transitionWindow;

        yield return new WaitUntil(() => (float)AudioSettings.dspTime >= (float)startTime + (float)trackLength);

        FadeOut(queueMusic);
        SwitchTrack(true);
        FadeIn(currentMusic);
        StartCoroutine(ContinuousLoop(trackName));
    }
}

[System.Serializable]
public struct SceneMusic
{
    public int scene;
    public AudioClip music;
    public bool loop;
}
