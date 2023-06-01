using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    private float savedVolume = 0.5f;
    [SerializeField] private AudioClip[] somInGameMusic;
    private AudioSource componentSourceMusic;

    private void Awake()
    {
        componentSourceMusic = GetComponent<AudioSource>();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            ChangeSoundMusic(0);
        }
        else
        {
            ChangeSoundMusic(1);
        }
    }
    public void ChangeSoundMusic(int indexSom)
    {
        componentSourceMusic.clip = somInGameMusic[indexSom];
        componentSourceMusic.Play(); 
    }
    public void SaveVolume(float volume)
    {
        savedVolume = volume;
    }

    public float LoadVolume()
    {
        return savedVolume;
    }
}
