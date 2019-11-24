
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer Instance;
    public AudioSource AudioSource { get; set; }
    public MusicLibrary library;

    private void Awake()
    {
        AudioSource = this.GetComponent<AudioSource>();
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            //Debug.LogError("This is not the singleton!");
            Destroy(gameObject);
        }
    }

    public static void PlayAudio(AudioClip audio)
    {
        MusicPlayer.Instance.AudioSource.clip = audio;
        MusicPlayer.Instance.AudioSource.Play();
    }
}
