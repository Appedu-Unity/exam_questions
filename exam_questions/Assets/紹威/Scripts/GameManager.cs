using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


public class GameManager : MonoBehaviour
{
    public List<Sprite> door = new List<Sprite>();
    public SpriteRenderer indoor;
    int index = 0;
    private void Start()
    {
        //�������l��
        indoor.sprite = door[index];
    }
    public void OpenDoor()
    {
        index++;
        if (index >= door.Count)
        {
            Destroy(indoor.gameObject);
        }
        else
        {
            indoor.sprite = door[index];
        }
    }
}