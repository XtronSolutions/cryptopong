using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddlePlayer : BasePaddle
{
    private Vector2 DefaultPosition;

    // Start is called before the first frame update
    protected override void Start()
    {

    }

    // Update is called once per frame
    protected override void Update()
    {
        if (Managers.Input.isActive)
        {
            if (Managers.Input.inputType == InputMethod.KeyboardInput)
                KeyboardInput();
            else if (Managers.Input.inputType == InputMethod.TouchLRInput)
                TouchLRInput();
            else
                DragInput();
        }
    }

    void KeyboardInput()
    {
        float direction = Input.GetAxisRaw("Vertical");
        CheckMovementBlock(direction);
    }

    void TouchLRInput()
    {
        float direction = 0;

        if (Input.GetMouseButton(0))
        {
            direction = (Input.mousePosition.y > Screen.width / 2) ? 1 : -1;
        }
        CheckMovementBlock(direction);
    }

    void DragInput()
    {
        Vector3 curScreenPoint = new Vector3(0, Input.mousePosition.y, 0);

        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
        curPosition.y = Mathf.Clamp(curPosition.y, -MaxBound, MaxBound);;
        curPosition.z = 0;
        curPosition.x = DefaultPosition.x;
        transform.position = curPosition;
    }

    void CheckMovementBlock(float dir)
    {
        float nextFramePosX = Mathf.Abs((new Vector2(0, dir) * speed * Time.deltaTime).y + transform.position.y);

        if (nextFramePosX < MaxBound)
        {
            transform.Translate(new Vector2(0, dir) * speed * Time.deltaTime);
        }
    }
}
