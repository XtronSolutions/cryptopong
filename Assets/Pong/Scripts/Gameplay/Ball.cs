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

public class Ball : MonoBehaviour
{	
	public float speed;
	public float speedMultiplier;
	public ParticleSystem particle;
	public ParticleSystem hitParticle;
    [HideInInspector]
	public Rigidbody2D ballBody;
    [HideInInspector]
    public Paddle lastTouchedPaddle;

	void Awake ()
	{		
		ballBody = GetComponent<Rigidbody2D> ();
		ResetBall ();
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		hitParticle.Play ();
		Managers.Audio.PlayCollisionSound ();
		StartCoroutine (Managers.Cam.shaker.Shake ());

		if (other.gameObject.name.Equals ("BottomWall"))
		{
			Managers.Score.OnScore (PaddleOwner.AI);
		} 
		else if (other.gameObject.name.Equals ("TopWall"))
		{
			Managers.Score.OnScore (PaddleOwner.PLAYER);
		} 
		else if (other.gameObject.CompareTag ("PADDLE"))
		{
			Vector2 velocity = ballBody.velocity;

			float x = HitFactor (transform.position,	other.transform.position,	other.collider.bounds.size.x);
			int temp = 0;
			temp = (other.transform.position.y > 1) ? -1 : 1;
			Vector2 dir = new Vector2 (x, temp).normalized;
			ballBody.velocity = dir * velocity.magnitude * speedMultiplier ;
			lastTouchedPaddle = other.gameObject.GetComponent<Paddle>();
		} 
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		if(lastTouchedPaddle!= null)
			other.gameObject.GetComponent<Powerup> ().TriggerPowerup (lastTouchedPaddle);

	}

	public void KickOffBall ()
	{		
		ballBody.angularVelocity = 0.0f;

		float r = Random.value;
		Vector2 _direction = (r >= 0.5f) ? new Vector2 (r, 1) : new Vector2 (r, -1);

		ballBody.AddForce (_direction * speed);
		particle.gameObject.SetActive (true);

	}

	public void ResetBall()
	{
		ballBody.velocity = Vector2.zero;
		transform.position = Vector2.zero;
		particle.gameObject.SetActive (false);
	}

	float HitFactor(Vector2 ballPosition, Vector2 paddlePosition,float paddleWidth) {

		return (ballPosition.x - paddlePosition.x) / paddleWidth;
	}

	public void ParticleRotation()
	{
		Vector3 directionOfMotion = new Vector3 ( 0, ballBody.velocity.y,ballBody.velocity.x);
		Quaternion rotation = Quaternion.LookRotation(directionOfMotion);
		particle.transform.localRotation = rotation;
	}
}
