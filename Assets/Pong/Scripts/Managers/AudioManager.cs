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

public class AudioManager : MonoBehaviour {

	public AudioSource musicSource;
	public AudioSource soundSource;

	public AudioClip gameMusic;
	public AudioClip menuClickSound;
	public AudioClip collisionSound;
	public AudioClip loseSound;
	public AudioClip winSound;

	void Awake()
	{
		soundSource.volume = PlayerPrefs.GetFloat ("Sound", 1f);
		musicSource.volume = PlayerPrefs.GetFloat ("Music", 1f);
	}

	public float GetSoundVolume => soundSource.volume;
	public float GetMusicVolume => musicSource.volume;

	#region Sound FX
	public void PlayLoseSound()
	{
		StopGameMusic ();
		soundSource.clip = loseSound;
		soundSource.Play ();
	}

	public void PlayClickSound()
	{
		soundSource.clip = menuClickSound;
		soundSource.Play ();
	}

	public void PlayWinSound()
	{
		StopGameMusic ();
		soundSource.clip = winSound;
		soundSource.Play ();
	}

	public void PlayCollisionSound()
	{
		soundSource.clip = collisionSound;
		soundSource.Play ();
	}

	public void SetSoundFxVolume(float value)
	{
		float temp = Mathf.Clamp(value + soundSource.volume,0,1);
		soundSource.volume += value;
	}
	#endregion

	#region Music 
	public void PlayGameMusic()
	{
		musicSource.clip = gameMusic;
		musicSource.Play ();
	}

	public void StopGameMusic()
	{
		musicSource.Stop ();
	}

	public void SetSoundMusicVolume(float value)
	{
		float temp = Mathf.Clamp(value + musicSource.volume,0,1);		
		musicSource.volume += value;
	}
	#endregion

}
