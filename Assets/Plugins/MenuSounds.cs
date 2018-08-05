using UnityEngine;
using UnityEngine.Audio;

public class MenuSounds
{

    private AudioSource backgroundMusic;
    private AudioSource hoverSound;
    private GameObject gameObject = null;
    private AudioMixer musicMixer;

    private const string strMusicMixer = "Music";
    private const string strSFXMixer = "SFX";
    private const string strMusicPath = "Audio/Spybreak!";
    private const string strHoverSoundPath = "Audio/computerbeep";


    public void StartMenuSounds()
    {
        if (gameObject == null || !backgroundMusic.isPlaying)
        {
            musicMixer = Resources.Load<AudioMixer>("Audio/MainMixer") as AudioMixer;
            StartBackgroundMusic();
            PrepareHoverSounds();
        }
    }

    private void StartBackgroundMusic()
    {
        gameObject = new GameObject { name = "Audio Object" };
        backgroundMusic = gameObject.AddComponent<AudioSource>();
        backgroundMusic.clip = Resources.Load<AudioClip>(strMusicPath);

        backgroundMusic.outputAudioMixerGroup = musicMixer.FindMatchingGroups(strMusicMixer)[0];
        backgroundMusic.playOnAwake = false;
        backgroundMusic.loop = true;
        backgroundMusic.Play();

    }

    private void PrepareHoverSounds()
    {
        hoverSound = gameObject.AddComponent<AudioSource>();
        hoverSound.clip = Resources.Load<AudioClip>(strHoverSoundPath);
        hoverSound.outputAudioMixerGroup = musicMixer.FindMatchingGroups(strSFXMixer)[0];
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
