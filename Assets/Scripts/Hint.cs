using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Hint : MonoBehaviour {

    bool isActive;
    bool isFadedIn;
    string nextCaption;
    const float fadeTime = 1.0f;
    const float minVisibleTime = 0.5f;

    Text textComponent;

	// Use this for initialization
	void Start ()
    {
        textComponent = GetComponent<Text>();
        Reset();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isActive && isFadedIn && nextCaption != "")
        {
            FadeOut();
        }

        if (!isActive)
        {
            if (nextCaption != "")
                Activate(nextCaption);
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
            nextCaption = ""; // could use some list here
            textComponent.text = caption;
            isActive = true;
            FadeIn();
        }
    }

    public void FadeIn()
    {
        isFadedIn = false;
        StartCoroutine(FadeIn(fadeTime));
    }

    public void FadeOut()
    {
        isFadedIn = false;
        StartCoroutine(FadeOut(fadeTime));
    }

    IEnumerator FadeIn(float totalTime)
    {
        float timePassed = 0.0f;
        while (textComponent.color.a < 1.0f)
        {
            timePassed += Time.deltaTime;
            textComponent.color = new Color(textComponent.color.r, textComponent.color.g, textComponent.color.b, Mathf.Lerp(0.0f, 1.0f, timePassed / totalTime));
            yield return null;
        }

        yield return new WaitForSeconds(minVisibleTime);
        isFadedIn = true;
    }

    IEnumerator FadeOut(float totalTime)
    {
        float timePassed = 0.0f;

        while (textComponent.color.a > 0.0f)
        {
            timePassed += Time.deltaTime;
            textComponent.color = new Color(textComponent.color.r, textComponent.color.g, textComponent.color.b, Mathf.Lerp(1.0f, 0.0f, timePassed / totalTime));
            yield return null;
        }

        isActive = false;
    }

    internal void Reset()
    {
        nextCaption = "";
        isActive = false;
        isFadedIn = false;
        textComponent.color = new Color(textComponent.color.r, textComponent.color.g, textComponent.color.b, 0.0f);
    }
}
