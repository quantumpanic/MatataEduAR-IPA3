using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class SceneManager_fadeFX : MonoBehaviour
{

    public Image SplashImage;
    public string NextPage;

    public void OnButtonClick()
    {
        SplashImage.enabled = true;
        StartCoroutine(FadeImage(true));

    }
    IEnumerator FadeImage(bool fadeaway)
    {

        SplashImage.canvasRenderer.SetAlpha(0.0f);

        FadeIn();
        yield return new WaitForSeconds(0f);
        //SceneManager.LoadScene(NextPage);

        DataHolder.Instance.nextScene = NextPage;
        SceneManager.LoadScene(NextPage);
        //SceneManager.LoadSceneAsync("SceneTransit", LoadSceneMode.Additive);
    }

    void FadeIn()
    {
        SplashImage.CrossFadeAlpha(1.0f, 1.5f, false);
    }

    public void OpenFarmFromMenu()
    {
        DataHolder.Instance.nextScene = "mainmenu";
        SceneManager.LoadScene("PerpusMenu");
    }
}