using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class SoundPlayer:ISoundEffectPlayer
{
    private readonly AudioSource _source;
    private readonly Dictionary<SoundEffectType, SoundEffectData> _soundEffects;

    public SoundPlayer(SoundEffectData[] soundEffects, AudioSource source)
    {
        _source = source;
        _soundEffects = soundEffects.ToDictionary(x=>x.SoundEffectType);
    }

    public void Play(SoundEffectType type)
    {
        if (!_soundEffects.ContainsKey(type)) return;
        
        _source.PlayOneShot(_soundEffects[type].AudioClip);
    }

}


public interface ISoundEffectPlayer
{
    void Play(SoundEffectType type);

}
