using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioClip loseMusic;
    [SerializeField] AudioClip winMusic;
    [SerializeField] AudioClip bossMusic;

    public static MusicManager instance = null;

    AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        instance = this;
    }

    public void PlayWin()
    {
        source.Stop();
        source.clip = winMusic;
        source.PlayDelayed(0.2f);
        source.loop = false;
    }

    public void PlayLose()
    {
        source.Stop();
        source.clip = loseMusic;
        source.PlayDelayed(0.2f);
        source.loop = false;
    }

    public void PlayBoss()
    {
        source.Stop();
        source.clip = bossMusic;
        source.PlayDelayed(0.2f);
    }
}
