using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class RunnerAgent : Agent
{
    public Transform Runner;
    //public Transform Sphere;
    public float forceMultiplier = 10;
    private Rigidbody rbody;
    float m_MovingTowardsDot;

    [Header("Target To Walk Towards")]
    [Space(10)]
    public Transform target;
    public GameObject ground;

   

    Vector3 orientation;
    Vector3 m_DirToTarget;
    Quaternion m_LookRotation; // LookRotation from m_TargetDirMatrix to Target
    Matrix4x4 m_TargetDirMatrix; // Matrix used by agent as orientation reference
    EnvironmentParameters defaultParams;

    public override void Initialize()
    {

        var groundRenderer = ground.GetComponent<Renderer>();
        groundRenderer.material.SetColor("_Color", Color.grey);
        rbody = GetComponent<Rigidbody>();

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

        // Rewards
        float distanceToTarget = Vector3.Distance(this.transform.localPosition, Runner.localPosition);
        Debug.Log("distance: " + distanceToTarget);

        // fell off the cliff
        if (this.transform.localPosition.y < 0)
        {
            SetReward(-1.0f);
            EndEpisode();
        }

        // Reached target
        if (distanceToTarget > 1.8f && distanceToTarget < 10.0f)
        {
            SetReward(0.1f);
        }
        else if (distanceToTarget > 10.0f)
        {
            SetReward(1.0f);
            EndEpisode();
        }
        else
        {
            SetReward(-1.0f);
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

    // Reward moving towards target & Penalize moving away from target.
    void RewardFunctionMovingTowards()
    {
        m_MovingTowardsDot = Vector3.Dot(rbody.velocity, m_DirToTarget.normalized);
        AddReward(0.01f * m_MovingTowardsDot);
    }

    // Reward facing target & Penalize facing away from target
    void RewardFunctionFacingTarget()
    {
        float bodyRotRelativeToMatrixDot = Quaternion.Dot(m_TargetDirMatrix.rotation, transform.rotation);
        AddReward(0.01f * bodyRotRelativeToMatrixDot);
    }

    // Existential penalty for time-contrained tasks.
    void RewardFunctionTimePenalty()
    {
        Debug.Log("reward it bad!");
        AddReward(-0.001f);
    }

    static void OnGUI(string msg)
    {
        var myLog = GUI.TextArea(new Rect(10, 10, Screen.width - 10, Screen.height - 10), msg);
    }
}

