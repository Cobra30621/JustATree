using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("AudioManager").AddComponent<AudioManager>();
            }
            return instance;
        }
    }

    private static AudioManager instance;

    public List<AudioClip> SFXList;
    public AudioSource audioSource;
    public AudioSource audioSourceOnce;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
        if (audioSource == null)
        {
            audioSource = this.gameObject.AddComponent<AudioSource>();
            audioSource.loop = true;
        }
            
        if (audioSourceOnce == null)
            audioSourceOnce = this.gameObject.AddComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartPlayLoop(int index, float volume = 1)
    {
        if (SFXList != null && index < SFXList.Count)
        {
            audioSource.loop = true;
            audioSource.volume = volume;
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
                audioSource.clip = SFXList[index];
                audioSource.Play();
            }
            else
            {
                audioSource.clip = SFXList[index];
                audioSource.Play();
            }
        }
        
    }

    public void StartPlayOnce(int index)
    {
        audioSourceOnce.PlayOneShot(SFXList[index]);
    }

    public void StopPlayLoop()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

}
