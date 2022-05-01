using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class PaddlePlayer : BasePaddle
{
    public Vector2 DefaultPosition;
    public RectTransform YBoundsRef;
    public float diff;

    [DllImport("__Internal")]
    private static extern bool IsMobile();
    private CharactersDatabase Database;

    public bool isMobile()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
         return IsMobile();
#endif
        return false;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        if (!Database)
            Database = Resources.Load<CharactersDatabase>(nameof(CharactersDatabase));
            
        if (base.Animator)
            base.Animator.runtimeAnimatorController = Database.GetSelectedCharacter.AnimatorController;
        else
            Debug.LogError("Animator not assigned.");
    }

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
        if (Managers.Input.isActive)
        {
            // if (Managers.Input.inputType == InputMethod.KeyboardInput)
            //     KeyboardInput();
            // else if (Managers.Input.inputType == InputMethod.TouchLRInput)
            //     TouchLRInput();
            // else
            DragInput();

            // KeyboardInput();
        }
    }

    void KeyboardInput()
    {
        // float direction = Mathf.Clamp((Input.GetAxis("Mouse Y") + Input.GetAxis("Vertical")), -1, 1);
        // CheckMovementBlock(direction);
    }

    void TouchLRInput()
    {
        float direction = 0;

        if (ControlFreak2.CF2Input.GetMouseButton(0))
        {
            direction = (ControlFreak2.CF2Input.mousePosition.y > Screen.width / 2) ? 1 : -1;
        }
        CheckMovementBlock(direction);
    }

    private float deltaY;
    private Vector3 lastMousePos;
    void DragInput()
    {
        Vector3 curScreenPoint = new Vector3(0, Input.mousePosition.y, 0);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
        curPosition.x = transform.position.x;
        curPosition.z = transform.position.z;

        var isKeyboard = Mathf.RoundToInt(Input.GetAxis("Vertical")) != 0;
        if (isKeyboard)
        {
            if (!lastMousePos.Equals(curPosition))
                lastMousePos = curPosition;

            float direction = Mathf.Clamp((Input.GetAxis("Vertical")), -1, 1);
            CheckMovementBlock(direction);
        }
        else
        {
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                if (Input.GetMouseButtonDown(0))
                    deltaY = (transform.position.y - curPosition.y);

                if (Input.GetMouseButton(0))
                {
                    curPosition.y = Mathf.Clamp(curPosition.y + deltaY, -YBoundsRef.position.y, YBoundsRef.position.y);
                    transform.position = curPosition;
                }
            }
            else
            {
                var hasMouseMoved = Mathf.RoundToInt(Mathf.Abs(curPosition.y - lastMousePos.y)) > 0 || Mathf.RoundToInt(Mathf.Abs(curPosition.x - lastMousePos.x)) > 0;
                if (!hasMouseMoved)
                    return;

                lastMousePos = Vector3.zero;
                curPosition.y = Mathf.Clamp(curPosition.y, -YBoundsRef.position.y, YBoundsRef.position.y);
                transform.position = curPosition;
            }
        }
    }

    void CheckMovementBlock(float dir)
    {
        transform.Translate(new Vector2(0, dir) * speed * Time.deltaTime);

        var pos = transform.position;
        diff = YBoundsRef.position.y;
        pos.y = Mathf.Clamp(pos.y, -diff, diff);
        transform.position = pos;
    }
}
