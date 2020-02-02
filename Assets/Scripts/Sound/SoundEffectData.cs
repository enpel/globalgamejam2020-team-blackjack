using System;
using UnityEngine;

public enum SoundEffectType{
    Tap,
    Repaired,
    GetRC,
    SelectButton,

}

[Serializable]
public class SoundEffectData
{
    public SoundEffectType SoundEffectType;
    public AudioClip AudioClip;
}
