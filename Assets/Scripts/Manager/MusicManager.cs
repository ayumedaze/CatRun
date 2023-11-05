using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : Singleton<MusicManager>
{
    public AudioClip backGround;
    public AudioClip[] soundEffect;
    public AudioSource backGroundSource;
    public AudioSource soundEffectSource;

    // Start is called before the first frame update
    void Start()
    {
        backGroundSource.clip = backGround;
        backGroundSource.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SoundEffectTrigger(string soundEffectToChange)
    {
        if (soundEffectToChange.Equals("按钮"))
        {
            soundEffectSource.clip = soundEffect[0];
        }
        else if (soundEffectToChange.Equals("三消"))
        {
            soundEffectSource.clip = soundEffect[1];
        }
        else if (soundEffectToChange.Equals("过关"))
        {
            soundEffectSource.clip = soundEffect[2];
        }
        soundEffectSource.Play();
    }
}
