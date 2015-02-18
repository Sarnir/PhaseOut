using UnityEngine;
using System.Collections;

public class Action
{
   /* 
    * Garbage :\ Reinventing the wheel, actually
    * 
    * float conditionScore;
    private delegate void eventmethod();
    private eventmethod actionDelegate;

    private Action(float _conditionScore, eventmethod _actionDelegate)
    {
        conditionScore = _conditionScore;
        actionDelegate = _actionDelegate;
    }

    public static Action CreateAction(float conditionScore, eventmethod actionDelegate)
    {
        return new Action(conditionScore, actionDelegate);
    }

    public void CheckForAction(float score)
    {
        if (score > conditionScore)
        {
            actionDelegate();
        }
    }*/
}
