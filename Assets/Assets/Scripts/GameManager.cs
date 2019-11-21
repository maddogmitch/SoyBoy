using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject buttonPrefab;
    private string selectedLevel;
    public string playerName;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void SetLevelName(string levelFilePath)
    {
        selectedLevel = levelFilePath;
        SceneManager.LoadScene("Game");
    }

    private void DiscoverLevels()
    {
        var levelPanelRectTransform =
            GameObject.Find("LevelItemsPanel")
            .GetComponent<RectTransform>();
        var levelFiles = Directory.GetFiles(Application.dataPath, "*.json");
    }

    public void RestartLevel(float delay)
    {
        StartCoroutine(RestartLevelDelay(delay));
    }
    private IEnumerator RestartLevelDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Game");
    }

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<PlayerTimeEntry> LoadPreviousTimes()
    {
        //to attempt to load saved time entries 
        try
        {
            var scoresFiles = Application.persistentDataPath +
                "/" + playerName + "_time.dat";
            using (var stream = File.Open(scoresFiles, FileMode.Open))
            {
                var bin = new BinaryFormatter();
                var times = (List<PlayerTimeEntry>)bin.Deserialize(stream);
                return times;
            }
        }
        //if unsuccesful output what was and why
        catch(IOException ex)
        {
            Debug.LogWarning("Couldn't load previous times for: " +
                playerName + ", Exception: " + ex.Message);
            return new List<PlayerTimeEntry>();
        }
    }

    public void SaveTime(decimal time)
    {
        //saving a time by fetching existing times first
        var times = LoadPreviousTimes();
        //create new instance of player time entry
        var newTime = new PlayerTimeEntry();
        newTime.entryDate = DateTime.Now;
        newTime.time = time;
        //create a binary formatter object to do the packing of the list of player names and times
        var bFormatter = new BinaryFormatter();
        var filePath = Application.persistentDataPath +
            "/" + playerName + "_times.dat";
        using (var file = File.Open(filePath, FileMode.Create))
        {
            times.Add(newTime);
            bFormatter.Serialize(file, times);
        }
    }

    public void DisplayPreviousTimes()
    {
        //collects existing items
        var times = LoadPreviousTimes();
        var topThree = times.OrderBy(time => time.time).Take(3);
        //finds the previous times
        var timesLabel = GameObject.Find("PreviousTimes")
            .GetComponent<Text>();
        //changes it to show each time found
        timesLabel.text = "BEST TIMES \n";
        foreach (var time in topThree)
        {
            timesLabel.text += time.entryDate.ToShortDateString() +
                ": " + time.time + "\n";
        }
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode loadsceneMode)
    {
        if(scene.name == "Game")
        {
            DisplayPreviousTimes();
        }
    }
}
