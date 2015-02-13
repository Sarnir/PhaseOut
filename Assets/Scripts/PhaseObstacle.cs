using UnityEngine;
using System.Collections;

public class PhaseObstacle : Obstacle
{
    PlayerController player;
    bool isPhasedOut;

    void Start()
    {
        base.Start();
        isPhasedOut = true;
        PhaseOut(false);

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();

        PhaseOut(player.IsPhasedOut());
    }

    void PhaseOut(bool val)
    {
        if (isPhasedOut == val)
            return;

        isPhasedOut = val;
        var color = gameObject.renderer.material.color;
        color.a = val ? 0.0f : 1.0f;
        gameObject.renderer.material.color = color;
    }
}
