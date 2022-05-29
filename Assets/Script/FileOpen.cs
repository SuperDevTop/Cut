using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System.Linq;
using SFB;
using System;
using MediaToolkit.Model;
using MediaToolkit;
using FfmpegApp;
using B83.Win32;
using System.Linq;
using System.Collections.Generic;
using System.IO;

//using UnityEditor;

public class FileOpen : MonoBehaviour
{
    private string path;
    public string convertedPath;
    public VideoPlayer videoPlayer;
    public VideoPlayer videoPlayer1;
    public VideoPlayer [] videoSet;
    public GameObject rawImage0;
    public Slider startSlider0;
    public Slider endSlider0;
    public Text TextCurserEndTimeCount0;
    public Text TextCurserStartTimeCount0;
    public Text TextEditTimeCount0;
    public GameObject[] rawImage;
    public Slider[] startSlider;
    public Slider[] endSlider;
    public Text TextEndTimeCount;
    public Text TextStartTimeCount;
    public Text[] TextCurserEndTimeCount;
    public Text[] TextCurserStartTimeCount;
    public Text[] TextEditTimeCount;
    public Text logText;
    public GameObject mainContent;
    public GameObject splashContent;
    public GameObject bottonContent;
    public List<string> pathList;
    public List<int> timeList;
    public GameObject[] boxes;

    int totalVideoTime;    
    int videoEditCount;
    int videoSaveCount;
    int selectBoxNumber = 0;
    string pathss;
    int timeCount;
    string timeStr;
    bool isUpload;

    DropInfo dropInfo = null;
    class DropInfo
    {
        public string file;
        public Vector2 pos;
    }

    // Start is called before the first frame update
    void Start()
    {
        pathList = new List<string>();
        startSlider0.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        endSlider0.onValueChanged.AddListener(delegate { ValueChangeCheck(); });

        startSlider[0].onValueChanged.AddListener(delegate { ValueChangeCheck1(0); });
        endSlider[0].onValueChanged.AddListener(delegate { ValueChangeCheck1(0); });
        startSlider[1].onValueChanged.AddListener(delegate { ValueChangeCheck1(1); });
        endSlider[1].onValueChanged.AddListener(delegate { ValueChangeCheck1(1); });
        startSlider[2].onValueChanged.AddListener(delegate { ValueChangeCheck1(2); });
        endSlider[2].onValueChanged.AddListener(delegate { ValueChangeCheck1(2); });
        startSlider[3].onValueChanged.AddListener(delegate { ValueChangeCheck1(3); });
        endSlider[3].onValueChanged.AddListener(delegate { ValueChangeCheck1(3); });
        startSlider[4].onValueChanged.AddListener(delegate { ValueChangeCheck1(4); });
        endSlider[4].onValueChanged.AddListener(delegate { ValueChangeCheck1(4); });
        startSlider[5].onValueChanged.AddListener(delegate { ValueChangeCheck1(5); });
        endSlider[5].onValueChanged.AddListener(delegate { ValueChangeCheck1(5); });
        startSlider[6].onValueChanged.AddListener(delegate { ValueChangeCheck1(6); });
        endSlider[6].onValueChanged.AddListener(delegate { ValueChangeCheck1(6); });
        startSlider[7].onValueChanged.AddListener(delegate { ValueChangeCheck1(7); });
        endSlider[7].onValueChanged.AddListener(delegate { ValueChangeCheck1(7); });
        startSlider[8].onValueChanged.AddListener(delegate { ValueChangeCheck1(8); });
        endSlider[8].onValueChanged.AddListener(delegate { ValueChangeCheck1(8); });
        startSlider[9].onValueChanged.AddListener(delegate { ValueChangeCheck1(9); });
        endSlider[9].onValueChanged.AddListener(delegate { ValueChangeCheck1(9); });        
    }

    // Update is called once per frame
    void Update()
    {

        if (startSlider0.value > endSlider0.value)
        {
            startSlider0 = endSlider0;
        }

        for (int i = 0; i < 10; i++)
        {
            if (startSlider[i].value > endSlider[i].value)
            {
                startSlider[i] = endSlider[i];
            }
        }
    }

