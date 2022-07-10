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

using Photon.Pun;
using UnityEngine;
using System.Collections;

public class PhotonBall : MonoBehaviourPunCallbacks, IPunInstantiateMagicCallback
{
    public float speed;
    public float velocity;
    public float speedMultiplier;
    public ParticleSystem particle;
    public ParticleSystem hitParticle;
    public Rigidbody2D ballBody;
    [HideInInspector]
    public BasePaddle lastTouchedPaddle;

    void Awake()
    {

    }

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            ResetBall();
            Invoke(nameof(KickOffBall), 3);
        }
        else
        {
            ballBody.isKinematic = true;
        }
    }

    public override void OnEnable()
    {
        base.OnEnable();
        if (PhotonNetwork.IsMasterClient)
        {
            Events.OnBallSpeedChanged += OnBallSpeedChanged;
            Events.OnImpactMultiplierChanged += OnImpactMultiplierChanged;
        }
    }

    public override void OnDisable()
    {
        base.OnDisable();
        if (PhotonNetwork.IsMasterClient)
        {
            Events.OnBallSpeedChanged -= OnBallSpeedChanged;
            Events.OnImpactMultiplierChanged -= OnImpactMultiplierChanged;
        }
    }

    private void OnBallSpeedChanged(float value)
    {
        speed = value;
        ballBody.velocity = Vector3.ClampMagnitude(ballBody.velocity, speed);
    }
    private void OnImpactMultiplierChanged(float value) => speedMultiplier = value;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        hitParticle.Play();
        // Managers.Audio.PlayCollisionSound();
        // StartCoroutine(Managers.Cam.shaker.Shake());

        if (other.gameObject.name.Equals("RightWall"))
        {
            // Managers.Score.OnScore(PaddleOwner.AI);
        }
        else if (other.gameObject.name.Equals("LeftWall"))
        {
            // Managers.Score.OnScore(PaddleOwner.PLAYER);
        }
        else if (other.gameObject.CompareTag("PADDLE"))
        {
            Vector2 velocity = ballBody.velocity;

            float x = HitFactor(transform.position, other.transform.position, other.collider.bounds.size.y);
            int temp = 0;
            temp = (other.transform.position.x > 1) ? -1 : 1;
            Vector2 dir = new Vector2(temp, x).normalized;
            ballBody.velocity = dir * velocity.magnitude * speedMultiplier;
            this.velocity = velocity.magnitude * speedMultiplier;
            lastTouchedPaddle = other.gameObject.GetComponent<BasePaddle>();

            if (lastTouchedPaddle.GetType().Name.Equals(nameof(PaddlePlayer)))
            {
                // Managers.Score.TotalHits += 1;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // if (lastTouchedPaddle != null)
        //     other.gameObject.GetComponent<Powerup>().TriggerPowerup(lastTouchedPaddle);

    }

    public void KickOffBall()
    {
        ballBody.angularVelocity = 0.0f;

        float r = Random.value;
        Vector2 _direction = (r >= 0.5f) ? new Vector2(1, r) : new Vector2(-1, r);

        ballBody.AddForce(_direction * speed);
        particle.gameObject.SetActive(true);

    }

    public void ResetBall()
    {
        ballBody.velocity = Vector2.zero;
        transform.position = Vector2.zero;
        particle.gameObject.SetActive(false);
    }

    float HitFactor(Vector2 ballPosition, Vector2 paddlePosition, float paddleWidth)
    {
        return (ballPosition.y - paddlePosition.y) / paddleWidth;
    }

    public void ParticleRotation()
    {
        Vector3 directionOfMotion = new Vector3(0, ballBody.velocity.y, ballBody.velocity.x);
        Quaternion rotation = Quaternion.LookRotation(directionOfMotion);
        particle.transform.localRotation = rotation;
    }

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        this.transform.parent = PhotonGameManager.Instance.BallSpawnPoint;
        this.transform.localScale = Vector3.one;
        this.transform.localPosition = Vector3.zero;
    }
}
