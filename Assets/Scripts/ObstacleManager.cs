using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleManager : MonoBehaviour {

    List<Sequence> availableSequences;
    Sequence currentSequence;

	// Use this for initialization
	void Start ()
    {
        currentSequence = null;
        availableSequences = new List<Sequence>();

        SetLevel1();
	}

    void SetLevel1()
    {
        availableSequences.Clear();

        availableSequences.Add(new Sequence(3, 3.0f, transform.position, ObstacleType.CubeObstacle).Reset());
        availableSequences.Add(new Sequence(5, 3.0f, transform.position, ObstacleType.CubeObstacle).Reset());
        availableSequences.Add(new Sequence(1, 2.0f, transform.position, ObstacleType.CubePhaseObstacle).Reset());
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (currentSequence == null)
            currentSequence = GetRandomSequence();

        if (!currentSequence.Update())
            currentSequence = null;
	}

    Sequence GetRandomSequence()
    {
        return availableSequences[Random.Range(0, availableSequences.Count)].Reset();
    }
}
