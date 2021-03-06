using UnityEngine;

public class Test_Player : MonoBehaviour
{
    #region - Fields -
    [SerializeField] [Header("角色狀態")] State state;

    float MinX, MaxX;           // X軸邊界
    public float MinY, MaxY;           // Y軸邊界
    public float movespeed;

    public Transform a;
    public Transform b;
    float pa;
    float pb;

    public GameObject _1;
    public GameObject _2;

    Rigidbody2D rig;
    AudioSource aud;
    public AudioClip clip;

    bool mouseTouchPlayer;      // 是否點擊玩家
    public float HoleCD = 5;
    public bool ismoveing;
    float Holetimer;

    Vector3 mousePos;
    Vector3 temp;
    #endregion

    #region - MonoBehaviour -
    void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        aud = GetComponent<AudioSource>();
    }

    void Start()
    {
        Initialize();
    }

    void Update()
    {
        if (rig.velocity == Vector2.zero) ismoveing = false;
    }

    void FixedUpdate()
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "傳送們")
        {
            pa = Vector3.Distance(transform.position, a.position);
            pb = Vector3.Distance(transform.position, b.position);

            if (pa <= pb && Holetimer > 2f)
            {
                transform.position = b.position;
                Holetimer = 0;
            }
            else if (pa >= pb && Holetimer > 2f)
            {
                transform.position = a.position;
                Holetimer = 0;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Walls")
        {
            rig.velocity = Vector2.zero;
            state = State.idle;
        }
        if (col.gameObject.tag == "牆壁")
        {
            transform.position = transform.position;
        }
        if (col.gameObject.tag == "反射")
        {
            if (col.gameObject.name == "2")
            {
                rig.AddForce(Vector2.up * movespeed * Time.deltaTime, ForceMode2D.Impulse);
            }
            else if (col.gameObject.name == "1")
            {
                rig.AddForce(Vector2.right * movespeed * Time.deltaTime, ForceMode2D.Impulse);
            }
        }
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
        if (!ismoveing)
        {
            state = State.idle;

            if (state == State.idle)
            {
                temp = transform.position;
                //a = mousePos.x - temp.x;
                if (mouseTouchPlayer)
                {
                    mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    if (mousePos.x - temp.x > 1) state = State.right;
                    if (mousePos.x - temp.x < -1) state = State.left;
                    if (mousePos.y - temp.y > 1) state = State.up;
                    if (mousePos.y - temp.y < -1) state = State.down;
                    ismoveing = true;
                }
            }

            switch (state)
            {
                case State.up:
                    rig.AddForce(Vector2.up * movespeed * Time.deltaTime, ForceMode2D.Impulse);
                    //transform.Translate(transform.position.x, 0.2f,transform.position.z);
                    break;
                case State.down:
                    rig.AddForce(Vector2.down * movespeed * Time.deltaTime, ForceMode2D.Impulse);
                    //transform.Translate(transform.position.x, -0.2f, transform.position.z);
                    break;
                case State.left:
                    rig.AddForce(Vector2.left * movespeed * Time.deltaTime, ForceMode2D.Impulse);
                    //transform.Translate(-0.2f, transform.position.y, transform.position.z);
                    break;
                case State.right:
                    rig.AddForce(Vector2.right * movespeed * Time.deltaTime, ForceMode2D.Impulse);
                    //transform.Translate(0.2f, transform.position.y, transform.position.z);
                    break;
                case State.idle:
                    break;
            }
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, MinX, MaxX), Mathf.Clamp(transform.position.y, MinY, MaxY), 0);

            aud.PlayOneShot(clip);
        }
    }
    #endregion

    public void GoUP(float x = 1)
    {
        rig.AddForce(Vector2.up * movespeed * Time.deltaTime * x, ForceMode2D.Impulse);
    }
}
public enum State
{
    up, down, left, right, idle
}
