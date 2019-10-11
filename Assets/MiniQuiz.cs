using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniQuiz : GameManagerClass
{

    // Use this for initialization
    void Start()
    {
        // read the txt file for a list of questions
        ReadDataFile();

        Transform holder = GameObject.Find("Objectives").transform;

        Instantiate(objectivePrefab, holder).GetComponent<Objective>().SetName("OneStar").SetActive().AddToList(objectives);

        StartGame();
        GetRandomQuestion();
    }

    bool isPause;

    // Update is called once per frame
    void Update()
    {
        if (isPlaying && !isPause)
        {
            // update timer
            timeElapsed += Time.deltaTime;
            float remainingTime = finishTime - timeElapsed;

            if (remainingTime <= 0) GetRandomQuestion();
            else
            {
                int minutes = Mathf.FloorToInt(remainingTime / 60F);
                int seconds = Mathf.FloorToInt(remainingTime - minutes * 60);
                string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);

                timeText.text = niceTime;
            }
        }
    }

    public Text timeText;
    float timeLimit = 10;
    float finishTime;

    void ResetTimer()
    {
        finishTime = timeElapsed + timeLimit;
        isPause = false;
    }

    // screen all goals and increment if condition met
    public void CheckAllObjectives()
    {
        foreach (Objective obj in objectives)
        {
            obj.CheckThenDo(CheckGoal, Increment);
        }

        CalculateScore();
        ReportScore();
    }

    bool CheckGoal(Objective obj)
    {
        if (obj.name == "OneStar")
        {
            if (correctAnswers >= 3)
            {
                return true;
            }
        }

        return false;
    }

    void Increment(Objective obj)
    {
        if (obj.name == "KnowStar")
        {
            obj.CheckProgress();
        }

        else
        {
            obj.Increment();
            obj.CheckProgress();
        }
    }

    // The Game
    public TextAsset textFile;
    public int totalQuestions;
    public List<int> pickedQuestions = new List<int>();

    void ReadDataFile()
    {
        string[] buffer = textFile.text.Split('\n');
        totalQuestions = buffer.Length;
        queries = new string[totalQuestions][];

        int listCount = 0;
        int itemCount = 0;

        foreach (string item in buffer)
        {
            queries[listCount] = new string[6]; // 1 question, 4 options, 1 answer
            listCount++;
        }

        listCount = 0;

        foreach (string s in buffer)
        {
            string[] newTemp = s.Split(',');
            foreach (string item in newTemp)
            {
                queries[listCount][itemCount] = item;
                itemCount++;
            }
            itemCount = 0;
            listCount++;
        }
    }

    // semua text untuk pertanyaan dan jawaban
    string[][] queries;

    string questionText;
    string optionTextA;
    string optionTextB;
    string optionTextC;
    string optionTextD;
    int correctAnswer;

    public Text questionLbl;
    public Text optionLblA;
    public Text optionLblB;
    public Text optionLblC;
    public Text optionLblD;

    List<int> usedQuestions = new List<int>();

    void GetNewQuestion(int num)
    {
        questionText = queries[num][0];
        optionTextA = queries[num][1];
        optionTextB = queries[num][2];
        optionTextC = queries[num][3];
        optionTextD = queries[num][4];

        int ans = -1;
        int.TryParse(queries[num][5], out ans);

        correctAnswer = ans - 1; // -1 means no correct answer

        UpdateQuestionText();
        ResetTimer();
        hasAnswered = false;
    }

    void GetRandomQuestion()
    {
        if (pickedQuestions.Count >= totalQuestions)
        {
            QuestionsExhausted();
            return;
        }

        int ran = Random.Range(0, totalQuestions);
        while (pickedQuestions.Contains(ran))
        {
            ran = Random.Range(0, totalQuestions);
        }

        pickedQuestions.Add(ran);
        GetNewQuestion(ran);
    }

    void UpdateQuestionText()
    {
        ResetTextColors();

        questionLbl.text = questionText;
        optionLblA.text = "A. " + optionTextA;
        optionLblB.text = "B. " + optionTextB;
        optionLblC.text = "C. " + optionTextC;
        optionLblD.text = "D. " + optionTextD;
    }

    void ResetTextColors()
    {
        BlockButtons(false);

        Color col = new Color(50f / 255f, 50f / 255f, 50f / 255f);

        optionLblA.color = col;
        optionLblB.color = col;
        optionLblC.color = col;
        optionLblD.color = col;
    }

    public Image block;

    void BlockButtons(bool show)
    {
        block.raycastTarget = show;
    }

    void CorrectAnswer()
    {
        ShowAnswer();
        GetComponent<AudioSource>().Play();
        correctAnswers++;
        Invoke("GetRandomQuestion", 2);
    }

    void WrongAnswer()
    {
        ShowAnswer();
        Invoke("GetRandomQuestion", 2);
    }

    public int correctAnswers;
    bool hasAnswered;

    public void GiveAnswer(int ans)
    {
        if (hasAnswered) return;
        hasAnswered = true;

        if (ans == correctAnswer)
            CorrectAnswer();
        else
            WrongAnswer();
    }

    void ShowAnswer()
    {
        isPause = true;
        BlockButtons(true);
        optionLblA.color = Color.red;
        optionLblB.color = Color.red;
        optionLblC.color = Color.red;
        optionLblD.color = Color.red;

        switch (correctAnswer)
        {
            case 0:
                optionLblA.color = Color.green;
                break;
            case 1:
                optionLblB.color = Color.green;
                break;
            case 2:
                optionLblC.color = Color.green;
                break;
            case 3:
                optionLblD.color = Color.green;
                break;
        }
    }

    void QuestionsExhausted()
    {
        GameOver();
        questionLbl.text = "Well Done!";
        optionLblA.text = "";
        optionLblB.text = "";
        optionLblC.text = "";
        optionLblD.text = "";

        CheckAllObjectives();
    }

    public void CalculateScore()
    {
        // pass
    }

    public Animator anim;

    void SetAnimStars(int stars)
    {
        anim.SetInteger(0, stars);
    }

    void PlayStarsAnime()
    {
        anim.Play("Star0");
    }

    public override void ReportScore()
    {
        base.ReportScore();
        GameOver();

        bool getBonus = false;
        int gotStars = 0;
        bool knowledge = false;

        if (GetObjective("OneStar").isDone) getBonus = true;

        anim.transform.parent.gameObject.SetActive(true);

        // add more points
        gotStars = DataHolder.Instance.starsHolder;
        if (gotStars >= 3 && getBonus) knowledge = true;
        else if (getBonus) gotStars++;

        // add accumulated stars
        if (knowledge == true)
        {
            DataHolder.Instance.totalKnowStars++;
        }
        else
        {
            GameObject.Find("ButtonAR").SetActive(false);
        }

        anim.SetInteger("Stars", gotStars);
        anim.SetBool("Knowledge", knowledge);

        PlayStarsAnime();

        // play music
        GameObject.Find("MusicManager").GetComponent<MusicManager>().PlayMusic("quizshow_start_nointro");
    }
}
