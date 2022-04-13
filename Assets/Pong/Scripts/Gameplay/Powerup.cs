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
using DG.Tweening;

public enum PowerupType
{
	PADDLE_SPEED_UP,
	PADDLE_SPEED_DOWN,
	PADDLE_ENLARGE,
	PADDLE_SHRINK,
	PADDLE_GHOST
}

[RequireComponent(typeof(SpriteRenderer))]
public class Powerup : MonoBehaviour {

	public PowerupType type;
	public float PowerupLifeDuration;

	private SpriteRenderer _sprite;

	void Awake()
	{
		_sprite = GetComponent<SpriteRenderer> ();
	}

	void Start()
	{
		FadePowerup (false);
	}

	public void TriggerPowerup(Paddle pad)
	{
		PowerUp (pad);
		FadePowerup (true);
	}


	public void FadePowerup(bool collected)
	{
		if (collected)
		{
			DisablePowerup();
		}
		else
		{
			DOTween.ToAlpha (() => _sprite.color, x => _sprite.color = x, 0, PowerupLifeDuration / 5).SetLoops (5)
		.OnComplete (() => {
					DisablePowerup();
			});
		}				
	}


	public void PowerUp(Paddle pad)
	{
		switch (type)
		{
			case PowerupType.PADDLE_SPEED_UP:
				PaddleSpeedUp(pad);
				break;
			case PowerupType.PADDLE_SPEED_DOWN:
				PaddleSpeedDown(pad);
				break;
			case PowerupType.PADDLE_ENLARGE:
				PaddleEnlarge(pad);
				break;
			case PowerupType.PADDLE_SHRINK:
				PaddleShrink(pad);
				break;
			case PowerupType.PADDLE_GHOST:
				PaddleGhost(pad);
				break;
		}
	}


	public void  PaddleSpeedUp(Paddle pad)
	{
		if(pad.speed<15)
			pad.speed += Managers.PowUps.speedUpValue;
	}

	public void PaddleSpeedDown(Paddle pad)
	{
		if(pad.speed>5)
			pad.speed -= Managers.PowUps.speedDownValue;
	}

	public void PaddleEnlarge(Paddle pad)
	{	
		if(pad.transform.localScale.x <0.7f)
			pad.transform.localScale += new Vector3 (Managers.PowUps.enlargeValue,0,0);
	}

	public void PaddleShrink(Paddle pad)
	{
		if(pad.transform.localScale.x >0.1f)
			pad.transform.localScale -= new Vector3 (Managers.PowUps.shrinkValue,0,0);
	}

	public IEnumerator PaddleGhost(Paddle pad)
	{
		print ("IMPLEMENT YOUR GHOSTING USE YOUR IMAGINATION xd");
		yield break;
	}

	IEnumerator DisableEffect()
	{
		_sprite.enabled = false;
		GetComponent<BoxCollider2D> ().enabled = false;
		yield return new WaitForSeconds (Managers.PowUps.powerupImpactDuration);
		DisablePowerup ();
	}

	public void DisablePowerup()
	{
		Managers.PowUps.spawnedPowerupList.Remove (this);
		Destroy(this.gameObject);
	}
}
