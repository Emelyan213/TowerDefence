using System.Collections;
using System.Collections.Generic;
using Source.Navigation;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public Mover mover;

    public WayPoint[] wayPoints;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            mover.SetWayPoints(wayPoints);
            mover.StartMoveOnPoints();

        }
    }
}
