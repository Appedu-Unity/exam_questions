using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [Header("移動步數")]
    public int move;
    
    [Header("玩家物件")]
    public GameObject play;


    public static int star = 3;

    private void Start()
    {
        move = 0;
    }
    private void Update()
    {

        
        
    }
    /// <summary>
    /// 減少移動步數
    /// </summary>
    public void Walk()
    {
        move++;
    }
    
    
    /// <summary>
    /// 遊戲過關紀錄星星
    /// </summary>
    public void GameWIN()
    {
        if (PlayerPrefs.GetInt("L") <= SceneManager.GetActiveScene().buildIndex)
        {

            PlayerPrefs.SetInt("L", SceneManager.GetActiveScene().buildIndex);
            print(PlayerPrefs.GetInt("L"));
        }
        play.SetActive(false);

    }



}