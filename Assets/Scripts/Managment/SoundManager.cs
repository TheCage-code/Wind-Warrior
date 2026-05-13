using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [Header ("SFX Volume")]
    public AudioClip swordSwing;    
    public AudioClip skeletonHit;       
    public AudioClip flame;
    public AudioClip slime;       
    



    [Header("Audio Sources")]
    [SerializeField] private AudioSource M_AudioSource;
    [SerializeField] private AudioSource E_AudioSource;
    

    [Header("Mixers")]
    public AudioMixer musicMixer;
    public AudioMixer effectMixer;

    [Header("Music Clips")]
    public AudioClip bossMusic;
    public AudioClip mainMusic; 

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        M_AudioSource.clip = mainMusic;
        M_AudioSource.Play();
    }


    public void SetMusicVolume(float value)
    {
        musicMixer.SetFloat("MusicVolume", value);
        PlayerPrefs.SetFloat("MusicVol", value);
        PlayerPrefs.Save();
    }
    public void SetEffectVolume(float value)
    {
        effectMixer.SetFloat("EffectVolume", value);
        PlayerPrefs.SetFloat("EffectVol", value);
        PlayerPrefs.Save();
    }
    public void ChangeBackgroundMusic(AudioClip newMusic)
    {
        
        M_AudioSource.Stop();

       
        M_AudioSource.clip = newMusic;

        
        M_AudioSource.loop = true;

        
        M_AudioSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {       
        E_AudioSource.PlayOneShot(clip);
    }
}