    void OnEnable()
    {
        // must be installed on the main thread to get the right thread id.
        if (splashContent.active == true)
        {
            UnityDragAndDropHook.InstallHook();
            UnityDragAndDropHook.OnDroppedFiles += OnFiles;
        }       
    }
    void OnDisable()
    {
        UnityDragAndDropHook.UninstallHook();
    }


    void OnFiles(List<string> aFiles, POINT aPos)
    {
        // do something with the dropped file names. aPos will contain the 
        // mouse position within the window where the files has been dropped.
        pathList.Add(aFiles[0]);              

        videoPlayer.url = "file://" + pathList[0];
        videoPlayer1.url = "file://" + pathList[0];
        videoPlayer.prepareCompleted += OnVideoPrepared;
        videoPlayer.Prepare();
    }

    public void ValueChangeCheck()
    {        
        SliderOperation();
    }  

    public void ValueChangeCheck1(int index)
    {          
        SliderOperation1(index);
    }

    public void SliderOperation()
    {
        if (endSlider0.value >= startSlider0.value)
        {
            TextEndTimeCount.text = TimeConvertString((int)(timeList[selectBoxNumber] * endSlider0.value));
            TextStartTimeCount.text = TimeConvertString((int)(timeList[selectBoxNumber] * startSlider0.value));

            TextCurserEndTimeCount0.text = TextEndTimeCount.text;
            TextCurserStartTimeCount0.text = TextStartTimeCount.text;
            TextEditTimeCount0.text = TimeConvertString(StringConvertTime(TextEndTimeCount.text) - StringConvertTime(TextStartTimeCount.text));
            TextCurserEndTimeCount[selectBoxNumber].text = TextEndTimeCount.text;
            TextCurserStartTimeCount[selectBoxNumber].text = TextStartTimeCount.text;
            TextEditTimeCount[selectBoxNumber].text = TimeConvertString(StringConvertTime(TextCurserEndTimeCount[selectBoxNumber].text) - StringConvertTime(TextCurserStartTimeCount[selectBoxNumber].text));
            startSlider[selectBoxNumber].value = startSlider0.value;
            endSlider[selectBoxNumber].value = endSlider0.value;
        }
        else
        {
            TextEndTimeCount.text = TimeConvertString((int)(timeList[selectBoxNumber] * endSlider0.value));
            TextStartTimeCount.text = TimeConvertString((int)(timeList[selectBoxNumber] * endSlider0.value));

            TextCurserEndTimeCount0.text = TextEndTimeCount.text;
            TextCurserStartTimeCount0.text = TextStartTimeCount.text;
            TextEditTimeCount0.text = TimeConvertString(StringConvertTime(TextEndTimeCount.text) - StringConvertTime(TextStartTimeCount.text));
            TextCurserEndTimeCount[selectBoxNumber].text = TextEndTimeCount.text;
            TextCurserStartTimeCount[selectBoxNumber].text = TextStartTimeCount.text;
            TextEditTimeCount[selectBoxNumber].text = TimeConvertString(StringConvertTime(TextCurserEndTimeCount[selectBoxNumber].text) - StringConvertTime(TextCurserStartTimeCount[selectBoxNumber].text));
            startSlider0.value = endSlider0.value;
            startSlider[selectBoxNumber].value = endSlider0.value;
            endSlider[selectBoxNumber].value = endSlider0.value;
        }
    }

