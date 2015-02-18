using UnityEngine;
using System.Collections;

public class AnimateColor : MonoBehaviour {

    [SerializeField]
    Gradient gradient;

    [SerializeField]
    float duration;

    SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        var percent = Mathf.Repeat(Time.time, duration) / duration;
        spriteRenderer.color = gradient.Evaluate(percent);
	}
}
