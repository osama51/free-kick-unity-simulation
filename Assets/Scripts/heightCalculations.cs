using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class heightCalculations : MonoBehaviour
{
    public Rigidbody Ball;
    public Transform Goal;
    public AudioSource audioSource;
    
    public AudioClip kick;
    //public AudioClip grass;
    public float volume=1.0f;

    public float gravity = 9.8f;
    public float v0;
    public float theta;
    public float distance;

    public float maxHeight;

    
     
    //  elapsed_t1 = (v0 * math.sin(theta))/self.gravity  
    // print("T", elapsed_t1)

    // max_height = (v0 * elapsed_t1 * math.sin(theta)) - (0.5 * self.gravity * (elapsed_t1**2))
    
    // distance_maxh = v0 * elapsed_t1 * math.cos(theta)
    // print("S", distance_maxh)
    
    // elapsed_t2 = (distanc_s - distance_maxh) / (v0 * math.cos(theta))
    // print("Tao", elapsed_t2,'\n')
    
    // goal_height = max_height - (0.5 * self.gravity * (elapsed_t2**2))

    /*
        velocity_x = v0 * math.cos(theta)
        velocity_y = v0 * math.sin(theta) - self.gravity * elapsed_t 
        
        theta: the kick angle
        v0: The initial velocity
        distance_s: distance from start to goal plane 
        elapsed_t1: The horizontal distance between the kicking point and the goal plane
        elapsed_t2: The horizontal distance between the goal plane and the maximum elevation point
        t: range of total time (0 : total time) with a step of 0.01
    */
    public void Simulation()
    {
        Physics.gravity = Vector3.up * -1 * gravity;
        Ball.useGravity = true;
        Ball.velocity = Velocity();
    }

    // void heightCalculation() 
    // {
    //     float time1 = (v0 * Mathf.Sin(ConvertToRad(theta))) / gravity;
    //     maxHeight = (v0 * time1 * Mathf.Sin(ConvertToRad(theta))) - (0.5f * gravity * Mathf.Pow(time1, 2.0f));
    //     print($"Max Height: {maxHeight}");

    //     float distanceAtMaxHeight = v0 * time1 * Mathf.Cos(ConvertToRad(theta));
    //     print("Distance @ Max Height: " + distanceAtMaxHeight);

    //     float time2 = (distance - distanceAtMaxHeight) / (v0 * Mathf.Cos(ConvertToRad(theta)));
    //     float heightAtGoalPlane = maxHeight - (0.5f * gravity * Mathf.Pow(time2, 2.0f));
    // }

    Vector3 Velocity()
    {
        distance = Goal.position.x - Ball.position.x;
        print($"real_distance: {distance}");
        
        float time1 = (v0 * Mathf.Sin(ConvertToRad(theta))) / gravity;
        maxHeight = (v0 * time1 * Mathf.Sin(ConvertToRad(theta))) - (0.5f * gravity * Mathf.Pow(time1, 2.0f));
        print($"Max Height: {maxHeight}");

        float distanceAtMaxHeight = v0 * time1 * Mathf.Cos(ConvertToRad(theta));
        print("Distance @ Max Height: " + distanceAtMaxHeight);

        float time2 = (distance - distanceAtMaxHeight) / (v0 * Mathf.Cos(ConvertToRad(theta)));
        float heightAtGoalPlane = maxHeight - (0.5f * gravity * Mathf.Pow(time2, 2.0f));
        print($"Height at Goal Plane: {heightAtGoalPlane}");


        Vector3 velocityXZ = new Vector3 (v0 * Mathf.Cos(ConvertToRad(theta)),0,0);
        Vector3 velocityY = Vector3.up * (v0 * Mathf.Sin(ConvertToRad(theta)) - (gravity * Time.deltaTime));
        print("VelocityY: " + velocityY);
        print("VelocityX: " + velocityXZ);
        print("V0: " + v0);
        print("Mathf.Sin(theta): " + Mathf.Sin(ConvertToRad(theta)));
        print($"GRAVITY: {(gravity)}");
        print($"TIME: {(Time.deltaTime)}");
        

        //heightCalculation();
        // float displacementY = Goal.position.y - Ball.position.y;
        // Vector3 displacementXZ = new Vector3 (distance, 0, Goal.position.z - Ball.position.z);

        // Vector3 velocityY = Vector3.up * Mathf.Sqrt (-2 * gravity * maxHeight);
        // Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2*maxHeight/gravity) + Mathf.Sqrt((2*displacementY - maxHeight)/gravity));
        return (velocityXZ + velocityY);
    }


    // Start is called before the first frame update
    void Start()
    {
        audioSource.volume = 0.5f;
        //Ball = GetComponent<Rigidbody>();
        // Ball.position.x = Goal.position.x - distance;
        //Ball.useGravity = true;
        moveBall();
    }
    public void moveBall()
    {
        Vector3 newPoistion = new Vector3 (Goal.position.x - distance, Ball.position.y, Ball.position.z);
        Ball.MovePosition(newPoistion);
    } 

    // Update is called once per frame
    void Update()
    {
       
        if(Input.GetKeyDown (KeyCode.Space)){
            
            audioSource.PlayOneShot(kick,  volume);
            Simulation();
        }
    }

    public float ConvertToDegree(float rad)
    {
        return (180 / Mathf.PI) * rad;
    }

    public float ConvertToRad(float degree)
    {
        return (Mathf.PI / 180) * degree;
    }
}
