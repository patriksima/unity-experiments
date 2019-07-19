using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Singleton
    private static readonly object iLock = new object();
    private static SoundManager instance;

    public static SoundManager Instance
    {
        get
        {
            lock (iLock)
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<SoundManager>();
                    DontDestroyOnLoad(instance.gameObject);
                }
            }
            return instance;
        }
    }
    #endregion

    public AudioSource music;
    public AudioSource sound;

    public void PlayOneShot(AudioClip clip)
    {
        if (clip != null)
            sound.PlayOneShot(clip);
    }
}
