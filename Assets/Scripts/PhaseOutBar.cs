using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class PhaseOutBar : MonoBehaviour {

    Vector2 baseScale;
    float availablePhasePercent = 1.0f;
    float phaseConsumptionRate = 0.001f;
    SpriteRenderer sprite;
    bool isConsumed;

	// Use this for initialization
	void Start () {
        baseScale = transform.localScale;
        sprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isConsumed)
        {
            availablePhasePercent -= phaseConsumptionRate;
        }
        else
        {
            availablePhasePercent += phaseConsumptionRate;
        }

        availablePhasePercent = Mathf.Clamp01(availablePhasePercent);
        transform.localScale = new Vector2(baseScale.x * availablePhasePercent, baseScale.y);
        var color = sprite.color;
        color.a = Mathf.Lerp(1.0f, 0.0f, (availablePhasePercent - 0.8f) * 5.0f); // debil i kretyn
        sprite.color = color;
	}

    public void SetConsumed(bool val)
    {
        isConsumed = val;
    }

    public bool IsDepleted()
    {
        return availablePhasePercent <= 0.0f;
    }

    public void Reset()
    {
 	    availablePhasePercent = 1.0f;
        renderer.enabled = true;
    }
}
