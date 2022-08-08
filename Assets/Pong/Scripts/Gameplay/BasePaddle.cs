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
using Fusion;

public enum PaddleOwner
{
    PLAYER,
    AI
}

public class BasePaddle : NetworkBehaviour
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

    protected virtual void Start()
    {
        _ball = Managers.Match.ball;
    }

    protected virtual void OnEnable()
    {
        if (!Animator)
            Animator = GetComponentInChildren<Animator>();
    }

    protected virtual void Update()
    {

    }
}
