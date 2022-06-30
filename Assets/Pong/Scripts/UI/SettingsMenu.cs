using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {

	public Text soundVolumeText,musicVolumeText;
	public Dropdown controllerSelectionDropdown;
	private float deltaSound, deltaMusic;

	[Space(10)]
	public Slider SoundSlider, MusicSlider;

	void Awake()
	{
		float _music = Managers.Audio.GetMusicVolume;

		SoundSlider.value = Managers.Audio.GetSoundVolume;
		MusicSlider.value = _music * (1 / AudioManager.MaxMusic);

		soundVolumeText.text = ((int)(Managers.Audio.soundSource.volume*100)).ToString()+"%";
		musicVolumeText.text = ((int)(MusicSlider.value * 100)).ToString()+"%";
		controllerSelectionDropdown.value =PlayerPrefs.GetInt ("Input");

		// SoundSlider.AddListener.OnValueChanged(OnSoundUpdate);
		// MusicSlider.AddListener.OnValueChanged(OnMusicUpdate);
	}

	public void OnSoundUpdate(float value)
	{
		Managers.Audio.SetSoundFxVolume (value);
		soundVolumeText.text = ((int)(Managers.Audio.soundSource.volume*100)).ToString()+"%";
		Managers.Audio.PlayClickSound ();
	}

	public void OnMusicUpdate(float value)
	{
		Managers.Audio.SetSoundMusicVolume (value);
		musicVolumeText.text = ((int)(Managers.Audio.musicSource.volume*100)).ToString()+"%";
		Managers.Audio.PlayClickSound ();
	}

	public void IncrementSound()
	{
		Managers.Audio.SetSoundFxVolume (0.1f);
		soundVolumeText.text = ((int)(Managers.Audio.soundSource.volume*100)).ToString()+"%";
		Managers.Audio.PlayClickSound ();
		SoundSlider.value = Managers.Audio.GetSoundVolume;
	}

	public void DecrementSound()
	{
		Managers.Audio.SetSoundFxVolume (-0.1f);
		soundVolumeText.text = ((int)(Managers.Audio.soundSource.volume*100)).ToString()+"%";
		Managers.Audio.PlayClickSound ();
		SoundSlider.value = Managers.Audio.GetSoundVolume;
	}

	public void IncrementMusic()
	{
		if (MusicSlider.value <= 0.9)
		{
			Managers.Audio.SetSoundMusicVolume(AudioManager.MaxMusic / 10);
			MusicSlider.value += 0.1f;
			musicVolumeText.text = ((int)(MusicSlider.value * 100)).ToString() + "%";
			Managers.Audio.PlayClickSound();
        }else
        {
			Managers.Audio.SetSoundMusicVolume(AudioManager.MaxMusic,true);
			MusicSlider.value += 0.1f;
			musicVolumeText.text = ((int)(MusicSlider.value * 100)).ToString() + "%";
			Managers.Audio.PlayClickSound();
		}
	}

	public void DecrementMusic()
	{
		if (MusicSlider.value >= 0.1)
		{
			Managers.Audio.SetSoundMusicVolume(-AudioManager.MaxMusic/10);
			MusicSlider.value -= 0.1f;
			musicVolumeText.text = ((int)(MusicSlider.value * 100)).ToString() + "%";
			Managers.Audio.PlayClickSound();
		}else
        {
			Managers.Audio.SetSoundMusicVolume(0,true);
			MusicSlider.value -= 0.1f;
			musicVolumeText.text = ((int)(MusicSlider.value * 100)).ToString() + "%";
			Managers.Audio.PlayClickSound();
		}

	}

	public void InputMethodChanged()
	{
		if (controllerSelectionDropdown.value == 0)
			PlayerPrefs.SetInt ("Input",0);
		else if (controllerSelectionDropdown.value == 1)
			PlayerPrefs.SetInt ("Input",1);
		else if (controllerSelectionDropdown.value == 2)
			PlayerPrefs.SetInt ("Input",2);

		Managers.Input.inputType = (InputMethod) PlayerPrefs.GetInt ("Input");
		Managers.Audio.PlayClickSound ();
	}

}