    public void SliderOperation1(int index)
    {
        //print(index);
        //print(videoEditCount);

        if (index <= videoEditCount)
        {
            //print(1231231);
            if (endSlider[index].value >= startSlider[index].value)
            {
                TextCurserEndTimeCount[index].text = TimeConvertString((int)(timeList[index] * endSlider[index].value));
                print(timeList[index]);
                TextCurserStartTimeCount[index].text = TimeConvertString((int)(timeList[index] * startSlider[index].value));
                TextEditTimeCount[index].text = TimeConvertString(StringConvertTime(TextCurserEndTimeCount[index].text) - StringConvertTime(TextCurserStartTimeCount[index].text));

                if (selectBoxNumber == index)
                {
                    TextCurserEndTimeCount0.text = TextCurserEndTimeCount[index].text;
                    TextCurserStartTimeCount0.text = TextCurserStartTimeCount[index].text;
                    TextEditTimeCount0.text = TimeConvertString(StringConvertTime(TextCurserEndTimeCount[index].text) - StringConvertTime(TextCurserStartTimeCount[index].text));
                    startSlider0.value = startSlider[index].value;
                    endSlider0.value = endSlider[index].value;
                }
                
            }
            else
            {
                //print(32423423);
                TextCurserEndTimeCount[index].text = TimeConvertString((int)(timeList[index] * endSlider[index].value));
                TextCurserStartTimeCount[index].text = TimeConvertString((int)(timeList[index] * startSlider[index].value));
                TextEditTimeCount[index].text = TimeConvertString(StringConvertTime(TextCurserEndTimeCount[index].text) - StringConvertTime(TextCurserStartTimeCount[index].text));
                TextCurserEndTimeCount0.text = TextCurserEndTimeCount[index].text;

                if (selectBoxNumber == index)
                {
                    TextCurserStartTimeCount0.text = TextCurserStartTimeCount[index].text;
                    TextEditTimeCount0.text = TimeConvertString(StringConvertTime(TextCurserEndTimeCount[index].text) - StringConvertTime(TextCurserStartTimeCount[index].text));
                    startSlider[index].value = endSlider[index].value;
                    startSlider0.value = endSlider[index].value;
                    endSlider0.value = endSlider[index].value;
                }
                
            }
        }       
    }

    public void OpenExplorer()
    {
        var paths = StandaloneFileBrowser.OpenFilePanel("Open File", "", "", false);                       
        pathList.Add(paths[0]);

        if (pathss != "")
        {
            videoPlayer.url = "file://" + pathList[0];
            videoPlayer1.url = "file://" + pathList[0];
            videoPlayer.prepareCompleted += OnVideoPrepared;
            videoPlayer.Prepare();
        }
        
    }

    public void OpenExplorer1()
    {        
        var paths = StandaloneFileBrowser.OpenFilePanel("Open File", "", "", false);
        pathList.Add(paths[0]);

        if (paths[0] != "")
        {
            videoEditCount++;
            isUpload = true;
            boxes[videoEditCount].SetActive(true);
            videoSet[videoEditCount].url = "file://" + pathList[videoEditCount];
            videoSet[videoEditCount].prepareCompleted += OnVideoPrepared;
            videoSet[videoEditCount].Prepare();
        }
               
    }

    private void OnVideoPrepared(VideoPlayer videoPlayer)
    {          
        if (isUpload == true)
        {
            for (int j = 0; j < rawImage[videoEditCount].GetComponentsInChildren<RawImage>().Length; j++)
            {
                int f = ((int)videoPlayer.frameCount * j) / 15;
                videoPlayer.frame = f;
                rawImage[videoEditCount].GetComponentsInChildren<RawImage>()[j].texture = videoPlayer.texture;
            }

            timeList.Add((int)videoPlayer.length);            
            InitialSet1((int)videoPlayer.length, videoEditCount);
        }
        else
        {
            //for (int j = 0; j < rawImage0.GetComponentsInChildren<RawImage>().Length; j++)
            //{
            //    int f = ((int)videoPlayer.frameCount * j) / 15;
            //    videoPlayer.frame = f;
            //    rawImage0.GetComponentsInChildren<RawImage>()[j].texture = videoPlayer.texture;
            //}

            for (int j = 0; j < rawImage[0].GetComponentsInChildren<RawImage>().Length; j++)
            {
                int f = ((int)videoPlayer.frameCount * j) / 15;
                videoPlayer.frame = f;
                rawImage[0].GetComponentsInChildren<RawImage>()[j].texture = videoPlayer.texture;
            }

            //Debug.Log("Video done prepared.");
            //print((int)videoPlayer.length);
            totalVideoTime = (int)videoPlayer.length;
            timeList.Add((int)videoPlayer.length);
            InitialSet(totalVideoTime);
            
            splashContent.GetComponent<Animator>().enabled = true;
            mainContent.SetActive(true);
            boxes[0].SetActive(true);
            InitialSet1((int)videoPlayer.length, 0);
        }
        
    }

