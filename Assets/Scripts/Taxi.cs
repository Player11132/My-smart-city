using UnityEngine;
using System.Collections;
using UnityEngine.AI;
 

public class Taxi : MonoBehaviour {
    public Transform target; 
    private NavMeshAgent nma; 
    public float probeRange = 1.0f; 
    private bool obstacleAvoid = false; 
    public float turnSpeed = 50f; 
   
    public Transform probePoint; 
    public Transform leftR; 
    public Transform rightR; 
   
    private Transform obstacleInPath; 
   
 
    void Start () {
        nma = this.GetComponent<NavMeshAgent>();
        if(probePoint == null)
            probePoint = transform;
        if(leftR == null) {
            leftR = transform;         
        }
        if(rightR == null)
            rightR = transform;        
    }
   
 
    void Update () {
       if(target!=null)
       {
        nma.SetDestination(target.position);
        RaycastHit hit;
        Vector3 dir = (target.position - transform.position).normalized;

        bool previousCastMissed = true;


        if(Physics.Raycast(probePoint.position, transform.forward, out hit, probeRange)){
            if(obstacleInPath != target.transform) { 
                Debug.Log("Found an object in path! - " + gameObject.name);
                Debug.DrawLine(transform.position, hit.point, Color.green);
                previousCastMissed = false;
                obstacleAvoid = true;
                nma.Stop(true);
                nma.ResetPath();
                if(hit.transform != transform) {                 
                    obstacleInPath = hit.transform;
                    Debug.Log("I hit: " + hit.transform.gameObject.name);              
                    dir += hit.normal * turnSpeed;
                   
                    Debug.Log("moving around an object - " + gameObject.name);
                   
                }
            }
        }
        
        if(obstacleAvoid&&previousCastMissed&&Physics.Raycast(leftR.position, transform.forward,out hit, probeRange)) {
            if(obstacleInPath != target.transform) {
                Debug.DrawLine(leftR.position, hit.point, Color.red);
                obstacleAvoid = true;
                nma.Stop();
                if(hit.transform != transform) {
                    obstacleInPath = hit.transform;
                    previousCastMissed = false;
                    
                    dir += hit.normal * turnSpeed;             
                }
            }
        }

        if(obstacleAvoid&&previousCastMissed&&Physics.Raycast(rightR.position, transform.forward,out hit, probeRange)) {
            if(obstacleInPath != target.transform) {
                Debug.DrawLine(rightR.position, hit.point, Color.green);
                obstacleAvoid = true;
                nma.Stop();
                if(hit.transform != transform) {
                    obstacleInPath = hit.transform;
                    dir += hit.normal * turnSpeed;
                }
            }
        }
       

         if (obstacleInPath != null) {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 toOther = obstacleInPath.position - transform.position;
            if (Vector3.Dot(forward, toOther) < 0) {

                Debug.Log("Back on Navigation! unit - " + gameObject.name);
                obstacleAvoid = false; 
                obstacleInPath = null;
                nma.ResetPath();
                nma.SetDestination(target.position);
                nma.Resume(); 
            }
           
        }
        if(obstacleAvoid) {
            Quaternion rot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime);
            transform.position += transform.forward * nma.speed * Time.deltaTime;
        }    
       }  
    }
   
    public void SetTarget(Transform tIn) {
        target = tIn;  
    }    
}