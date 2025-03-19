using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Audio Sources
    public AudioSource musicSource;
    public AudioSource effectsSource;
    
    // Audio Clips
    public AudioClip backgroundMusic;
    public AudioClip weaponPickupSound;
    public AudioClip gunshotSound;
    public AudioClip hitSound;
    public AudioClip deathSound;
    
    // Volume settings
    [Range(0f, 1f)]
    public float musicVolume = 0.5f;
    [Range(0f, 1f)]
    public float effectsVolume = 0.7f;
    
    void Start()
    {
        // Set up audio sources if not assigned
        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.loop = true;
            musicSource.volume = musicVolume;
        }
        
        if (effectsSource == null)
        {
            effectsSource = gameObject.AddComponent<AudioSource>();
            effectsSource.volume = effectsVolume;
        }
        
        // Start background music
        if (backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.Play();
        }
    }
    
    public void PlayWeaponPickup()
    {
        PlayEffect(weaponPickupSound);
    }
    
    public void PlayGunshot()
    {
        PlayEffect(gunshotSound);
    }
    
    public void PlayHit()
    {
        PlayEffect(hitSound);
    }
    
    public void PlayDeath()
    {
        PlayEffect(deathSound);
    }
    
    private void PlayEffect(AudioClip clip)
    {
        if (clip != null && effectsSource != null)
        {
            effectsSource.PlayOneShot(clip);
        }
    }
    
    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        if (musicSource != null)
        {
            musicSource.volume = musicVolume;
        }
    }
    
    public void SetEffectsVolume(float volume)
    {
        effectsVolume = Mathf.Clamp01(volume);
        if (effectsSource != null)
        {
            effectsSource.volume = effectsVolume;
        }
    }
    
    public void MuteMusic(bool mute)
    {
        if (musicSource != null)
        {
            musicSource.mute = mute;
        }
    }
    
    public void MuteEffects(bool mute)
    {
        if (effectsSource != null)
        {
            effectsSource.mute = mute;
        }
    }
}