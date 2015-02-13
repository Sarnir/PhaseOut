using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Score : MonoBehaviour {

    float score;
    float lastPhaseOutBonus;
    float lastScoreReported;
    bool updateScore = false;
    Text textComponent;

    [SerializeField()]
    Text bonusText;

	// Use this for initialization
	void Start () {
        textComponent = GetComponent<Text>();
        ResetScoreAndStart(false);
	}

    public void ResetScoreAndStart(bool update)
    {
        updateScore = update;
        lastScoreReported = 0.0f;
        score = 0;
        lastPhaseOutBonus = 0.0f;
        textComponent.text = "0";
        bonusText.text = "";
    }

    public void SetUpdateScore(bool val)
    {
        updateScore = val;
        bonusText.text = "";
    }

    public void AddPhaseOutBonus(float phaseOutDeltaTime)
    {
        lastPhaseOutBonus = (0.1f / phaseOutDeltaTime);
        print("+" + lastPhaseOutBonus);
    }

    public void AddPhaseInBonus(float phaseInDeltaTime)
    {
        float phaseInBonus = (0.1f / phaseInDeltaTime);
        print("+" + phaseInBonus);

        int bonusPoints = (int)(lastPhaseOutBonus + phaseInBonus);
        if (bonusPoints > 0)
        {
            UpdateScore(bonusPoints);
            bonusText.text = "+" + bonusPoints.ToString();
        }
        else
        {
            bonusText.text = "";
        }
        lastPhaseOutBonus = 0.0f;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (updateScore)
        {
            UpdateScore(Time.deltaTime);
            textComponent.text = ((int)score).ToString();
        }
    }

    void UpdateScore(float delta)
    {
        lastScoreReported = score;
        score += delta;
    }
}
