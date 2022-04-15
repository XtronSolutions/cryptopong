using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleBot : BasePaddle
{
    [SerializeField] private float ReactionSpeed;

    [Range(1, 3), Header("Dumb - Smart - Tony Stark")]
    [SerializeField] protected int Intelligence = 3;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Events.OnIntelligenceChanged += OnIntelligenceChanged;
    }

    private void OnIntelligenceChanged(int value) => Intelligence = value;

    // Update is called once per frame
    protected override void Update()
    {
        var speed = base.speed;
        float lvl = ((float)Intelligence / 3f);
        speed *= lvl;
        ReactionSpeed = speed;

        AIControl();
    }

    void AIControl()
    {
        if (Mathf.Sign(transform.position.x) == Mathf.Sign(_ball.ballBody.velocity.x))
        {
            if (_ball.transform.position.y > transform.position.y + 0.410f)
            {
                if (_rigidBody.velocity.y < 0)
                    _rigidBody.velocity = Vector2.zero;

                _rigidBody.velocity = Vector2.up * ReactionSpeed;
            }
            else if (_ball.transform.position.y < transform.position.y - 0.410f)
            {
                if (_rigidBody.velocity.y > 0)
                    _rigidBody.velocity = Vector2.zero;

                _rigidBody.velocity = Vector2.down * ReactionSpeed;
            }
            else
            {
                _rigidBody.velocity = Vector2.zero;
            }
        }
        else
            _rigidBody.velocity = Vector2.zero;
    }
}
