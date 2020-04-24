using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class setPosition : MonoBehaviour
{
    //GameSetting
    public GameObject bottom;
    public GameObject Drop;
    public GameObject holder;
    public GameObject DropCover;

    //Score Position
    public GameObject theScoreYouGot;
    public GameObject theScoreYouWillGet;
    float xPositionGotWill = 0;
    float yPositionGot = 0;
    float adjust = 0;
    float yPositionWill = 0;
    //Score text part
    int score = 0;
    int point = 1;
    int roundScore = 0;
    float original = 0f;

    private GameObject bottom_1;
    private GameObject bottom_2;
    private GameObject bottom_3;
    private GameObject bottom_4;
    private GameObject bottom_5;
    private GameObject bottom_6;
    private GameObject bottom_7;
    private GameObject Drop_;
    private GameObject DropCover_;
    private GameObject Temp;

    Vector3 bottomPosition;
    Vector3 dropPosition;
    Vector3 dropCoverPosition;
    Vector3 holderPosition;

    int trig = 0;
    public Text intro;
    private string num;

    //Text part
    public Text ScoreText;
    public Text IndexScore;
    public Slider slider;
    //Milliseconds
    float milliseconds, seconds, minutes, hours;

    //Position part
    float samplepos = 0;
    float pos = 0;
    int index = 1;
    float height = -3.0f;

    //Button part
    float markTime = 0;

    //Game start setting
    void Start()
    {
        num = "PSYuserID" + ScenesSwitch.Text_;
        set();
        ScoreText.text = score.ToString();
        IndexScore.text = point.ToString();
    }

    // Checking KeyBoard Info
    void Update()
    {
        hours = (int)(Time.timeSinceLevelLoad / 3600f);
        minutes = (int)(Time.timeSinceLevelLoad / 60f) % 60;
        seconds = (int)(Time.timeSinceLevelLoad % 60f);
        milliseconds = (int)(Time.timeSinceLevelLoad * 1000f) % 1000;
        if (trig == 0)
        {
            intro.text = "Welcome to the experiment, click Space to start";
        }
        else
        {
            intro.text = "";
        }

        if (Input.GetKeyDown("space"))
        {
            trig = 1;
            ClickButtonA();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ClickButtonB();
        }
        point = rewardEval(index, original, pos);
        IndexScore.text = point.ToString();
        if (index == 8)
        {
            WriteFileByLine(Application.persistentDataPath, num, " Success, X Position: " + pos + " Y position" + height + " current round score is "+ score + " System time: " + hours + ":" + minutes + ":" + seconds + ":" + milliseconds + "  ");
            WriteFileByLine(Application.persistentDataPath, num, " ====== Round END ====== Success");
            WriteFileByLine(Application.persistentDataPath, num, "           ");
            //Time.timeSinceLevelLoad;
            //score = score - 1;
            //ScoreText.text = score.ToString();
            resetCube();
        }
        
        //Manipulate slider score
        progress(score);
    }

    //Check 2 seconds left
    bool timeDiff(float i)
    {
        if(Time.time - i > 2f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Action after click Drop
    void ClickButtonA()
    {
        if (timeDiff(markTime) == true)
        {
            markTime = Time.time;
            Destroy(Drop_);
            Destroy(DropCover_);
            WriteFileByLine(Application.persistentDataPath, num, " User choose: Drop; X Position: " + pos + " Y position" + height + " current round score: " + score + " System time: " + hours + ":" + minutes + ":" + seconds + ":" + milliseconds + "  ");
            keepDrop(index);
            index = index + 1;
            height = height + 1;
        }
    }

    //Action after click Restart
    void ClickButtonB()
    {
        Destroy(Drop_);
        Destroy(DropCover_);
        WriteFileByLine(Application.persistentDataPath, num, " User Choose: Restart, X Position: " + pos + " Y position" + height + " current round score: " + score + " System time: " + hours + ":" + minutes + ":" + seconds + ":" + milliseconds + "  ");
        WriteFileByLine(Application.persistentDataPath, num, " ====== Round END ====== Restart");
        WriteFileByLine(Application.persistentDataPath, num, "           ");
        resetCube(); 
    }

    //Set the start scene
    void set()
    {
        pos = 0.0f;
        original = pos;
        samplepos = pos;
        //Set holder
        holderPosition = new Vector3(pos, -4.0f, 0);
        holder = Instantiate(holder, holderPosition, Quaternion.identity);
        //Set Drop Cube
        pos = Random.Range(pos - 0.5f, pos + 0.5f);
        //Create dashline part
        dropPosition = new Vector3(samplepos, height, -5);
        Drop_ = Instantiate(Drop, dropPosition, Quaternion.identity);
        dropCoverPosition = new Vector3(samplepos, height, -7);
        DropCover_ = Instantiate(DropCover, dropCoverPosition, Quaternion.identity);
        //Set Score text position
        xPositionGotWill = 1187;
        yPositionGot = 47;
        yPositionWill = 141;
        theScoreYouWillGet.transform.position = new Vector3(xPositionGotWill, yPositionWill, -2);
        theScoreYouGot.transform.position = new Vector3(xPositionGotWill, yPositionGot, -1);
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

    void keepDrop(int i)
    {
        bottomPosition = new Vector3(pos, height, 0);
        // Adjust score position
        Debug.Log("samplepos = " + samplepos);
        Debug.Log("pos = " + pos);
        adjust = pos - samplepos;
        xPositionGotWill = xPositionGotWill + adjust * 100;
        yPositionGot = yPositionGot + 94;
        yPositionWill = yPositionGot + 94;
        theScoreYouWillGet.transform.position = new Vector3(xPositionGotWill, yPositionWill, -2);
        theScoreYouGot.transform.position = new Vector3(xPositionGotWill, yPositionGot, -1);
        //=========================
        if (i == 1)
        {
            bottom_1 = Instantiate(bottom, bottomPosition, Quaternion.identity);
        }
        if (i == 2)
        {
            bottom_2 = Instantiate(bottom, bottomPosition, Quaternion.identity);
        }
        if (i == 3)
        {
            bottom_3 = Instantiate(bottom, bottomPosition, Quaternion.identity);
        }
        if (i == 4)
        {
            bottom_4 = Instantiate(bottom, bottomPosition, Quaternion.identity);
        }
        if (i == 5)
        {
            bottom_5 = Instantiate(bottom, bottomPosition, Quaternion.identity);
        }
        if (i == 6)
        {
            bottom_6 = Instantiate(bottom, bottomPosition, Quaternion.identity);
        }
        if( i < 7)
        {
            score = score + point;
            roundScore = roundScore + point;
            ScoreText.text = score.ToString();
            samplepos = pos;
            pos = Random.Range(pos - 0.5f, pos + 0.5f);
            dropPosition = new Vector3(samplepos, height + 1f, -5);
            Drop_ = Instantiate(Drop, dropPosition, Quaternion.identity);
            dropCoverPosition = new Vector3(samplepos, height + 1f, -7);
            DropCover_ = Instantiate(DropCover, dropCoverPosition, Quaternion.identity);
        }

    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            score = score - roundScore;
            ScoreText.text = score.ToString();
            WriteFileByLine(Application.persistentDataPath, num, " User Failed, Last X Position: " + pos + " Y position" + height + " current round score: " + score + " System time: " + hours + ":" + minutes + ":" + seconds + ":" + milliseconds + "  ");
            WriteFileByLine(Application.persistentDataPath, num, " The Game End System time: " + hours + ":" + minutes + ":" + seconds + ":" + milliseconds + "  ");
            WriteFileByLine(Application.persistentDataPath, num, " ====== Round END ====== Fail");
            WriteFileByLine(Application.persistentDataPath, num, "           ");
            resetCube();
        }
    }

    
    void resetCube()
    {
        roundScore = 0;
        height = -3.0f;
        index = 1;
        Destroy(Drop_);
        Destroy(DropCover_);
        Destroy(bottom_1);
        Destroy(bottom_2);
        Destroy(bottom_3);
        Destroy(bottom_4);
        Destroy(bottom_5);
        Destroy(bottom_6);
        Destroy(holder);
        set();
    }

    void progress(int point1)
    {
        if(point1 > 100)
        {
            point1 = 99;
        }
        slider.value = point1;
    }

    int rewardEval(int currentScore, float old, float now)
    {
        if(currentScore == 1)
        {
            return 1;
        }
        else
        {
            return currentScore;
        }

    }
}
