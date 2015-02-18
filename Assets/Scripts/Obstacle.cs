using UnityEngine;
using System.Collections;

public enum ObstacleType
{
    CubeObstacle,
    CubePhaseObstacle,
    CubeInvPhaseObstacle
};

public class Obstacle : MonoBehaviour {

    private bool seen = false;

	// Use this for initialization
	protected void Start () {
	}

    public void AddForce(Vector2 force)
    {
        rigidbody2D.AddForce(force);
    }
	
	// Update is called once per frame
	protected void Update () {
        if (renderer.isVisible)
            seen = true;

        if (seen && !renderer.isVisible)
            Destroy(gameObject);
	}

    internal void SetSpeed(float speed)
    {
        rigidbody2D.AddForce(-rigidbody2D.velocity);
        rigidbody2D.AddForce(new Vector2(speed, 0.0f));
    }
}
