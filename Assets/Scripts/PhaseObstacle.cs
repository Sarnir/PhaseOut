using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Obstacle))]
public class PhaseObstacle : MonoBehaviour
{
    PlayerController player;
    Obstacle obstacle;
    bool isPhasedOut;
    float spawnTime;

    [SerializeField]
    bool inverted;

    const float minVisibleTime = 0.3f;

    void Start()
    {
        spawnTime = Time.time;
        isPhasedOut = true;
        PhaseOut(false);

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        obstacle = GetComponent<Obstacle>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - spawnTime > minVisibleTime)
            PhaseOut(player.IsPhasedOut() ^ inverted);
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
