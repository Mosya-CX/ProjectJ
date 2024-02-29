using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class soundManager : MonoBehaviour
{
    public static soundManager Instance;
    [Header("文件管理与播放器")]
    public List<Sound> musicSound, sfxSound = new List<Sound>();
     public  AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;
    }
    //播放music
    public void PlayMusic(string name)
    {
        //找到对应音效片段
        for (int i = 0; i < musicSound.Count; i++)
        {
            if (musicSound[i].names == name && musicSound[i].Clip != null)
            {
                musicSource.clip = musicSound[i].Clip;
                musicSource.Play();
                break;
            }
        }

    }
    public void Playsfx(string name)
    {
        //找到对应音效片段
       // print(sfxSound[0].names);
            print(name);
        for (int i = 0; i < Instance.sfxSound.Count; i++)
        {

            if (sfxSound[i].names == name&& sfxSound[i].Clip!=null)
            {
                sfxSource.clip = sfxSound[i].Clip;
                print(sfxSource.clip);
                sfxSource.PlayOneShot(sfxSource.clip);
                break;
            }
        }
    }
    public void stopMusic()
    {
        Instance.musicSource.Stop();
    }
    public void stopsfx()
    {
        Instance.sfxSource.Stop();
    }
}
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEngine;

//public class soundManager : MonoBehaviour
//{
//    public static soundManager Instance;
//    //private AudioSource audioManager;
//    //private AudioClip jumpSound;
//    [Header("文件管理与播放器")]
//    public Sound[] musicSound, sfxSounds;
//    public AudioSource musicSource, sfxSource;

//    public void playMusic(string name)
//    {
//        Debug.Log("play");
//        Sound s = Array.Find(musicSound, x => x.name == name);
//        if (s == null)
//        {
//            Debug.Log("Not found");
//        }
//        else
//        {
//            musicSource.clip = s.Clip;
//            musicSource.Play();
//        }
//    }
//    public void playSFX(string name)
//    {
//        Sound s = Array.Find(sfxSounds, x => x.name == name);
//        if (s == null)
//        {
//            Debug.Log("Not found");
//        }
//        else
//        {
//            sfxSource.PlayOneShot(s.Clip);
//        }
//    }

//    private void Awake()
//    {
//        if (Instance != null)
//        {
//            Destroy(this);
//        }
//        Instance = this;
//        DontDestroyOnLoad(this);
//    }
//    static public void stopMusic()
//    {
//        Instance.musicSource.Stop();
//    }
//}
