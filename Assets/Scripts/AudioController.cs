using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] AudioSource audioSourceMusic;
    [SerializeField] AudioSource audioSourceSFX;

    [SerializeField] AudioClip clipFullLine;
    [SerializeField] AudioClip clipGameOver;
    [SerializeField] AudioClip clipPause;
    [SerializeField] AudioClip clipPieceStop;

    public static AudioController instance;

    public AudioClip ClipFullLine { get { return clipFullLine; } }
    public AudioClip ClipGameOver { get { return clipGameOver; } }
    public AudioClip ClipPause { get { return clipPause; } }
    public AudioClip ClipPieceStop { get { return clipPieceStop; } }

    void Awake()
    {
        instance = this;
    }
    
    public void PlayClip(AudioClip clip)
    {
        audioSourceSFX.PlayOneShot(clip);
    }

    public void PlayMusic()
    {
        audioSourceMusic.Play();
    }

    public void StopMusic()
    {
        audioSourceMusic.Stop();
    }

    public void PlayMusic(AudioClip clip)
    {
        audioSourceMusic.resource = clip;
        audioSourceMusic.Play();
    }
}
