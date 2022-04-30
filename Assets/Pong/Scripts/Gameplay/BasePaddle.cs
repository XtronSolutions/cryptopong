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

public enum PaddleOwner
{
    PLAYER,
    AI
}

public class BasePaddle : MonoBehaviour
{
    public float speed;
    [HideInInspector]
    public Vector2 scale;
    int cnt = 0;
    #region Private Vars
    protected Ball _ball;
    protected Rigidbody2D _rigidBody;
    [SerializeField] protected float MaxBound = 2.36f;
    [SerializeField] protected Animator Animator;
    #endregion

    protected virtual void Start()
    {
        _ball = Managers.Match.ball;
        _rigidBody = GetComponent<Rigidbody2D>();
        Animator = GetComponentInChildren<Animator>();
    }

    protected virtual void OnEnable()
    {
    }

    protected virtual void Update()
    {

    }
}
