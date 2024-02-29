using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{

    public Slider slider;
    public Toggle toggle;
    public int highscore;
    void Awake()
    {
        LoadGame();
    }

    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file;
        if (File.Exists(Application.persistentDataPath + "/save.dat"))
        {
            file = File.Open(Application.persistentDataPath + "/save.dat", FileMode.Open);
        }
        else 
        {
            file = File.Create(Application.persistentDataPath + "/save.dat");
        }
        SaveData data = new SaveData();
        data.volumeSlider = slider.value;
        data.fsToggle = toggle.isOn;
        data.highscore = highscore;
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Save successful");
    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/save.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/save.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            slider.value = data.volumeSlider;
            toggle.isOn = data.fsToggle;
            highscore = data.highscore;
            file.Close();
        }
    }
}

[Serializable]
public class SaveData
{
    public float volumeSlider;
    public bool fsToggle;
    public int highscore;
    public SaveData(){}
}
