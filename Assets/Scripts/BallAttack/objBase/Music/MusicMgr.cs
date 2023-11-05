using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Events;

public class MusicMgr : SingleBase<MusicMgr>
{
    private AudioSource BkMusic = null;
    public float BkMusicSl = 1;

    private GameObject BkSound = null;
    private List<AudioSource> BkSounds = null;
    public float BkSoundSl = 1;

    public MusicMgr()
    {
        MonoMgr.Instance().UpdateAddListener(AutoDesSound);
    }

    public void PlayMusic(string name)
    {
        if (BkMusic == null)
        {
            GameObject obj = new GameObject("BkMusic");
            BkMusic = obj.AddComponent<AudioSource>();
        }
        ResourcesMgr.Instance().LoadAsync<AudioClip>("Music/BkMusic/" + name, (Clip) =>
        {
            BkMusic.clip = Clip;
            BkMusic.loop = true;
            BkMusic.volume= BkMusicSl;
            BkMusic.Play();
        });
    }
    public void StopMusic()
    {
        if (BkMusic == null)
            return;
        BkMusic.Stop();
    }
    public void PauseMusic()
    {
        if (BkMusic == null)
            return;
        BkMusic.Pause();
    }
    public void ChangeMusicVolume(float v)
    {
        BkMusicSl = v;
        if (BkMusic == null)
            return;
        BkMusic.volume = BkMusicSl;
    }
    public void PlaySound(string name,bool isLoop,UnityAction<AudioSource> CallBack = null)
    {
        if (BkSound == null)
        {
            BkSound = new GameObject("BkSound");
            
        }
        ResourcesMgr.Instance().LoadAsync<AudioClip>("Music/BkSound/" + name, (Clip) =>
        {
            AudioSource au = BkSound.AddComponent<AudioSource>();
            au.clip = Clip;
            au.volume = BkSoundSl;
            au.loop = isLoop;
            au.Play();
            BkSounds.Add(au);
            if (CallBack != null)
            {
                CallBack(au);
            }
        });
    }
    public void StopSound(AudioSource audioSource)
    {
        if (BkSounds.Contains(audioSource))
        {
            BkSounds.Remove(audioSource);
            audioSource.Stop();
            GameObject.Destroy(audioSource);
        }
    }
    public void AutoDesSound()
    {
        for (int i = BkSounds.Count - 1; i >= 0; i--)
        {
            if (!BkSounds[i].isPlaying)
            {
                GameObject.Destroy(BkSounds[i]);
                BkSounds.RemoveAt(i);
            }
        }
    }
    public void ChangeSoundVolume(float v)
    {
        BkSoundSl = v;
        for (int i = 0; i < BkSounds.Count; i++)
        {
            BkSounds[i].volume = BkSoundSl;
        }
    }
}
