using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamBuilderGame : GameManagerClass {

	// Use this for initialization
	new void Start () {
        base.Start();

        ClearPuzzleGrid();
	}
	
	// Update is called once per frame
	void Update () {
        bool tapped = false;
        if (Input.GetMouseButtonUp(0)) tapped = true;

        if (tapped)
        {
            // if sack rect is inside droprect
            if (RectTransformUtility.RectangleContainsScreenPoint(dropRect, sandSackObj.position))
            {
                DropSack();
            }

            // put the sandsack back
            ReturnSandSack();
        }
    }

    // The Game
    public WaterfallSegment activeSegment;
    public RectTransform sandSackObj;
    public RectTransform dropRect;
    public RectTransform buildRect;
    public GameObject damPanel;

    void DropSack()
    {
        activeSegment.DropSack();
        UpdateSacks();

        // add score
        GlobalGameManager.Instance.ScoreAdd(100);
    }

    void ReturnSandSack()
    {
        sandSackObj.anchoredPosition = Vector2.zero;
    }

    void UpdateSacks()
    {
        // if sacks at maximum, begin puzzle game and start timer
        if (activeSegment.hasMaxSacks)
        {
            ToggleSack(false);
            Invoke("EnablePuzzle", 0.1F); // 0.5 secs for sack drop animation
        }
    }

    void EnableSack()
    {
        ToggleSack(true);
    }

    void ToggleSack(bool enable)
    {
        // enable or disable the sack
        sandSackObj.transform.parent.GetComponent<ScrollRect>().horizontal = enable;
        sandSackObj.transform.parent.GetComponent<ScrollRect>().vertical = enable;

        if (enable) sandSackObj.GetComponent<Animator>().Play("PairSpawn");
        else sandSackObj.GetComponent<Animator>().Play("PairCorrect");
    }

    void EnablePuzzle()
    {
        // enable the puzzle panel
        TogglePuzzle(true);
    }

    void TogglePuzzle(bool enable)
    {
        if (enable) PopulatePuzzleGrid();
        else ClearPuzzleGrid();
    }

    public Sprite[] puzzleObjects;

    void PopulatePuzzleGrid()
    {
        // add objects in each puzzle box
        foreach (PuzzleButton rt in damPanel.GetComponentsInChildren<PuzzleButton>())
        {
            var item = rt.transform.GetChild(0).GetComponent<Image>();
            item.sprite = puzzleObjects[Random.Range(0, puzzleObjects.Length)];
            rt.SendMessage("ResetButton");
        }
    }

    void ClearPuzzleGrid()
    {
        // empty all puzzle boxes
        foreach (PuzzleButton rt in damPanel.GetComponentsInChildren<PuzzleButton>())
        {
            rt.SendMessage("Hide");
        }
    }

    public void PuzzleButton(Button button)
    {
        // get the sprite
        var script = button.GetComponent<PuzzleButton>();
                
        if (script.isDebris)
        {
            // it's debris, do nothing and try to break the debris
            button.SendMessage("BreakDebris");
        }
        else
        {
            // build the dam with the puzzle piece
            BuildDam(button);
        }

        if (!activeSegment.damFinished) CheckPuzzleEmpty();
        else
        {
            TogglePuzzle(false);
            Invoke("FinishedSegment", 1F);
        }
    }

    void BuildDam(Button button)
    {
        activeSegment.BuildDam(button);
    }

    void SackTimerUp()
    {
        // puzzle timer up, destroy the sacks and puzzle
        ClearPuzzleGrid();
        Invoke("EnableSack", 2F);
    }

    void CheckPuzzleEmpty()
    {
        // if all puzzles are used, make a new batch
        foreach (Button btn in damPanel.GetComponentsInChildren<Button>())
        {
            if (btn.interactable) return;
        }

        PopulatePuzzleGrid();
    }

    void FinishedSegment()
    {
        //activeSegment.DestroyDam();
        activeSegment.DestroySacks();
        Invoke("FadeInWaterfall", 2F);
        Invoke("MoveToNextSegment", 4F);

        // add score
        GlobalGameManager.Instance.ScoreAdd(1500);
    }

    void FadeInWaterfall()
    {
        activeSegment.FadeWaterToStage(5);
    }

    void MoveToNextSegment()
    {
        // total of 3 phases
        phase++;
        if (phase >= 3)
        {
            EndGame();
            return;
        }                

        // move the camera 15 spaces up
        var newPos = Camera.main.transform.position + (Vector3.up * 15);

        StartCoroutine(DoMove(newPos));
    }

    IEnumerator DoMove(Vector3 newPos)
    {
        while (Camera.main.transform.position.y < newPos.y)
        {
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, newPos, Time.deltaTime * 10);
            yield return null;
        }

        BeginNextSegment();
    }

    public GameObject[] allSegments;
    int phase = 0;

    void BeginNextSegment()
    {
        activeSegment = allSegments[phase].GetComponent<WaterfallSegment>();
        Invoke("EnableSack", 0.5F);
    }

    void EndGame()
    {
        // notify game manager that you finished this game
        StartCoroutine(Finish(0.5F));
    }

    IEnumerator Finish(float delay)
    {
        yield return new WaitForSeconds(delay);
        Victory();
    }
}
