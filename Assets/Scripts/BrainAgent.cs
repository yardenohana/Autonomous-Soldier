
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class BrainAgent : Agent
{
    public Transform Runner;

    public float forceMultiplier = 10;
    private Rigidbody rbody;
    float m_MovingTowardsDot;

    [Header("Target To Walk Towards")]
    [Space(10)]
    public Transform target;
    public GameObject ground;
    [HideInInspector]
    public FieldOfView fieldOfView;
    Vector3 m_DirToTarget;
    Matrix4x4 m_TargetDirMatrix; // Matrix used by agent as orientation reference
    EnvironmentParameters defaultParams;
    float dstToTarget;
    float checkVisibleTrg;
    GameObject theplayer;

    public override void Initialize()
    {

        var groundRenderer = ground.GetComponent<Renderer>();
        groundRenderer.material.SetColor("_Color", Color.grey);
        rbody = GetComponent<Rigidbody>();
        theplayer = GameObject.Find("Chaser");
        fieldOfView = theplayer.GetComponent<FieldOfView>();
        //checkVisibleTrg = fieldOfView.FindVisibleTargets();
        dstToTarget = fieldOfView.getDistance();



    }

    public override void CollectObservations(VectorSensor sensor)
    {

        // Target and Agent positions
        sensor.AddObservation(Runner.localPosition);
        sensor.AddObservation(this.transform.localPosition);

        // Agent velocity
        sensor.AddObservation(rbody.velocity.x);
        sensor.AddObservation(rbody.velocity.z);
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        //OnGUI("Action recieved");

        Debug.Log("Action recieved");

        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = vectorAction[0];
        controlSignal.z = vectorAction[1];
        rbody.AddForce(controlSignal * forceMultiplier);
        if (this.transform.localPosition.y < 0)
        {
            SetReward(-1.0f);
            EndEpisode();
        }
        // Rewards

        foreach (Transform visibleTarget in fieldOfView.visibleTargets)
        {
            float dstToTarget = Vector3.Distance(transform.position, visibleTarget.position);

            Debug.Log("distance from brain: " + dstToTarget);

            // Reached target
            if (dstToTarget < 2f)
            {
                //var groundRenderer = ground.GetComponent<Renderer>();
                //groundRenderer.material.SetColor("_Color", Color.green);

                SetReward(1.0f);
                EndEpisode();
            }
        }






    }

    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = Input.GetAxis("Horizontal");
        actionsOut[1] = Input.GetAxis("Vertical");
    }


    public override void OnEpisodeBegin()
    {
        var groundRenderer = ground.GetComponent<Renderer>();
        groundRenderer.material.SetColor("_Color", Color.grey);

        if (this.transform.localPosition.y < 0)
        {
            // If the Agent fell, zero its momentum
            this.rbody.angularVelocity = Vector3.zero;
            this.rbody.velocity = Vector3.zero;
            this.transform.localPosition = new Vector3(0, 0.5f, 0);
        }

        // Move the target to a new spot
        Runner.localPosition = new Vector3(Random.value * 8 - 4, 0.5f, Random.value * 8 - 4);
    }

}
