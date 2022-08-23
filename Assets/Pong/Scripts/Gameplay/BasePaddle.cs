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
using Photon.Pun;
public enum PaddleOwner
{
    PLAYER,
    AI
}

public class BasePaddle : MonoBehaviourPunCallbacks
{
    public float speed;
    [HideInInspector]
    public Vector2 scale;
    int cnt = 0;
    #region Private Vars
    protected Ball _ball;
    [SerializeField]protected Rigidbody2D _rigidBody;
    [SerializeField] protected float MaxBound = 2.36f;
    [SerializeField] protected Animator Animator;
    #endregion
    [HideInInspector] public bool RubySwordActivated = false;
    [HideInInspector] public static bool RubySwordGlobalActivattion=false;
    [HideInInspector] public static bool StoreVelocity = false;
    protected virtual void Start()
    {
        _ball = Managers.Match.ball;
    }

    public override void OnEnable()
    {
        if (!Animator)
            Animator = GetComponentInChildren<Animator>();
    }

    protected virtual void Update()
    {

    }

    public void PowerUpShield()
    {
        StopCoroutine(ApplyShield());
        StartCoroutine(ApplyShield());
    }
    public IEnumerator ApplyShield()
    {
        this.transform.localScale = Managers.PowUps.shieldData.EnlargeScale;
        this.GetComponent<BoxCollider2D>().size = Managers.PowUps.shieldData.EnlargeColliderSize;

        yield return new WaitForSeconds(Managers.PowUps.shieldData.ShieldImpactDuration);

        this.transform.localScale = Managers.PowUps.shieldData.NormalScale;
        this.GetComponent<BoxCollider2D>().size = Managers.PowUps.shieldData.NoramlColliderSize;
    }

    public void PowerUpRubySword()
    {
        StopCoroutine(ApplyRubySword());
        StartCoroutine(ApplyRubySword());
    }

    public IEnumerator ApplyRubySword()
    {
        if(!BasePaddle.StoreVelocity)
            BasePaddle.StoreVelocity = true;

        BasePaddle.RubySwordGlobalActivattion = true;
        RubySwordActivated = true;
        yield return new WaitForSeconds(Managers.PowUps.rubySwordData.SwordImpactDuration);
        RubySwordActivated = false;
        BasePaddle.RubySwordGlobalActivattion = false;
        BasePaddle.StoreVelocity = false;
    }

    public void PowerUpMysteryBox(BasePaddle _pod)
    {
        StopCoroutine(ApplyMysteryBox(_pod));
        StartCoroutine(ApplyMysteryBox(_pod));
    }

    public IEnumerator ApplyMysteryBox(BasePaddle _pod)
    {
        int _random = Random.Range(0, Managers.PowUps.mysteryBoxData.PowerUpRandom.Count);
        GameObject clone = (GameObject)Instantiate(Managers.PowUps.mysteryBoxData.PowerUpRandom[_random], Vector3.zero, Quaternion.identity);
        clone.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        clone.GetComponent<Powerup>().TriggerPowerup(_pod);
        yield return new WaitForSeconds(Managers.PowUps.mysteryBoxData.BoxImpactDuration);
    }

}
