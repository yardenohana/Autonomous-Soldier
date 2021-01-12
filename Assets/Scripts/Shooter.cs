using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class Shooter : Agent
{
    public Transform shootingPoint;
    public int minStepBetweenShots = 50;
    public int damage = 100;
    public KeyCode shoot;

    private bool shotAvailable = true;
    private int stepsUntilShotIsAvailable = 0;

    private Vector3 startPosition;
    private Rigidbody rb;

    // For the enemy layer we will need 8 bit shift left.
    public const int layerShift = 8;


    /*
     * The function shoots a raycast.
     * Input: Void
     * Output: Void
     */
    private void Shoot()
    {
        Debug.Log("entered Shoot!");

        // continue only if available
        if (!shotAvailable)
            return;

        var layerMask = 1 << layerShift;
        var direction = transform.forward;

        Debug.DrawRay(shootingPoint.position, direction * 200f, Color.green, 2f);
        
        // If the ray did hit the target zombie layer
        if (Physics.Raycast(shootingPoint.position, direction, out var hit, 200f, layerMask))
        {
            // Zombie has been shot
            hit.transform.GetComponent<Zombie>().RecieveShot(damage);

            // it is now not available since the other player died
            shotAvailable = false;

            // set the steps until next shot to be constant
            stepsUntilShotIsAvailable = minStepBetweenShots;
            
            RegisterKill(); // reward 1
        }
    }


    private void FixedUpdate()
    {
        var direction = transform.forward;

        if (!shotAvailable)
        {
            Debug.DrawRay(shootingPoint.position, direction * 200f, Color.red, 2f);
            stepsUntilShotIsAvailable--;

            if (stepsUntilShotIsAvailable <= 0)
                shotAvailable = true;
        }
    }


    /*
     * This function responsible for the actions the agent makes.
     */
    public override void OnActionReceived(float[] vectorAction)
    {
        //Debug.Log("action[0]: " + vectorAction[0]);
        if (Mathf.RoundToInt(vectorAction[0]) >= 1)
        {
            Shoot();
        }
    }


    /*
     * This function responsible for the agents' observations 
     * of the environment.
     * Input: the mlagents observations
     * Output: void
     */
    //public override void CollectObservations(VectorSensor sensor)
    //{
    //    base.CollectObservations(sensor);
    //    //sensor.AddObservation(transform.)
    //}


    /*
     * Initialize agent in the scene before the first frame.
     * Input: void
     * Output: void
     */
    public override void Initialize()
    {
        Debug.Log("initialize agent");
        startPosition = transform.position;
        rb = GetComponent<Rigidbody>();
    }


    /*
     * Initialize agent every episode.
     * Input: void
     * Output: void
     */
    public override void OnEpisodeBegin()
    {
        Debug.Log("Episode begin!");
        transform.position = startPosition;

        // start with zero velocity
        rb.velocity = Vector3.zero;
        
        // our agent is able to shoot
        shotAvailable = true;

        stepsUntilShotIsAvailable = minStepBetweenShots;
    }


    /*
     * Set shooting in gaming mode.
     * Input: the agents' actions vector
     * Output: void
     */
    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = Input.GetKey(shoot) ? 1f : 0f;
    }


    /*
     * The method is called when the agent
     * has managed to neutralize the enemy.
     * Adds success reward, and ends the current episode.
     */
    public void RegisterKill()
    {
        AddReward(1.0f);
        EndEpisode();
    }
}
