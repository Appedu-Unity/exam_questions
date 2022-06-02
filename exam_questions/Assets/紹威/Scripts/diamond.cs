using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diamond : MonoBehaviour
{
    public AudioClip eataud;
    private AudioSource aud;
    private GameManager gm;
    private void Awake()
    {
        aud = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        gm=GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            aud.PlayOneShot(eataud);
            gm.OpenDoor();
            Destroy(gameObject);
        }
    }
}
