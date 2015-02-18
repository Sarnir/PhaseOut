using UnityEngine;
using System.Collections;

public class Sequence
{

    int numObjectsToSpawn;
    float spawnPercent;
    float timePassed;
    float duration;
    float speed;
    bool isActive;
    ObstacleType obstacleType;

    Vector2 spawnPosition;

    string[] obstacleNames = { "CubeObstacle", "CubePhaseObstacle", "CubeInvPhaseObstacle" };

    public Sequence(int _numObjects, float _duration, float _speed, Vector2 _pos, ObstacleType _obstacleType)
    {
        isActive = false;
        numObjectsToSpawn = _numObjects;
        speed = -Mathf.Abs(_speed);
        duration = _duration;
        spawnPosition = _pos;
        obstacleType = _obstacleType;
    }

	// Use this for initialization
	public Sequence Reset ()
    {
        isActive = true;
        timePassed = 0.0f;
        spawnPercent = 0.0f;
        return this;
	}
	
	// Update is called once per frame
	public bool Update ()
    {
        if (isActive)
        {
            timePassed += Time.deltaTime;

            if (timePassed / duration > spawnPercent && spawnPercent < 1.0f)
            {
                GameObject obstacle = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/" + obstacleNames[(int)obstacleType])) as GameObject;
                obstacle.GetComponent<Obstacle>().SetSpeed(speed);
                obstacle.transform.position = spawnPosition;

                spawnPercent += 1 / (float)numObjectsToSpawn;
            }

            if (timePassed > duration)
            {
                isActive = false;
                //Destroy(this);
            }
        }

        return isActive;
	}
}
