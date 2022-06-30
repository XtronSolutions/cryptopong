using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class AudioManager : MonoBehaviour {

	public AudioSource musicSource;
	public AudioSource soundSource;

	public AudioClip gameMusic;
	public AudioClip loginScreenMusic;
	public AudioClip LobbyMusic;
	public AudioClip menuClickSound;
	public AudioClip hoverSound;
	public AudioClip collisionSound;
	public AudioClip loseSound;
	public AudioClip winSound;
	public AudioClip losePointSound;
	public AudioClip winPointSound;
	public AudioClip CyberpunkMap;
	public AudioClip forestMap;
	public AudioClip spaceMap;

	public static float MaxMusic = 0.2f;
	public static float MaxSound = 1f;
	private int soundIndex=0;
	void Awake()
	{
		soundSource.volume = PlayerPrefs.GetFloat ("Sound", MaxSound);
		musicSource.volume = PlayerPrefs.GetFloat ("Music", MaxMusic);
	}

	public float GetSoundVolume => soundSource.volume;
	public float GetMusicVolume => musicSource.volume;

	#region Sound FX

	public void PlayLoseSound()
	{
		StopGameMusic();
		soundSource.clip = loseSound;
		soundSource.Play();
	}

	public void PlayClickSound()
	{
		soundSource.clip = menuClickSound;
		soundSource.Play ();
	}

	public void PlayHoverSound()
	{
		soundSource.clip = hoverSound;
		soundSource.Play();
	}

	public void PlayPointLoseSound()
	{
		soundSource.clip = losePointSound;
		soundSource.Play();
	}

	public void PlayPointWinSound()
	{
		soundSource.clip = winPointSound;
		soundSource.Play();
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
	public void PlayCyberpunkMusic()
	{
		musicSource.clip = CyberpunkMap;
		musicSource.Play();
	}

	public void PlayForestMusic()
	{
		musicSource.clip = forestMap;
		musicSource.Play();
	}

	public void PlaySpaceMusic()
	{
		musicSource.clip = spaceMap;
		musicSource.Play();
	}

	public void PlayLoginMusic()
	{
		musicSource.clip = loginScreenMusic;
		musicSource.Play();
	}

	public void PlayLobbyMusic()
	{
		musicSource.clip = LobbyMusic;
		musicSource.Play();
	}

	public void PlayGameMusic()
	{
		musicSource.clip = gameMusic;
		musicSource.Play ();
	}

	public void StopGameMusic()
	{
		musicSource.Stop ();
	}

	public void SetSoundMusicVolume(float value,bool setDirect=false)
	{
		if (!setDirect)
			musicSource.volume += value;
		else
			musicSource.volume = value;
	}
	#endregion

}
