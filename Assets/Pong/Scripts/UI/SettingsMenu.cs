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

	void Awake()
	{
		soundVolumeText.text = ((int)(Managers.Audio.soundSource.volume*100)).ToString();
		musicVolumeText.text = ((int)(Managers.Audio.musicSource.volume*100)).ToString();
		controllerSelectionDropdown.value =PlayerPrefs.GetInt ("Input");
	}

	public void IncrementSound()
	{
		Managers.Audio.SetSoundFxVolume (0.01f);
		soundVolumeText.text = ((int)(Managers.Audio.soundSource.volume*100)).ToString();
		Managers.Audio.PlayClickSound ();
	}

	public void DecrementSound()
	{
		Managers.Audio.SetSoundFxVolume (-0.01f);
		soundVolumeText.text = ((int)(Managers.Audio.soundSource.volume*100)).ToString();
		Managers.Audio.PlayClickSound ();
	}

	public void IncrementMusic()
	{
		Managers.Audio.SetSoundMusicVolume (0.01f);
		musicVolumeText.text = ((int)(Managers.Audio.musicSource.volume*100)).ToString();
		Managers.Audio.PlayClickSound ();
	}

	public void DecrementMusic()
	{
		Managers.Audio.SetSoundMusicVolume(-0.01f);
		musicVolumeText.text = ((int)(Managers.Audio.musicSource.volume*100)).ToString();
		Managers.Audio.PlayClickSound ();
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
