using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource audioSourcePrefab;
    public int initialPoolSize = 10;

    private List<AudioSource> audioSourcePool;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAudioSourcePool();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeAudioSourcePool()
    {
        audioSourcePool = new List<AudioSource>();

        for (int i = 0; i < initialPoolSize; i++)
        {
            AudioSource newAudioSource = Instantiate(audioSourcePrefab, transform);
            newAudioSource.gameObject.SetActive(false);
            audioSourcePool.Add(newAudioSource);
        }
    }

    public AudioSource GetAvailableAudioSource()
    {
        foreach (AudioSource source in audioSourcePool)
        {
            if (source && !source.isPlaying)
            {
                return source;
            }
        }

        // If no available AudioSource, create a new one
        AudioSource newSource = Instantiate(audioSourcePrefab, transform);
        audioSourcePool.Add(newSource);
        return newSource;
    }

    public void PlaySFX(AudioClip clip, float volume = 0.3f)
    {
        AudioSource source = GetAvailableAudioSource();
        source.clip = clip;
        source.volume = volume;
        source.gameObject.SetActive(true);
        source.Play();
        StartCoroutine(DisableAfterPlaying(source));
    }

    private IEnumerator DisableAfterPlaying(AudioSource source)
    {
        yield return new WaitWhile(() => source.isPlaying);
        source.gameObject.SetActive(false);
    }
}
