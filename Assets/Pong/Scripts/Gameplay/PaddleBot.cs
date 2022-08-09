using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleBot : BasePaddle
{
    [SerializeField] private float ReactionSpeed;
    [SerializeField] private float FreeStyleReactionSpeed;

    [Range(1, 3), Header("Dumb - Smart - Tony Stark")]
    [SerializeField] protected int Intelligence = 3;
    [SerializeField] private RuntimeAnimatorController[] Characters;

    [SerializeField] private Transform[] XBounds;
    private CharactersDatabase Database => Databases.CharactersDatabase;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Events.OnIntelligenceChanged += OnIntelligenceChanged;
    }

    public override void OnEnable()
    {
        int CharIndex = Random.Range(0, Characters.Length);
        base.Animator.runtimeAnimatorController = Characters[CharIndex];
        base.Animator.GetComponent<RectTransform>().sizeDelta = Database.GetCharacterOfIndex(CharIndex).GetImageSize; 

        if (Constants.Mode == GameMode.CLASSIC)
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        else if (Constants.Mode == GameMode.FREESTYLE)
        {
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            gameObject.GetComponent<Rigidbody2D>().mass = 0.001f;
        }
    }

    private void OnIntelligenceChanged(int value) => Intelligence = value;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ball")
        {
            if (base.Animator)
            {
                base.Animator.Play("Attack");
            }
        }
    }

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
            if (_ball.transform.position.y > transform.position.y + 0.410f) //for up and right movement
            {
                if (_rigidBody.velocity.y < 0)
                    _rigidBody.velocity = Vector2.zero;

                switch (Constants.Mode)
                {
                    case GameMode.CLASSIC:
                        _rigidBody.velocity = Vector2.up * ReactionSpeed;
                        break;
                    case GameMode.FREESTYLE:
                        _rigidBody.velocity = new Vector2(1, 1) * FreeStyleReactionSpeed;

                        if (transform.position.x > XBounds[1].position.x)
                            _rigidBody.velocity = new Vector2(0, _rigidBody.velocity.y);
                        
                        break;
                }
            }
            else if (_ball.transform.position.y < transform.position.y - 0.410f)
            {
                if (_rigidBody.velocity.y > 0)
                    _rigidBody.velocity = Vector2.zero;

                switch (Constants.Mode)
                {
                    case GameMode.CLASSIC:
                        _rigidBody.velocity = Vector2.down * ReactionSpeed;
                        break;
                    case GameMode.FREESTYLE:
                        _rigidBody.velocity = new Vector2(-1, -1) * FreeStyleReactionSpeed;

                        if (transform.position.x < XBounds[0].position.x)
                            _rigidBody.velocity = new Vector2(0, _rigidBody.velocity.y);

                        break;
                }
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
