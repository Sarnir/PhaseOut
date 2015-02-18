using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Score : MonoBehaviour {

    float score;
    float lastPhaseOutBonus;
    float lastScoreReported;
    bool updateScore = false;

    // components
    Text textComponent;
    ObstacleManager obstacleManager;

    // serializedFields
    [SerializeField]
    Text bonusText;
    [SerializeField]
    Hint hintObject;

	// Use this for initialization
	void Start () {
        textComponent = GetComponent<Text>();
        obstacleManager = GameObject.FindGameObjectWithTag("ObstacleSpawner").GetComponent<ObstacleManager>();
        ResetScoreAndStart(false);
	}

    public void ResetScoreAndStart(bool update)
    {
        updateScore = update;
        lastScoreReported = -1.0f;
        score = 0;
        lastPhaseOutBonus = 0.0f;
        textComponent.text = "0";
        bonusText.text = "";
        hintObject.Reset();
    }

    public void SetUpdateScore(bool val)
    {
        updateScore = val;
        bonusText.text = "";

        if (!val)
            hintObject.FadeOut();
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

        CheckForActions();
    }

    void CheckForActions()
    {
        // i should take all such events in some collection and iterate through them instead of doing such crappy switch style
        if (ReachedScore(0))
        {
            hintObject.Activate("Tap & hold anywhere on screen to phase out");
            obstacleManager.SetLevel0();
        }

        if (ReachedScore(15))
        {
            hintObject.Activate("Phasing in and out is the only thing you can do");
            obstacleManager.SetLevel1();
        }

        if (ReachedScore(50))
        {
            hintObject.Activate("Phasing out consumes your mana bar, phasing in restores it");
        }

        if (ReachedScore(70))
        {
            hintObject.Activate("When your mana runs out, you phase in");
            obstacleManager.SetLevel2();
        }

        if (ReachedScore(150))
        {
            hintObject.Activate("You get more points when phasing out/in close to obstacle");
            obstacleManager.SetLevel3();
        }

        if (ReachedScore(400))
        {
            hintObject.Activate("You will fail");
            obstacleManager.SetLevel4();
        }

        if (ReachedScore(500))
        {
            hintObject.Activate("Red obstacles are invisible when you are phased out");
            obstacleManager.SetLevel5();
        }

        if (ReachedScore(1000))
        {
            hintObject.Activate("Purple obstacles are invisible when you are not phased out");
            obstacleManager.SetLevel6();
        }

        if (ReachedScore(1500))
        {
            hintObject.Activate("\"I am a leaf on the wind; watch how I soar\"");
            obstacleManager.SetLevel7();
        }
    }

    bool ReachedScore(float _score)
    {
        return lastScoreReported <= _score && score >= _score;
    }
}
