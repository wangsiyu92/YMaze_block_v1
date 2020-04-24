using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class ScenesSwitch : MonoBehaviour
{
    public Text a;
    public static string Text_;
    public Text b;
    string num;
    public Button buttonA;
    public InputField inputID;
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        buttonA.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        if(i == 0)
        {
            ScenesSwitch.Text_ = inputID.text;
            num = "PSYuserID"+inputID.text.ToString();
            WriteFileByLine(Application.persistentDataPath, num, "The user id is: " + inputID.text);
            SceneManager.LoadScene("Introduction");
            i = 1;
        }
    }

    public void WriteFileByLine(string file_path, string file_name, string str_info)
    {
        StreamWriter sw;
        FileInfo file_info = new FileInfo(file_path + "//" + file_name);
        if (!file_info.Exists)
        {
            sw = file_info.CreateText();
            Debug.Log("File Created！");
        }
        else
        {
            sw = file_info.AppendText();
        }
        sw.WriteLine(str_info);
        sw.Close();
        sw.Dispose();
    }
}
