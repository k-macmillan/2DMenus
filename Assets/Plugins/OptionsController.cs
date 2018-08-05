using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsController {
    
    private Slider masterVol;
    private Slider sfxVol;
    private Slider musicVol;

    private AudioMixer audioMixer;

    private float vol = 0.0f;

    public void SetupAudio(AudioMixer AudioMixer, Slider MasterVol, Slider SFXVol, Slider MusicVol)
    {
        audioMixer = AudioMixer;
        masterVol = MasterVol;
        sfxVol = SFXVol;
        musicVol = MusicVol;
    }
    
    
    public void DisplayValues()
    {
        masterVol.value = MasterVol;
        musicVol.value = MusicVol;
        sfxVol.value = SFXVol;
    }

    public float MasterVol
    {
        get { audioMixer.GetFloat("MasterVolume", out vol); return vol; }
        set { audioMixer.SetFloat("MasterVolume", value); }
    }

    public float MusicVol
    {
        get { audioMixer.GetFloat("MusicVolume", out vol); return vol; }
        set { audioMixer.SetFloat("MusicVolume", value); }
    }

    public float SFXVol
    {
        get { audioMixer.GetFloat("SFXVolume", out vol); return vol; }
        set { audioMixer.SetFloat("SFXVolume", value); }
    }

}
