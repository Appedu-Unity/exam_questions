using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [Header("���ʨB��")]
    public int move;
    
    [Header("���a����")]
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
    /// ��ֲ��ʨB��
    /// </summary>
    public void Walk()
    {
        move++;
    }
    
    
    /// <summary>
    /// �C���L�������P�P
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