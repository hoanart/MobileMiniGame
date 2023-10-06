using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public int Score
    {
        get => mScore;
        set
        {
            mScore = value;
        }
    }
    private static GameManager mInstance = null;

    [SerializeField]
    private int mScore;

    [SerializeField]
    private TMP_Text mFinalScore;
    [SerializeField]
    private GameObject gameoverPanel;

    public delegate int DeleChange();

    public event DeleChange OnChangeScore;
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
        if (gameoverPanel == null)
        {
            Debug.LogError("Add GameOverPanel in the Scene");
            return;
        }

        mFinalScore.text = OnChangeScore().ToString();
        gameoverPanel.SetActive(true);
        Debug.Log("게임 오버");
    }

    public void Retry()
    {
        SceneManager.LoadScene(0);

    }

    public void Quit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
        
    }
}
