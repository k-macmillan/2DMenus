using UnityEngine.Audio;


/// <summary>
/// Class to control audio options
/// </summary>
public class OptionsControllerAudio {
    
    private AudioMixer audioMixer;
    private float vol = 0.0f;

    /// <summary>
    /// Exposed Parameter of Master mixer
    /// </summary>
    public const string strMasterVolume = "MasterVolume";
    /// <summary>
    /// Exposed Parameter of SFX mixer
    /// </summary>
    public const string strSFWVolume = "SFXVolume";
    /// <summary>
    /// Exposed Parameter of Music mixer
    /// </summary>
    public const string strMusicVolume = "MusicVolume";
        

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="AudioMixer"></param>
    public OptionsControllerAudio(AudioMixer AudioMixer)
    {
        audioMixer = AudioMixer;
    }


    /// <summary>
    /// Gets/Sets Master mixer volume
    /// </summary>
    public float MasterVol
    {
        get { audioMixer.GetFloat(strMasterVolume, out vol); return vol; }
        set { audioMixer.SetFloat(strMasterVolume, value); }
    }
    

    /// <summary>
    /// Gets/Sets SFX mixer volume
    /// </summary>
    public float SFXVol
    {
        get { audioMixer.GetFloat(strSFWVolume, out vol); return vol; }
        set { audioMixer.SetFloat(strSFWVolume, value); }
    }


    /// <summary>
    /// Gets/Sets Music mixer volume
    /// </summary>
    public float MusicVol
    {
        get { audioMixer.GetFloat(strMusicVolume, out vol); return vol; }
        set { audioMixer.SetFloat(strMusicVolume, value); }
    }
}
