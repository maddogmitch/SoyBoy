using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerName : MonoBehaviour
{
    private InputField input;

    // Start is called before the first frame update
    void Start()
    {
        //locating and catching inputfield component and add a listener
        input = GetComponent<InputField>();
        input.onValueChanged.AddListener(SavePlayerName);
        //Use playerprefs to look and retrieve the value key named PlayerName
        var savedName = PlayerPrefs.GetString("PlayerName");
        if(!string.IsNullOrEmpty(savedName))
        {
            input.text = savedName;
            GameManager.instance.playerName = savedName;
        }
        
    }

    private void SavePlayerName(string playerName)
    {
        //Sets the supplied playername to the playername
        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.Save();
        GameManager.instance.playerName = playerName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
