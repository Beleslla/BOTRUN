using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource soundSource;
    private AudioSource musicSource;

    [SerializeField] private Slider soundSlider;
    [SerializeField] private Slider musicSlider;

    private void Awake()
    {
        soundSource = GetComponent<AudioSource>();
        musicSource = transform.GetChild(0).GetComponent<AudioSource>();
        instance = this;

        loadVol(0.3f, "musicVolume", musicSlider, musicSource);
        loadVol(1, "soundVolume", soundSlider, soundSource);
    }

    private void loadVol(float baseVolume, string volumeName, Slider _slider, AudioSource _source)
    {
        _source.volume = PlayerPrefs.GetFloat(volumeName);
        _slider.value = PlayerPrefs.GetFloat(volumeName, 1) / baseVolume;
    }

    public void Playsound(AudioClip _sound)
    {
        soundSource.PlayOneShot(_sound);
    }

    public void ChangeSoundVolume()
    {
        ChangeSourceVolume(1, "soundVolume", soundSlider.value, soundSource);
    }
    public void ChangeMusicVolume()
    {
        ChangeSourceVolume(0.3f, "musicVolume", musicSlider.value, musicSource);
    }
    private void ChangeSourceVolume(float baseVolume, string volumeName, float change, AudioSource source)
    {
        float finalVolume = baseVolume * change;
        source.volume = finalVolume;

        PlayerPrefs.SetFloat(volumeName, finalVolume);
    }
}
