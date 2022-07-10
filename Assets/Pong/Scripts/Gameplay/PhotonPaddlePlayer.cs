using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Photon.Pun;

public class PhotonPaddlePlayer : BasePaddle, IPunObservable, IPunInstantiateMagicCallback
{
    public Vector2 DefaultPosition;
    public RectTransform YBoundsRef;
    public Transform[] XBoundsRef;
    public float diff;

    [DllImport("__Internal")]
    private static extern bool IsMobile();
    private CharactersDatabase Database => Databases.CharactersDatabase;

    private float deltaY;
    private float deltaX;
    private float directionX = 0;
    private float directionY = 0;
    private Vector3 pos = Vector3.zero;
    private float storedXCursor;
    private Vector3 lastMousePos;
    private bool[] isKeyboard = new bool[2];

    [SerializeField] private Transform charContainer, textContainer;
    [SerializeField] private Text charNameText;

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
        this.YBoundsRef = PhotonGameManager.Instance.YBounds;
        this.charNameText.text = PhotonNetwork.NickName;
    }

    public override void OnEnable()
    {
        base.OnEnable();

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
        if (photonView.IsMine)
        {
            DragInput();
        }
    }

    void TouchLRInput()
    {
        float direction = 0;

        if (ControlFreak2.CF2Input.GetMouseButton(0))
            direction = (ControlFreak2.CF2Input.mousePosition.y > Screen.width / 2) ? 1 : -1;

        //CheckMovementBlock(direction);
    }

    void DragInput()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
        storedXCursor = curPosition.x;
        curPosition.x = transform.position.x;
        curPosition.z = transform.position.z;

        isKeyboard[0] = Mathf.RoundToInt(Input.GetAxisRaw("Vertical")) != 0;
        isKeyboard[1] = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal")) != 0;
        if (isKeyboard[0] || isKeyboard[1])
        {
            if (!lastMousePos.Equals(curPosition))
                lastMousePos = curPosition;

            directionX = 0;
            directionY = 0;

            switch (Constants.Mode)
            {
                case GameMode.FREESTYLE:
                    // directionY = Mathf.Clamp((Input.GetAxisRaw("Vertical")), -1, 1);
                    directionX = Mathf.Clamp((Input.GetAxisRaw("Horizontal")), -1, 1);
                    break;
            }

            Debug.Log(directionX + " " + directionY);
            directionY = Mathf.Clamp((Input.GetAxisRaw("Vertical")), -1, 1);
            CheckMovementBlock(directionX, directionY);
        }
        else
        {
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    deltaY = (transform.position.y - curPosition.y);
                    deltaX = (transform.position.x - storedXCursor);
                }

                if (Input.GetMouseButton(0))
                {
                    switch (Constants.Mode)
                    {
                        case GameMode.PRACTICE:
                            // curPosition.y = Mathf.Clamp(curPosition.y + deltaY, -YBoundsRef.position.y, YBoundsRef.position.y);
                            break;
                        case GameMode.FREESTYLE:
                            curPosition.y = Mathf.Clamp(curPosition.y + deltaY, -YBoundsRef.position.y, YBoundsRef.position.y);
                            curPosition.x = Mathf.Clamp(storedXCursor + deltaY, XBoundsRef[1].position.x, XBoundsRef[0].position.x);
                            break;
                    }
                 
                    curPosition.y = Mathf.Clamp(curPosition.y, -YBoundsRef.position.y, YBoundsRef.position.y);
                    transform.position = curPosition;
                }
            }
            else
            {
                var hasMouseMoved = Mathf.RoundToInt(Mathf.Abs(curPosition.y - lastMousePos.y)) > 0 || Mathf.RoundToInt(Mathf.Abs(curPosition.x - lastMousePos.x)) > 0;
                if (!hasMouseMoved)
                    return;

                lastMousePos = Vector3.zero;

                switch (Constants.Mode)
                {
                    case GameMode.PRACTICE:
                        // curPosition.y = Mathf.Clamp(curPosition.y, -YBoundsRef.position.y, YBoundsRef.position.y);
                        break;
                    case GameMode.FREESTYLE:
                        curPosition.y = Mathf.Clamp(curPosition.y, -YBoundsRef.position.y, YBoundsRef.position.y);
                        curPosition.x = Mathf.Clamp(storedXCursor, XBoundsRef[1].position.x, XBoundsRef[0].position.x);
                        break;
                }

                curPosition.y = Mathf.Clamp(curPosition.y, -YBoundsRef.position.y, YBoundsRef.position.y);
                transform.position = curPosition;
            }
        }
    }

    void CheckMovementBlock(float dirX, float dirY)
    {
        transform.Translate(new Vector2(dirX, dirY) * speed * Time.deltaTime);
        pos = transform.position;

        switch (Constants.Mode)
        {
            case GameMode.PRACTICE:
                diff = YBoundsRef.position.y;
                pos.y = Mathf.Clamp(pos.y, -diff, diff);
                break;
            case GameMode.FREESTYLE:
                diff = YBoundsRef.position.y;
                pos.y = Mathf.Clamp(pos.y, -diff, diff);
                pos.x = Mathf.Clamp(pos.x, XBoundsRef[1].position.x, XBoundsRef[0].position.x);
                break;
        }

        transform.position = pos;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // throw new System.NotImplementedException();
    }

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        if (info.photonView.IsMine)
        {
            info.photonView.transform.SetParent(PhotonGameManager.Instance.PlayerSpawnPointA);
            info.photonView.transform.SetPositionAndRotation(PhotonGameManager.Instance.PlayerSpawnPointA.position, PhotonGameManager.Instance.PlayerSpawnPointA.rotation);
            info.photonView.transform.localScale = Vector3.one;
        }
        else
        {
            info.photonView.transform.SetParent(PhotonGameManager.Instance.PlayerSpawnPointB);
            info.photonView.transform.SetPositionAndRotation(PhotonGameManager.Instance.PlayerSpawnPointB.position, PhotonGameManager.Instance.PlayerSpawnPointB.rotation);
            info.photonView.transform.localScale = Vector3.one;

            this.charContainer.localScale = Vector3.one;
            this.textContainer.localScale = Vector3.one;
        }
    }
}
