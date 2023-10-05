using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameState
{
    READY,
    ONGOING,
    GAMEOVER
}
public class GameManager : MonoBehaviour
{
    public GameState State;
    public static GameManager instance
    {
        get
        {
            if (mInstance == null)
            {
                return null;
            }

            return mInstance;
        }
    }
    private static GameManager mInstance = null;

    void Awake()
    {
        if (mInstance == null)
        {
            mInstance = this;
        }

        State = GameState.READY;
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        
    }
    public void GameOver()
    {
        #if UNITY_EDITOR
        //UnityEditor.EditorApplication.isPlaying = false;
        Debug.Log("게임 오버");
        #else
        
        #endif

    }
}