    private void OnVideoPrepared1(VideoPlayer videoPlayer)
    {
        for (int j = 0; j < rawImage0.GetComponentsInChildren<RawImage>().Length; j++)
        {
            int f = ((int)videoPlayer.frameCount * j) / 15;
            videoPlayer.frame = f;
            rawImage0.GetComponentsInChildren<RawImage>()[j].texture = videoPlayer.texture;
        }

        totalVideoTime = (int)videoPlayer.length;
        InitialSet(totalVideoTime);
    }

    //public void BoxButton0Click()
    //{
    //    videoPlayer1.url = "file://" + pathss;
    //    videoPlayer1.Prepare();
    //    videoPlayer1.Play();
    //}
    public void BoxButton1Click()
    {
        selectBoxNumber = 0;
        videoPlayer1.url = "file://" + pathList[0];
        videoPlayer1.Prepare();
        videoPlayer1.prepareCompleted += OnVideoPrepared1;
        videoPlayer1.Play();
    }
   
    public void BoxButton2Click()
    {
        selectBoxNumber = 1;
        videoPlayer1.url = "file://" + pathList[1];
        videoPlayer1.Prepare();
        videoPlayer1.prepareCompleted += OnVideoPrepared1;
        videoPlayer1.Play();
    }

    public void BoxButton3Click()
    {
        selectBoxNumber = 2;
        videoPlayer1.url = "file://" + pathList[2];
        videoPlayer1.Prepare();
        videoPlayer1.prepareCompleted += OnVideoPrepared1;
        videoPlayer1.Play();
    }
    public void BoxButton4Click()
    {
        selectBoxNumber = 3;
        videoPlayer1.url = "file://" + pathList[3];
        videoPlayer1.Prepare();
        videoPlayer1.prepareCompleted += OnVideoPrepared1;
        videoPlayer1.Play();
    }

    public void BoxButton5Click()
    {
        selectBoxNumber = 4;
        videoPlayer1.url = "file://" + pathList[4];
        videoPlayer1.Prepare();
        videoPlayer1.prepareCompleted += OnVideoPrepared1;
        videoPlayer1.Play();
    }
    public void BoxButton6Click()
    {
        selectBoxNumber = 5;
        videoPlayer1.url = "file://" + pathList[5];
        videoPlayer1.Prepare();
        videoPlayer1.prepareCompleted += OnVideoPrepared1;
        videoPlayer1.Play();
    }

    public void BoxButton7Click()
    {
        selectBoxNumber = 6;
        videoPlayer1.url = "file://" + pathList[6];
        videoPlayer1.Prepare();
        videoPlayer1.prepareCompleted += OnVideoPrepared1;
        videoPlayer1.Play();
    }
    public void BoxButton8Click()
    {
        selectBoxNumber = 7;
        videoPlayer1.url = "file://" + pathList[7];
        videoPlayer1.Prepare();
        videoPlayer1.prepareCompleted += OnVideoPrepared1;
        videoPlayer1.Play();
    }

    public void BoxButton9Click()
    {
        selectBoxNumber = 8;
        videoPlayer1.url = "file://" + pathList[8];
        videoPlayer1.Prepare();
        videoPlayer1.prepareCompleted += OnVideoPrepared1;
        videoPlayer1.Play();
    }
    public void BoxButton10Click()
    {
        selectBoxNumber = 9;
        videoPlayer.url = "file://" + pathList[9];
        videoPlayer.Prepare();
        videoPlayer1.prepareCompleted += OnVideoPrepared1;
        videoPlayer.Play();
    }
    public void PlayButton()
    {
        videoPlayer.url = "file://" + pathss;
        videoPlayer.Prepare();
        videoPlayer.Play();

    }

  

    public void FinalizeClick()
    {
        string outputPath = StandaloneFileBrowser.SaveFilePanel("Save File", "", "", "");
        string outputFileName = $@"" + outputPath;
        //print(outputPath);
        int start = StringConvertTime(TextStartTimeCount.text);
        int end = StringConvertTime(TextEditTimeCount0.text);
       
        string command = $"-i \"{pathss}\" -ss {start} -t {end} \"{outputPath}\"";

        FfmpegHandler.ExecuteFFMpeg(command);               
    }

