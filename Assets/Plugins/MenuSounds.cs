using UnityEngine;
using UnityEngine.Audio;

public class MenuSounds
{

    private AudioSource backgroundMusic;
    private AudioSource hoverSound;
    private static GameObject gameObject = null;
    public AudioMixer MusicMixer { get; set; }

    private const string strMusicMixer = "Music";
    private const string strSFXMixer = "SFX";
    private const string strMusicPath = "Audio/Spybreak!";
    private const string strHoverSoundPath = "Audio/computerbeep";


    public void StartMenuSounds()
    {
        if (gameObject == null || !backgroundMusic.isPlaying)
        {
            MusicMixer = Resources.Load<AudioMixer>("Audio/MainMixer") as AudioMixer;
            StartBackgroundMusic();
            PrepareHoverSounds();
        }
    }

    private void StartBackgroundMusic()
    {
        gameObject = new GameObject { name = "Audio Object" };
        backgroundMusic = gameObject.AddComponent<AudioSource>();
        backgroundMusic.clip = Resources.Load<AudioClip>(strMusicPath);

        backgroundMusic.outputAudioMixerGroup = MusicMixer.FindMatchingGroups(strMusicMixer)[0];
        backgroundMusic.playOnAwake = false;
        backgroundMusic.loop = true;
        backgroundMusic.Play();

    }

    private void PrepareHoverSounds()
    {
        hoverSound = gameObject.AddComponent<AudioSource>();
        hoverSound.clip = Resources.Load<AudioClip>(strHoverSoundPath);
        hoverSound.outputAudioMixerGroup = MusicMixer.FindMatchingGroups(strSFXMixer)[0];
        hoverSound.playOnAwake = false;
    }

    public void HoverSoundPlay()
    {
        hoverSound.Stop();
        hoverSound.Play();
    }

    public void StopMusic()
    {
        backgroundMusic.Stop();
        backgroundMusic = null;

        gameObject = null;

    }
}
