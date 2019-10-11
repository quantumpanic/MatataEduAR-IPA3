using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class TextTypeAnim : MonoBehaviour {
	public float letterPause = 0.2f;

	string message;
    string messageFront;
    string messageBack;
	Text textComp;

	// Use this for initialization
	void Start () {
		textComp = GetComponent<Text>();
        btn = GameObject.Find("Button").GetComponent<Button>();
		message = textComp.text;
		textComp.text = "";
		StartCoroutine(TypeText ());

        hasStart = true;
	}

    bool hasStart;

    void Restart()
    {
        if (hasStart)
        {
            messageFront = "";
            messageBack = "";
            message = textComp.text;
            StartCoroutine(TypeText());
        }
    }

    string transparentTextHex = "<color=#00000000>";
    string endTransparentTextHex = "</color>";

    IEnumerator TypeText ()
    {
        messageBack = message;

        foreach (char letter in message.ToCharArray())
        {
            messageFront += messageBack.Substring(0, 1);
            messageBack = messageBack.Substring(1);
            
            textComp.text = messageFront + transparentTextHex + messageBack + endTransparentTextHex;
            //yield return 0;
            yield return new WaitForSeconds(letterPause);
        }

        textComp.text = message;
        FadeInButton();
    }

    void ResetText()
    {
        StopAllCoroutines();
        textComp.text = "";
        message = "";
        messageFront = "";
        messageBack = "";
    }

    public int textIndex;

    public void NextText()
    {
        int numTexts = transform.childCount;

        if (textIndex > numTexts - 1) FinishedTexts();
        else
        {
            ResetText();
            ShowText(textIndex);
            Restart();
        }

        textIndex++;

        DisableButton();
    }

    Button btn;

    void DisableButton()
    {
        btn.image.CrossFadeAlpha(0, 0, false);
        btn.interactable = false;
    }

    void FadeInButton()
    {
        btn.image.CrossFadeAlpha(1, 1, false);
        btn.interactable = true;
    }

    public void ShowText(int index)
    {
        Text nextText = transform.GetChild(index).GetComponent<Text>();
        textComp.text = nextText.text;
    }

    void FinishedTexts()
    {
        GameObject.Find("SceneManager").GetComponent<SceneManager_fadeFX>().OnButtonClick();
    }
}
