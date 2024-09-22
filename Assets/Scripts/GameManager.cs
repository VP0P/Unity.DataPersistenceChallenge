using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TMP_InputField nameInputField;
    public TextMeshProUGUI scoreText;
    public int highScore;
    public string PlayerName;
    private string savePath;


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
        savePath = $"{Application.persistentDataPath}/saveData.json";

        LoadHighScore();
        scoreText.text = $"Best Score: {PlayerName} : {highScore}";
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        SaveHighScore();


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
    

    private void SaveHighScore()
    {
        var saveData = new SaveData()
        {
            Name = PlayerName,
            Score = highScore
        };

        var json = JsonUtility.ToJson(saveData);

        Debug.Log(savePath);
        Debug.Log(json);

        File.WriteAllText(savePath, json);
    }

    private void LoadHighScore()
    {
        if(File.Exists(savePath))
        {
            var json = File.ReadAllText(savePath);
            var saveData = JsonUtility.FromJson<SaveData>(json);

            PlayerName = saveData.Name;
            highScore = saveData.Score;
        }
    }

    private class SaveData
    {
        public string Name;
        public int Score;
    }
}
