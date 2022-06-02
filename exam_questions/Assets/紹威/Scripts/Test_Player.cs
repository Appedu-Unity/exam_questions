using UnityEngine;

public class Test_Player : MonoBehaviour
{
    #region - Fields -
    [SerializeField] [Header("角色狀態")] State state;

    float MinX, MaxX;           // X軸邊界
    float MinY, MaxY;           // Y軸邊界
    public float movespeed;

    public Transform a;
    public Transform b;
    float pa;
    float pb;

    public GameObject _1;
    public GameObject _2;

    private Rigidbody2D rig;
    bool mouseTouchPlayer;      // 是否點擊玩家
    public float timer = 2;
    bool isHole;
    public float HoleCD = 5;
    public bool ismoveing;
    float Holetimer;

    Vector3 mousePos;
    Vector3 temp;
    #endregion

    #region - MonoBehaviour -
    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        Initialize();
    }

    void Update()
    {
        print(Holetimer);
        //print(rig.velocity);
        if (rig.velocity == Vector2.zero)
            ismoveing = false;
    }
    private void FixedUpdate()
    {
        timer++;
        if (timer > 10)
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
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "傳送們")
        {
            pa = Vector3.Distance(transform.position, a.position);
            pb = Vector3.Distance(transform.position, b.position);


            if (pa <= pb && Time.time >= Holetimer)
            {
                isHole = true;
                transform.position = b.position;
                //現在的時間加上冷卻時間
                Holetimer = Time.time + HoleCD;
            }
            else
            {
                isHole = false;
            }

            if (pa >= pb && Time.time >= Holetimer)
            {
                isHole = false;
                transform.position = a.position;
                //現在的時間加上冷卻時間
                Holetimer = Time.time + HoleCD;
            }
            else
            {
                isHole = true;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Walls")
        {
            ismoveing = false;
            state = State.idle;
            //print("hi");
            //Debug.Log("hi");
        }
        if (col.gameObject.tag == "牆壁")
        {
            transform.position = transform.position;
        }
        if (col.gameObject.tag == "反射")
        {
            if (col.gameObject.name == "2")
            {
                GoUP();
            }
            else if (col.gameObject.name == "1")
            {
                rig.AddForce(Vector2.right * movespeed * 5000, ForceMode2D.Impulse);
            }
        }
    }

    /*void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7) Destroy(collision.gameObject);
    }*/
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
