using UnityEngine.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public Sound[] sounds;

    public static AudioManager instance;

    public AudioSource backgroundMusic;

	// Use this for initialization
	void Awake () {

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

		foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
	}

    public void SetBackgroundMusic(string name)
    {
        if(backgroundMusic != null)
        {
            Debug.Log(backgroundMusic.name);
            backgroundMusic.Stop();
        }
        if(name.Length > 0)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found");
                return;
            }
            backgroundMusic = s.source;
            backgroundMusic.Play();
            //backgroundMusic.volume = 0.5f;
        }

    }
    public void SetBackgroundMusicVolume(float target)
    {
        StartCoroutine(_SetBackgroundMusicVolume(target));
    }
    IEnumerator _SetBackgroundMusicVolume(float target)
    {
        if(backgroundMusic != null)
        {
            float currentVolume = backgroundMusic.volume;
            if(currentVolume > target)
            {
                while(currentVolume > target)
                {
                    backgroundMusic.volume = currentVolume;
                    currentVolume -= 0.005f;
                    yield return null;
                }
            }
            else
            {
                while(currentVolume < target)
                {
                    backgroundMusic.volume = currentVolume;
                    currentVolume += 0.01f;
                    yield return null;
                }
            }
        }
    }

    public void Play(string name, float volume = 0.5f, float time = 0)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        if (time > 0)
            s.source.volume = 0;
        else
            s.source.volume = volume;
        s.source.Play();
        if(time > 0)
        {
            StartCoroutine(_SetVolume(s.source, volume, 1));
        }

    }
    public AudioSource Play2(string name, float volume = 0.5f, float time = 0)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return null;
        }
        if (time > 0)
            s.source.volume = 0;
        else
            s.source.volume = volume;
        s.source.Play();
        if (time > 0)
        {
            StartCoroutine(_SetVolume(s.source, volume, 1));
        }
        return s.source;

    }

    public void Stop(AudioSource a, float time = 0)
    {
        if(a!=null)
            StartCoroutine(_SetVolume(a, 0, time, true));
        Debug.Log(time);
    }

    public void SetVolume(AudioSource a, float target, float t, bool stopAfter = false)
    {
        StartCoroutine(_SetVolume(a, target, t, stopAfter));
    }

    IEnumerator _SetVolume(AudioSource a, float target, float t, bool stopAfter = false)
    {
        float current = a.volume;
        if(t > 0)
        {
            float step = Time.deltaTime / t;
            if (current <= target)
            {
                while (current <= target)
                {
                    Debug.Log(a.volume);
                    current += step;
                    a.volume = current;
                    yield return null;
                }
            }
            else
            {
                while (current >= target)
                {
                    Debug.Log("Decreasing volume: " + a.volume);
                    current -= step;
                    a.volume = current;
                    yield return null;
                }
            }
        }
        if(stopAfter)
        {
            a.Stop();
        }
    }

    public AudioSource GetSource(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return null;
        }
        return s.source;
    }

}