    public void FinalizeClick1()
    {
        //videoSaveCount++;
        string outputPath = StandaloneFileBrowser.SaveFilePanel("Save File", "", "", "");
        string outputFileName = $@"" + outputPath;

        int start0 = StringConvertTime(TextStartTimeCount.text);
        int end0 = StringConvertTime(TextEditTimeCount0.text);

        string command0 = $" -i \"{pathList[0]}\" -ss {start0} -t {end0} -vf scale=1920:1000 \"{$@"" + "1.mp4"}\"" + " & ";

        //FfmpegHandler.ExecuteFFMpeg(command0);
        string command1 = "";
        if (videoEditCount >= 1)
        {
            for (int i = 1; i < videoEditCount + 1; i++)
            {
                int start = StringConvertTime(TextCurserStartTimeCount[i].text);
                int end = StringConvertTime(TextEditTimeCount[i].text);

                command1 += $"ffmpeg -i \"{pathList[i]}\" -ss {start} -t {end} -vf scale=1920:1000 \"{$@"" + (i + 1) + ".mp4"}\"" + " & ";

                //FfmpegHandler.ExecuteFFMpeg(command);
            }
        }

        string concatCommand = "concat:";

        for (int i = 0; i < videoEditCount + 1; i++)
        {
            if (i == videoEditCount)
            {
                concatCommand += "" + (i + 1) + ".mp4";
            }
            else
            {
                concatCommand += "" + (i + 1) + ".mp4|";
            }
            
        }

        string fn = "";


        using (var stw = new StreamWriter("videos.txt"))
            for(int i = 0; i < videoEditCount + 1; i++)
            {               
                stw.WriteLine($"file '{(i + 1)}.mp4'");                
            }

        string command2 = $"ffmpeg -f concat -safe 0 -i videos.txt -c copy {outputFileName}";
        string command = command0 + command1 + command2 + "";
        //print(command);    
        FfmpegHandler.ExecuteFFMpeg(command);
    }

    public void StartUpClick()
    {
        if (StringConvertTime(TextStartTimeCount.text) == timeList[selectBoxNumber])
        {   
        }        
        else
        {
            TextStartTimeCount.text = TimeConvertString(StringConvertTime(TextStartTimeCount.text) + 1);
            startSlider0.value = (float)StringConvertTime(TextStartTimeCount.text) / (float)timeList[selectBoxNumber];
            TextEditTimeCount0.text = TimeConvertString(StringConvertTime(TextEndTimeCount.text) - StringConvertTime(TextStartTimeCount.text));

            TextCurserStartTimeCount0.text = TextStartTimeCount.text;
            TextCurserEndTimeCount[selectBoxNumber].text = TextEndTimeCount.text;
            TextCurserStartTimeCount[selectBoxNumber].text = TextStartTimeCount.text;
            startSlider[selectBoxNumber].value = startSlider0.value;
            TextEditTimeCount[selectBoxNumber].text = TimeConvertString(StringConvertTime(TextCurserEndTimeCount[selectBoxNumber].text) - StringConvertTime(TextCurserStartTimeCount[selectBoxNumber].text));
        }        
    }

    public void StartDownClick()
    {
        
        if (StringConvertTime(TextStartTimeCount.text) == 0)
        {            
        }
        else
        {
            TextStartTimeCount.text = TimeConvertString(StringConvertTime(TextStartTimeCount.text) - 1);
            startSlider0.value = (float)StringConvertTime(TextStartTimeCount.text) / (float)timeList[selectBoxNumber];
            TextEditTimeCount0.text = TimeConvertString(StringConvertTime(TextEndTimeCount.text) - StringConvertTime(TextStartTimeCount.text));
            TextCurserStartTimeCount0.text = TextStartTimeCount.text;
            TextCurserEndTimeCount[selectBoxNumber].text = TextEndTimeCount.text;
            TextCurserStartTimeCount[selectBoxNumber].text = TextStartTimeCount.text;
            startSlider[selectBoxNumber].value = startSlider0.value;
            TextEditTimeCount[selectBoxNumber].text = TimeConvertString(StringConvertTime(TextCurserEndTimeCount[selectBoxNumber].text) - StringConvertTime(TextCurserStartTimeCount[selectBoxNumber].text));
        }
    }

