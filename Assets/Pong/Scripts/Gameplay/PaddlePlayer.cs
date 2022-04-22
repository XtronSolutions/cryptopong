using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaddlePlayer : BasePaddle
{
    public Vector2 DefaultPosition;
    public RectTransform YBoundsRef;
    public float diff;
    // Start is called before the first frame update
    protected override void Start()
    {

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

    void DragInput()
    {
        Vector3 curScreenPoint = new Vector3(0, Input.mousePosition.y, 0);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
        curPosition.x = transform.position.x;
        curPosition.z = transform.position.z;

        if (Application.isMobilePlatform)
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
            curPosition.y = Mathf.Clamp(curPosition.y, -YBoundsRef.position.y, YBoundsRef.position.y);
        }
        
        transform.position = curPosition;
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
