using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("移動速度")]
    public float speed = 0.0001f;
    [Header("點擊距離")]
    public float push = 0.5f;
    [Header("玩家物件")]
    public GameObject target;
    [Header("判斷器大小")]
    public Vector2 radius;
    [Header("判斷器位置")]
    public Vector3 offset;
    public float roset;
    [Header("上個位置紀錄")]
    public Vector3 reset;
    public float MaxX, MinX, MaxY, MinY;
    
    public AudioClip soundWrong;
    public State state;

    private GameManager gm;
    private AudioSource aud;

    [Header("方向判斷")]
    private bool click;

    [Header("觸控位置紀錄")]
    public float x;
    public float y;
    private float x1;
    private float y1;
    private float x2;
    private float y2;

    [Header("角度判斷")]
    public float roro = 0;


    private int r = 0;


    private void Awake()
    {
        aud = GetComponent<AudioSource>();
        gm = FindObjectOfType<GameManager>();
    }
    public void Start()
    {

        offset.x = gameObject.transform.position.x;
        offset.y = gameObject.transform.position.y;
        offset.z = gameObject.transform.position.z;
        reset = offset;

        roset = gameObject.transform.rotation.x;
        
    }
    public void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, MinX, MaxX), Mathf.Clamp(transform.position.y, MinY, MaxY), 0);
        Touch();
        Click();
        Move();
        //Win();
    }

    /// <summary>
    /// 觸控位置
    /// </summary>
    private void Touch()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            x1 = Input.mousePosition.x;
            y1 = Input.mousePosition.y;

        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            x2 = Input.mousePosition.x;
            y2 = Input.mousePosition.y;
            x = x2 - x1;
            y = y2 - y1;
            TouchJunge(); //判斷觸控方向

        }

    }

    /// <summary>
    /// 觸控方向
    /// </summary>
    private void TouchJunge()
    {
        switch (state)
        {
            case State.up:
                transform.Translate(0, 0.1f, 0);
                break;
            case State.down:
                transform.Translate(0, -0.1f, 0);
                break;
            case State.left:
                transform.Translate(-0.1f, 0, 0);
                break;
            case State.right:
                transform.Translate(0.1f, 0, 0);
                break;
            case State.idle:
                transform.Translate(0, 0, 0);
                break;
        }
        if (x == 0 && y == 0) // 原點
        {
            click = true;
        }
        else if (x > 0 && y > 0) //第一象限
        {
            if (x > y)
            {
                state = State.right;
            }
            else
            {
                state = State.right;
            }
        }
        else if (x < 0 && y > 0) //第二象限
        {
            x = x * -1;
            if (x > y)
            {
                reset = offset;
                offset.x -= 5;
                gm.Walk();
            }
            else
            {
                reset = offset;
                offset.y += 5;
                gm.Walk();
            }
        }
        else if (x < 0 && y < 0) //第三象限
        {
            if (x > y)
            {
                reset = offset;
                offset.y -= 5f;
                gm.Walk();
            }
            else
            {
                reset = offset;
                offset.x -= 5f;
                gm.Walk();
            }
        }
        else if (x > 0 && y < 0) //第四象限
        {
            y = y * -1;
            if (x > y)
            {
                reset = offset;
                offset.x += 5f;
                gm.Walk();
            }
            else
            {
                reset = offset;
                offset.y -= 5f;
                gm.Walk();
            }
        }

    }

    /// <summary>
    /// 移動方向
    /// </summary>
    public void Click()
    {
        if (click == true && r != 100)
        {
            transform.Rotate(0, 0, -3);
            r++;
            roro++;
        }
        else if (click == true)
        {
            gm.Walk();
            click = false;
        }
        else
        {
            click = false;
            r = 0;
        }

    }
    public void Move()
    {
        transform.position = Vector3.Lerp(transform.position, offset, 5f);
        
    }
    public enum State
    {
        up, down, left, right, idle
    }
}