using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Hint : MonoBehaviour {

    Vector3 speed;
    Vector2 startPos;
    bool isActive;
    bool seen;

    string nextCaption;

    Text textComponent;

	// Use this for initialization
	void Start ()
    {
        textComponent = GetComponent<Text>();
        nextCaption = "";
        seen = false;
        speed = new Vector3(-1.0f, 0.0f, 0.0f);
        isActive = false;
        startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isActive)
            transform.position += speed;

        if (renderer.isVisible)
            seen = true;

        if (seen && !renderer.isVisible)
        {
            isActive = false;
        }
	}

    public void Activate(string caption)
    {
        if (isActive)
        {
            nextCaption = caption;
        }
        else
        {
            textComponent.text = caption;
            seen = false;
            isActive = true;
            transform.position = startPos;
        }
    }
}
