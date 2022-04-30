//  /*********************************************************************************
//   *********************************************************************************
//   *********************************************************************************
//   * Produced by Skard Games										                  *
//   * Facebook: https://goo.gl/5YSrKw											      *
//   * Contact me: https://goo.gl/y5awt4								              *											
//   * Developed by Cavit Baturalp Gürdin: https://tr.linkedin.com/in/baturalpgurdin *
//   *********************************************************************************
//   *********************************************************************************
//   *********************************************************************************/

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
		soundVolumeText.text = ((int)(Managers.Audio.soundSource.volume*100)).ToString()+"%";
		musicVolumeText.text = ((int)(Managers.Audio.musicSource.volume*100)).ToString()+"%";
		controllerSelectionDropdown.value =PlayerPrefs.GetInt ("Input");

		SoundSlider.value = Managers.Audio.GetSoundVolume;
		MusicSlider.value = Managers.Audio.GetMusicVolume;

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
		Managers.Audio.SetSoundMusicVolume (0.1f);
		musicVolumeText.text = ((int)(Managers.Audio.musicSource.volume*100)).ToString()+"%";
		Managers.Audio.PlayClickSound ();
		MusicSlider.value = Managers.Audio.GetMusicVolume;
	}

	public void DecrementMusic()
	{
		Managers.Audio.SetSoundMusicVolume(-0.1f);
		musicVolumeText.text = ((int)(Managers.Audio.musicSource.volume*100)).ToString()+"%";
		Managers.Audio.PlayClickSound ();
		MusicSlider.value = Managers.Audio.GetMusicVolume;
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
