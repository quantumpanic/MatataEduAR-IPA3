using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    public bool isActive;
    public bool isDone = false;
    public float progress = 0;
    public float goal = 1;
    public float incrementValue = 1;

    public Objective SetActive(bool active = true)
    {
        isActive = active;
        return this;
    }

    public Objective SetName(string val)
    {
        name = val;
        return this;
    }

    public Objective SetIncrement(float val)
    {
        incrementValue = val;
        return this;
    }

    public Objective SetProgress(float val)
    {
        progress = val;
        return this;
    }

    public Objective SetGoal(float val)
    {
        goal = val;
        return this;
    }

    public void Increment(float val = 0)
    {
        if (isActive && !isDone)
        {
            progress += (val == 0) ? incrementValue : val;
        }
    }

    public void CheckProgress()
    {
        if (progress >= goal)
        {
            ReachGoal();
        }
    }

    void ReachGoal()
    {
        isDone = true;
    }

    public void AddToList(List<Objective> list)
    {
        list.Add(this);
    }

    public void CheckThenDo(System.Func<Objective, bool> condition, System.Action<Objective> toDo)
    {
        if (condition(this)) toDo(this);
    }
}
