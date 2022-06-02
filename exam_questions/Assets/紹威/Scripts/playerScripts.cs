using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScripts : MonoBehaviour
{
    public float moveSpeed = 20f;
    public State state;

    private bool mousetouchplayer;
    private bool ismagnet;
    private bool isup;
    private bool isdown;
    private bool isleft;
    private bool isright;

    private Rigidbody2D rig;
    private Animator ani;

    Vector2 moveMent;


    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }
    void Start()
    {

    }
    void Update()
    {
        Move2();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "���")
        {
            state = State.idle;
        }
    }
    private void OnMouseDown()
    {
        mousetouchplayer = true;
    }
    private void OnMouseUp()
    {
        mousetouchplayer = false;
    }
    private void Move()
    {
        /*//�������I�� = ��J �� ���o�b�V("����") - ���kAD
        float v = Input.GetAxis("Horizontal");

        //�������I�� = ��J �� ���o�b�V("����") - ���kAD
        float h = Input.GetAxis("Vertical");
        //���� �� �[�t�� = �s �G���V�q(�������I�� * �t�סA����[�t��Y)
        rig.velocity = new Vector2(h * moveSpeed, rig.velocity.y);
        //���� �� �[�t�� = �s �G���V�q(�������I�� * �t�סA����[�t��Y)
        rig.velocity = new Vector2(v * moveSpeed, rig.velocity.x);*/
    }
    private void Move2()
    {

        if (mousetouchplayer)
        {
            if (state == State.idle)
            {
                float x = Input.GetAxis("Mouse X");
                Debug.Log("X=" + x);
                float y = Input.GetAxis("Mouse Y");
                Debug.Log("Y =" + y);
                if (x > 0)
                {
                    state = State.right;
                }
                else if (x < 0)
                {
                    state = State.left;
                }
                else if (y < 0)
                {
                    state = State.down;
                }
                else if (y > 0)
                {
                    state = State.up;
                }
            }
        }
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
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -20.6f, -6f), Mathf.Clamp(transform.position.y, -17.7f, -9.5f), 0f);

    }
    public enum State
    {
        up, down, left, right, idle
    }
}
