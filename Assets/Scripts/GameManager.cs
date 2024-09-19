using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TMP_InputField nameInputField;
    public TextMeshProUGUI scoreText;
    public int score;
    public string PlayerName;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        scoreText.text = $"Best Score: : {score}";
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
#if (UNITY_EDITOR)
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void SetPlayerName()
    {
        PlayerName = nameInputField.text;
    }
    
}
