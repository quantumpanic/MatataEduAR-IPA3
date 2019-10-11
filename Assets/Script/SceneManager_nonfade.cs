using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneManager_nonfade : MonoBehaviour
{

    public string NextPage;

    public void ButtonPressed()
    {
        SceneManager.LoadScene(NextPage);
    }

    public void OpenFarm()
    {
        DataHolder.Instance.nextScene = NextPage;
        SceneManager.LoadScene("Farm_AR");
    }

    public GameObject quizObject;

    public void QuizOption()
    {
        GameObject.Find("Stars").SetActive(false);
        quizObject.SetActive(true);
    }

    public void QuizOptionSelect(bool accept)
    {
        if (accept)
        {
            NextPage = "SwimmingQuiz";
        }

        ButtonPressed();
    }
}