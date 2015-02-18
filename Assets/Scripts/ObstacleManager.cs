using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleManager : MonoBehaviour {

    List<Sequence> availableSequences;
    Sequence currentSequence;
    
    void Awake()
    {
        currentSequence = null;
        availableSequences = new List<Sequence>();
    }

    // how about getting level data from file instead of copypasting sequences here?

    public void SetLevel0()
    {
        currentSequence = null;
        availableSequences.Clear();

        CreateSequence(1, 3.0f, 250.0f, ObstacleType.CubeObstacle);
    }

    public void SetLevel1()
    {
        availableSequences.Clear();

        CreateSequence(1, 2.0f, 250.0f, ObstacleType.CubeObstacle);
        CreateSequence(1, 3.0f, 300.0f, ObstacleType.CubeObstacle);
    }

    public void SetLevel2()
    {
        availableSequences.Clear();

        CreateSequence(1, 2.0f, 250.0f, ObstacleType.CubeObstacle);
        CreateSequence(1, 2.0f, 350.0f, ObstacleType.CubeObstacle);
        CreateSequence(2, 3.0f, 350.0f, ObstacleType.CubeObstacle);
        CreateSequence(4, 4.0f, 400.0f, ObstacleType.CubeObstacle);
    }

    public void SetLevel3()
    {
        availableSequences.Clear();

        CreateSequence(1, 2.0f, 250.0f, ObstacleType.CubeObstacle);
        CreateSequence(1, 2.0f, 350.0f, ObstacleType.CubeObstacle);
        CreateSequence(2, 3.0f, 350.0f, ObstacleType.CubeObstacle);
        CreateSequence(4, 4.0f, 400.0f, ObstacleType.CubeObstacle);
        CreateSequence(6, 4.0f, 400.0f, ObstacleType.CubeObstacle);
    }

    public void SetLevel4()
    {
        availableSequences.Clear();

        CreateSequence(3, 3.0f, 500.0f, ObstacleType.CubeObstacle);
        CreateSequence(5, 3.0f, 500.0f, ObstacleType.CubeObstacle);
        CreateSequence(1, 2.0f, 700.0f, ObstacleType.CubeObstacle);
    }

    public void SetLevel5()
    {
        availableSequences.Clear();

        CreateSequence(3, 3.0f, 500.0f, ObstacleType.CubeObstacle);
        CreateSequence(5, 3.0f, 500.0f, ObstacleType.CubeObstacle);
        CreateSequence(1, 2.0f, 700.0f, ObstacleType.CubeObstacle);
        CreateSequence(1, 2.0f, 300.0f, ObstacleType.CubePhaseObstacle);
    }

    public void SetLevel6()
    {
        availableSequences.Clear();

        CreateSequence(3, 3.0f, 500.0f, ObstacleType.CubeObstacle);
        CreateSequence(5, 3.0f, 500.0f, ObstacleType.CubeObstacle);
        CreateSequence(1, 2.0f, 700.0f, ObstacleType.CubeObstacle);
        CreateSequence(1, 2.0f, 300.0f, ObstacleType.CubeInvPhaseObstacle);
    }

    public void SetLevel7()
    {
        // this was the original sandbox level
        availableSequences.Clear();

        CreateSequence(3, 3.0f, 500.0f, ObstacleType.CubeObstacle);
        CreateSequence(5, 3.0f, 500.0f, ObstacleType.CubeObstacle);
        CreateSequence(1, 2.0f, 700.0f, ObstacleType.CubePhaseObstacle);
        CreateSequence(1, 2.0f, 300.0f, ObstacleType.CubeInvPhaseObstacle);
    }

    void CreateSequence(int numObjects, float duration, float speed, ObstacleType obstacleType)
    {
        availableSequences.Add(new Sequence(numObjects, duration, speed, transform.position, obstacleType).Reset());
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (currentSequence == null)
        {
            currentSequence = GetRandomSequence();
        }
        else if (!currentSequence.Update())
        {
            currentSequence = null;
        }
	}

    Sequence GetRandomSequence()
    {
        if (availableSequences.Count == 0)
            return null;

        return availableSequences[Random.Range(0, availableSequences.Count)].Reset();
    }
}
