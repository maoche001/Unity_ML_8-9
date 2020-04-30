using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using MLAgents.Sensors;


public class GunAgent : Agent
{
    public float speed = 10;
    private Rigidbody rigRobot;
    private Rigidbody rigGun;
    private Rigidbody rigEnemy;
    private int countTotal;
    private int countComplete;
    public GameObject[] bullet;

    // Start is called before the first frame update
    void Start()
    {
        rigRobot = GetComponent<Rigidbody>();
        rigGun = GameObject.Find("gun").GetComponent<Rigidbody>();
        rigEnemy = GameObject.Find("enemy").GetComponent<Rigidbody>();
    }
    public override void OnEpisodeBegin()
    {
        countTotal++;

        rigRobot.velocity = Vector3.zero;
        rigRobot.angularVelocity = Vector3.zero;
        rigGun.velocity = Vector3.zero;
        rigGun.angularVelocity = Vector3.zero;
        rigEnemy.velocity = Vector3.zero;
        rigEnemy.angularVelocity = Vector3.zero;


        
        Vector3 posRobot = new Vector3(0, 5,-11);
        transform.position = posRobot;
        Vector3 posGun = new Vector3(posRobot.x, 5.45f, -9.675f);
        rigGun.position = posGun;
        Vector3 posEnemy = new Vector3(Random.Range(-5, 5), 5, Random.Range(0, 10));
        rigEnemy.position = posEnemy;
        enemy.complete = false;
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        sensor.AddObservation(rigGun.position);
        sensor.AddObservation(rigGun.velocity);
        sensor.AddObservation(rigEnemy.position);
    }
    public override void OnActionReceived(float[] vectorAction)
    {
        Vector3 controlSignal = Vector3.zero;
        controlSignal.z = vectorAction[0];
        controlSignal.x = vectorAction[1];
        rigGun.AddForce(controlSignal * speed);

        if (enemy.complete)
        {
            countComplete++;
            SetReward(1);
            EndEpisode();
        }
        if (rigGun.position.z > 15|| rigGun.position.z <-10|| rigGun.position.x > 7|| rigGun.position.x < -7)
        {
            SetReward(-1);
            EndEpisode();
        }
    }
}
