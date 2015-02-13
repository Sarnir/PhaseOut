using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public bool isAlive { get; set; }

    bool isPhasedOut = false;
    float timeWhenPhasedOut = 0.0f;
    float timeWhenExitedCollision = 0.0f;

    ArrayList cubicleList = null;

    [SerializeField()]
    PhaseOutBar phaseOutBar = null;
    [SerializeField()]
    Score score = null;

	// Use this for initialization
	void Start ()
    {
        // should check for a missing component here :\

        cubicleList = new ArrayList();
        CreateCubicles(64);

        Reset();
	}


    public bool IsPhasedOut()
    {
        return isPhasedOut;
    }

    private void CreateCubicles(int count)
    {
        cubicleList.Clear();
        GameObject cubicles = new GameObject("cubicles");
        cubicles.transform.parent = gameObject.transform;
        for (int i = 0; i < count; i++)
        {
            var cube = new GameObject("cubicle");
            cube.transform.localScale = gameObject.transform.localScale / (float)(count/8);
            cube.AddComponent<Rigidbody2D>().interpolation = RigidbodyInterpolation2D.Interpolate;
            cube.AddComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
            cube.SetActive(false);
            cube.transform.parent = cubicles.transform;
            cube.rigidbody2D.gravityScale = 0.0f;

            cubicleList.Add(cube);
        }
    }

    void ResetCubicles()
    {
        foreach (GameObject cube in cubicleList)
        {
            cube.SetActive(false);
            cube.transform.position = transform.position;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (phaseOutBar.IsDepleted())
            PhaseOut(false);
	}

    void OnTriggerEnter2D()
    {
        if (isPhasedOut)
        {
            print("Time between phase out and collision is " + (Time.time - timeWhenPhasedOut));
            score.AddPhaseOutBonus(Time.time - timeWhenPhasedOut);
        }
        else
        {
            if (isAlive)
                Die();
        }
    }

    void OnTriggerExit2D()
    {
        if (isPhasedOut)
        {
            timeWhenExitedCollision = Time.time;
        }
        else
        {
            if (isAlive)
                Die();
        }
    }

    private void Die()
    {
        isAlive = false;

        gameObject.renderer.enabled = false;
        phaseOutBar.renderer.enabled = false;

        UnleashCubicles();
    }

    private void UnleashCubicles()
    {
        foreach(GameObject cube in cubicleList)
        {
            cube.SetActive(true);
            cube.rigidbody2D.AddForce(GenerateRandomVector2(300));
            cube.rigidbody2D.AddTorque(Random.Range(-300, 300));
        }
    }

    private Vector2 GenerateRandomVector2(int p)
    {
        return new Vector2(Random.Range(-p, p), Random.Range(-p, p));
    }

    public void PhaseOut(bool val)
    {
        if (!isAlive)
            return;

        isPhasedOut = val;
        gameObject.renderer.enabled = !isPhasedOut;
        phaseOutBar.SetConsumed(val);

        if (val)
        {
            timeWhenPhasedOut = Time.time;
        }
        else
        {
            print("Time between collision exit and phase in is " + (Time.time - timeWhenExitedCollision));
            score.AddPhaseInBonus(Time.time - timeWhenExitedCollision);
        }
    }

    internal void Reset()
    {
        isAlive = true;
        PhaseOut(false);
        phaseOutBar.Reset();
        ResetCubicles();
    }
}
