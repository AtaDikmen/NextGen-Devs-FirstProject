using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyAudioManagerScript : MonoBehaviour
{
    public MyAudioManagerSO myAudioManagerSO;
    public AudioSource musicSource3D, SFXSource3D,AmbienceSource3D, musicSource2D, SFXSource2D, AmbienceSource2D;
    public GameObject musicObject3D, SFXObject3D, AmbienceObject3D, musicObject2D, SFXObject2D, AmbienceObject2D;

    #region UNITY LISTENER METHODS;
    private void OnEnable()
    {
        myAudioManagerSO.StopSFX3D.AddListener(StopSFX3D);
        myAudioManagerSO.StopMusic3D.AddListener(StopMusic3D);
        myAudioManagerSO.StopAmbience3D.AddListener(StopAmbience3D);
        myAudioManagerSO.StopSFX2D.AddListener(StopSFX2D);
        myAudioManagerSO.StopMusic2D.AddListener(StopMusic2D);
        myAudioManagerSO.StopAmbience2D.AddListener(StopAmbience2D);
        myAudioManagerSO.ToggleMusic3D.AddListener(ToggleMusic3D);
        myAudioManagerSO.ToggleSFX3D.AddListener(ToggleSFX3D);
        myAudioManagerSO.ToggleAmbience3D.AddListener(ToggleAmbience3D);
        myAudioManagerSO.ToggleMusic2D.AddListener(ToggleMusic2D);
        myAudioManagerSO.ToggleSFX2D.AddListener(ToggleSFX2D);
        myAudioManagerSO.ToggleAmbience3D.AddListener(ToggleAmbience2D);
        myAudioManagerSO.SFXRandomPitchValue3D.AddListener(SFXRandomPitchValue3D);
        myAudioManagerSO.SFXRandomPitchValue2D.AddListener(SFXRandomPitchValue2D);
        myAudioManagerSO.PlayMusic3D.AddListener(PlayMusic3D);
        myAudioManagerSO.PlayMusic2D.AddListener(PlayMusic2D);
        myAudioManagerSO.PlaySFX3D.AddListener(PlaySFX3D);
        myAudioManagerSO.PlaySFX2D.AddListener(PlaySFX2D);
        myAudioManagerSO.PlayAmbience3D.AddListener(PlayAmbience3D);
        myAudioManagerSO.PlayAmbience2D.AddListener(PlayAmbience2D);
    }
    private void OnDisable()
    {
        myAudioManagerSO.StopSFX3D.RemoveListener(StopSFX3D);
        myAudioManagerSO.StopMusic3D.RemoveListener(StopMusic3D);
        myAudioManagerSO.StopAmbience3D.RemoveListener(StopAmbience3D);
        myAudioManagerSO.StopSFX2D.RemoveListener(StopSFX2D);
        myAudioManagerSO.StopMusic2D.RemoveListener(StopMusic2D);
        myAudioManagerSO.StopAmbience2D.RemoveListener(StopAmbience2D);
        myAudioManagerSO.ToggleMusic3D.RemoveListener(ToggleMusic3D);
        myAudioManagerSO.ToggleSFX3D.RemoveListener(ToggleSFX3D);
        myAudioManagerSO.ToggleAmbience3D.RemoveListener(ToggleAmbience3D);
        myAudioManagerSO.ToggleMusic2D.RemoveListener(ToggleMusic2D);
        myAudioManagerSO.ToggleSFX2D.RemoveListener(ToggleSFX2D);
        myAudioManagerSO.ToggleAmbience3D.RemoveListener(ToggleAmbience2D);
        myAudioManagerSO.SFXRandomPitchValue3D.RemoveListener(SFXRandomPitchValue3D);
        myAudioManagerSO.SFXRandomPitchValue2D.RemoveListener(SFXRandomPitchValue2D);
        myAudioManagerSO.PlayMusic3D.RemoveListener(PlayMusic3D);
        myAudioManagerSO.PlayMusic2D.RemoveListener(PlayMusic2D);
        myAudioManagerSO.PlaySFX3D.RemoveListener(PlaySFX3D);
        myAudioManagerSO.PlaySFX2D.RemoveListener(PlaySFX2D);
        myAudioManagerSO.PlayAmbience3D.RemoveListener(PlayAmbience3D);
        myAudioManagerSO.PlayAmbience2D.RemoveListener(PlayAmbience2D);
    }
    #endregion

    #region READY TO USE AUDIO CALLBACK METHODS

    // STOP 3D

    public void StopSFX3D() { SFXSource3D.Stop(); }
    public void StopMusic3D() { musicSource3D.Stop(); }
    public void StopAmbience3D() { AmbienceSource3D.Stop(); }

    // STOP 2D

    public void StopSFX2D() { SFXSource2D.Stop(); }
    public void StopMusic2D() { musicSource2D.Stop(); }
    public void StopAmbience2D() { AmbienceSource2D.Stop(); }

    // TOGGLE 3D

    public void ToggleMusic3D() { musicSource3D.mute = !musicSource3D.mute; }
    public void ToggleSFX3D() { SFXSource3D.mute = !SFXSource3D.mute; }
    public void ToggleAmbience3D() { AmbienceSource3D.mute = !AmbienceSource3D.mute; }

    // TOGGLE 2D

    public void ToggleMusic2D() { musicSource2D.mute = !musicSource2D.mute; }
    public void ToggleSFX2D() { SFXSource2D.mute = !SFXSource2D.mute; }
    public void ToggleAmbience2D() { AmbienceSource2D.mute = !AmbienceSource2D.mute; }

    //SFX Random Pitch

    public void SFXRandomPitchValue3D() {
        Sound s = Array.Find(myAudioManagerSO.SFXSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not Found");
        }
        else
        {
            SFXSource3D.clip = s.clip;
            if (SFXSource3D.clip == s.clip)
            {
                SFXSource3D.pitch = UnityEngine.Random.Range(1f, 3f);
            }
        }
    }

    public void SFXRandomPitchValue2D()
    {
        Sound s = Array.Find(myAudioManagerSO.SFXSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not Found");
        }
        else
        {
            SFXSource3D.clip = s.clip;
            if (SFXSource2D.clip == s.clip)
            {
                SFXSource2D.pitch = UnityEngine.Random.Range(1f, 3f);
            }
        }
    }

    //PLAY 

    public void PlayMusic3D(string name, float volume, Vector3 audioPosition)
    {
        Sound s = Array.Find(myAudioManagerSO.musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not Found");
        }
        else
        {
            musicSource3D.volume = volume;
            musicObject3D.transform.position = audioPosition;
            musicSource3D.clip = s.clip;
            musicSource3D.Play();
        }
        Debug.Log("Müzik3D Baþladý");
    }

    public void PlayMusic2D(string name, float volume)
    {
        Sound s = Array.Find(myAudioManagerSO.musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not Found");
        }
        else
        {
            musicSource3D.volume = volume;  
            musicSource3D.clip = s.clip;
            musicSource3D.Play();
        }
        Debug.Log("Müzik2D Baþladý");
    }

    public void PlaySFX3D(string name, float volume, Vector3 audioPosition,bool isLoop)
    {
        Sound s = Array.Find(myAudioManagerSO.SFXSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not Found");
        }
        else
        {
            SFXSource3D.loop = isLoop;
            SFXSource3D.volume = volume;
            SFXSource3D.transform.position = audioPosition;
            SFXSource3D.clip = s.clip;
            SFXSource3D.Play();
        }
        Debug.Log("SFX 3D Baþladý");
    }

    public void PlaySFX2D(string name, float volume, bool isLoop)
    {
        Sound s = Array.Find(myAudioManagerSO.SFXSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not Found");
        }
        else
        {
            SFXSource2D.loop = isLoop;
            SFXSource2D.volume = volume;
            SFXSource2D.clip = s.clip;
            SFXSource2D.Play();
        }
        Debug.Log("SFX 2D Baþladý");
    }

    public void PlayAmbience3D(string name, float volume, Vector3 audioPosition)
    {
        Sound s = Array.Find(myAudioManagerSO.ambienceSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not Found");
        }
        else
        {
            AmbienceSource3D.volume = volume;
            AmbienceSource3D.transform.position = audioPosition;
            AmbienceSource3D.clip = s.clip;
            AmbienceSource3D.Play();
        }
        Debug.Log("Ambience 3D Baþladý");
    }

    public void PlayAmbience2D(string name, float volume)
    {
        Sound s = Array.Find(myAudioManagerSO.ambienceSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not Found");
        }
        else
        {
            AmbienceSource2D.volume = volume;
            AmbienceSource2D.clip = s.clip;
            AmbienceSource2D.Play();
        }
        Debug.Log("Ambience 2D Baþladý");
    }
    #endregion
}