    public void EndUpClick()
    {
        if (StringConvertTime(TextEndTimeCount.text) == timeList[selectBoxNumber])
        {                                
        }       
        else
        {
            TextEndTimeCount.text = TimeConvertString(StringConvertTime(TextEndTimeCount.text) + 1);
            endSlider0.value = (float)StringConvertTime(TextEndTimeCount.text) / (float)timeList[selectBoxNumber];
            TextEditTimeCount0.text = TimeConvertString(StringConvertTime(TextEndTimeCount.text) - StringConvertTime(TextStartTimeCount.text));
            TextCurserEndTimeCount0.text = TextEndTimeCount.text;
            TextCurserEndTimeCount[selectBoxNumber].text = TextEndTimeCount.text;
            TextCurserStartTimeCount[selectBoxNumber].text = TextStartTimeCount.text;
            endSlider[selectBoxNumber].value = endSlider0.value;
            TextEditTimeCount[selectBoxNumber].text = TimeConvertString(StringConvertTime(TextCurserEndTimeCount[selectBoxNumber].text) - StringConvertTime(TextCurserStartTimeCount[selectBoxNumber].text));
        }
    }

    public void EndDownClick()
    {        
        if (StringConvertTime(TextEndTimeCount.text) == 0)
        {            
        }
        else
        {
            TextEndTimeCount.text = TimeConvertString(StringConvertTime(TextEndTimeCount.text) - 1);
            endSlider0.value = (float)StringConvertTime(TextEndTimeCount.text) / (float)timeList[selectBoxNumber];
            TextEditTimeCount0.text = TimeConvertString(StringConvertTime(TextEndTimeCount.text) - StringConvertTime(TextStartTimeCount.text));
            TextCurserEndTimeCount0.text = TextEndTimeCount.text;
            TextCurserEndTimeCount[selectBoxNumber].text = TextEndTimeCount.text;
            TextCurserStartTimeCount[selectBoxNumber].text = TextStartTimeCount.text;
            endSlider[selectBoxNumber].value = endSlider0.value;
            TextEditTimeCount[selectBoxNumber].text = TimeConvertString(StringConvertTime(TextCurserEndTimeCount[selectBoxNumber].text) - StringConvertTime(TextCurserStartTimeCount[selectBoxNumber].text));
        }
    }

    public void InitialSet(int totalVideoTime)
    {
        endSlider0.value = 1;
        TextCurserEndTimeCount0.text = TimeConvertString((int)(totalVideoTime));
    }

    public void InitialSet1(int totalVideoTime, int index)
    {
        endSlider[index].value = 1;
        TextCurserEndTimeCount[index].text = TimeConvertString((int)(timeList[index]));
    }

    public string TimeConvertString(int time)
    {
        int hours = time / 3600;
        int mins = (time - 3600 * hours) / 60;
        int secs = time - 3600 * hours - 60 * mins;
        //print(hours);
        //print(mins);
        //print(secs);
        timeStr = "";

        if (hours < 10)
        {
            timeStr = "0" + hours + ":";
        }
        else
        {
            timeStr = "" + hours + ":";
        }

        if (mins < 10)
        {
            timeStr += "0" + mins + ":";
        }
        else
        {
            timeStr += "" + mins + ":";
        }

        if (secs < 10)
        {
            timeStr += "0" + secs;
        }
        else
        {
            timeStr += "" + secs;
        }

        return timeStr; 
    }

    public int StringConvertTime(string text)
    {
        char[] spearator = { ':' };
        string[] strlist = text.Split(spearator);

        timeCount = System.Int32.Parse(strlist[0]) * 3600 + System.Int32.Parse(strlist[1]) * 60 + System.Int32.Parse(strlist[2]);
        //print(timeCount);     

        return timeCount;   
    }    
}
