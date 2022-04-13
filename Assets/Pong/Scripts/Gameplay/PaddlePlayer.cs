using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddlePlayer : BasePaddle
{
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
        float direction = Input.GetAxisRaw("Horizontal");
        CheckMovementBlock(direction);
    }

    void TouchLRInput()
    {
        float direction = 0;

        if (Input.GetMouseButton(0))
        {
            direction = (Input.mousePosition.x > Screen.width / 2) ? 1 : -1;
        }
        CheckMovementBlock(direction);
    }

    void DragInput()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, 0, 0);

        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
        curPosition.y = -4;
        curPosition.z = 0;
        curPosition.x = Mathf.Clamp(curPosition.x, -MaxBound, MaxBound);
        transform.position = curPosition;
    }

    void CheckMovementBlock(float dir)
    {
        float nextFramePosX = Mathf.Abs((new Vector2(dir, 0) * speed * Time.deltaTime).x + transform.position.x);

        if (nextFramePosX < MaxBound)
        {
            transform.Translate(new Vector2(dir, 0) * speed * Time.deltaTime);
        }
    }
}
