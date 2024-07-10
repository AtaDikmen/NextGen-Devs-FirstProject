using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "MyAudioManagerSO", menuName = "MyAudioManagerSO")]
public class MyAudioManagerSO : ScriptableObject
{
    public Sound[] musicSounds, SFXSounds,ambienceSounds;
        

    [HideInInspector]
    public UnityEvent  StopSFX3D, StopMusic3D, StopAmbience3D, StopSFX2D, StopMusic2D, StopAmbience2D, ToggleMusic3D, ToggleSFX3D,ToggleAmbience3D, ToggleMusic2D, ToggleSFX2D, ToggleAmbience2D, SFXRandomPitchValue3D, SFXRandomPitchValue2D;

    [HideInInspector]
    public UnityEvent<string> PlaySFXWithLoop;

    [HideInInspector]
    public UnityEvent<string, float, Vector3> PlayMusic3D, PlayAmbience3D;

    [HideInInspector]
    public UnityEvent<string, float>  PlayMusic2D , PlayAmbience2D;

    [HideInInspector]
    public UnityEvent<string, float, Vector3, bool> PlaySFX3D;

    [HideInInspector]
    public UnityEvent<string, float,bool> PlaySFX2D;
}
