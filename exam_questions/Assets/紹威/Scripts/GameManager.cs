using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


public class GameManager : MonoBehaviour
{
    public List<Sprite> door = new List<Sprite>();
    public SpriteRenderer indoor;
    Test_Player player;
    int index = 0;

    private void Start()
    {
        //先把門初始化
        indoor.sprite = door[index];
        player = GameObject.Find("Player").GetComponent<Test_Player>();
    }

    public void OpenDoor()
    {
        index++;
        if (index >= door.Count)
        {
            player.MinY = -8f;
            Destroy(indoor.gameObject);
        }
        else
        {
            indoor.sprite = door[index];
        }
    }
}