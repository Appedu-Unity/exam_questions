using UnityEngine;

public class Test_Player : MonoBehaviour
{
    #region - Fields -
    [SerializeField] [Header("角色狀態")] State state;

    float MinX, MaxX;           // X軸邊界
    float MinY, MaxY;           // Y軸邊界

    bool mouseTouchPlayer;      // 是否點擊玩家

    Vector3 mousePos;
    Vector3 temp;
    #endregion

    #region - MonoBehaviour -
    void Start()
    {
        Initialize();
    }

    void Update()
    {
        Movement();
    }

    void OnMouseDown()
    {
        mouseTouchPlayer = true;
    }

    void OnMouseUp()
    {
        mouseTouchPlayer = false;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Walls")
        {
            print("hi");
            Debug.Log("hi");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7) Destroy(collision.gameObject);
    }
    #endregion

    #region - Methods -
    /// <summary>
    /// 初始化
    /// </summary>
    void Initialize()
    {
        MinX = -7.5f;
        MaxX = 7.5f;
        MinY = -4.3f;
        MaxY = 4.2f;

        state = State.idle;
        mousePos = new Vector3(-5.85f, -2.4f, 0);
    }

    /// <summary>
    /// 移動行為
    /// </summary>
    void Movement()
    {
        if (state == State.idle)
        {
            temp = transform.position;

            if (mouseTouchPlayer)
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                if (mousePos.x - temp.x > 1) state = State.right;
                if (mousePos.x - temp.x < -1) state = State.left;
                if (mousePos.y - temp.y > 1) state = State.up;
                if (mousePos.y - temp.y < -1) state = State.down;
            }
        }

        switch (state)
        {
            case State.up:
                transform.Translate(0, 0.2f, 0);
                break;
            case State.down:
                transform.Translate(0, -0.2f, 0);
                break;
            case State.left:
                transform.Translate(-0.2f, 0, 0);
                break;
            case State.right:
                transform.Translate(0.2f, 0, 0);
                break;
            case State.idle:
                transform.position += Vector3.zero;
                break;
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, MinX, MaxX), Mathf.Clamp(transform.position.y, MinY, MaxY), 0);
    }
    #endregion
}

public enum State
{
    up, down, left, right, idle
}
