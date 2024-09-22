using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get { 
            if (instance == null)
            {
                SetupInstance();
            }
            return instance;
        }
    }

    public TMP_InputField nameInputField;
    public TextMeshProUGUI scoreText;
    public int highScore;
    public string PlayerName;
    public string HighScorePlayerName;
    private string savePath;


    private void Awake()
    {
        RemoveDuplicates();
    }

    private void Start()
    {
        savePath = $"{Application.persistentDataPath}/saveData.json";

        LoadHighScore();
        scoreText.text = $"Best Score: {HighScorePlayerName} : {highScore}";
        nameInputField.text = HighScorePlayerName;
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
    

    public void SaveHighScore(int score)
    {
        HighScorePlayerName = PlayerName;
        highScore = score;

        var saveData = new SaveData()
        {
            Name = PlayerName,
            Score = highScore
        };

        var json = JsonUtility.ToJson(saveData);

        File.WriteAllText(savePath, json);
    }

    private void LoadHighScore()
    {
        if(File.Exists(savePath))
        {
            var json = File.ReadAllText(savePath);
            var saveData = JsonUtility.FromJson<SaveData>(json);

            HighScorePlayerName = saveData.Name;
            highScore = saveData.Score;

        }
    }

    private static void SetupInstance()
    {
        instance = FindObjectOfType<GameManager>();
        if (instance == null)
        {
            GameObject gameObj = new GameObject();
            gameObj.name = "GameManager";
            instance = gameObj.AddComponent<GameManager>();
            DontDestroyOnLoad(gameObj);
        }
    }

    private void RemoveDuplicates()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private class SaveData
    {
        public string Name;
        public int Score;
    }
}
