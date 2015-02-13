using UnityEngine;
using System.Collections;

public enum ObstacleType
{
    CubeObstacle,
    CubePhaseObstacle
};

public class Obstacle : MonoBehaviour {

    public Vector2 force;
    private bool seen = false;

	// Use this for initialization
	protected void Start () {
        rigidbody2D.AddForce(force);
	}
	
	// Update is called once per frame
	protected void Update () {
        if (renderer.isVisible)
            seen = true;

        if (seen && !renderer.isVisible)
            Destroy(gameObject);
	}
}
