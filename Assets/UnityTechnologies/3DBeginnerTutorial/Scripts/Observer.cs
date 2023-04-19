using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour{
    
    public float detectionRadius = 2.3f;
    public float detectionAngle = 40.0f;

    public Transform player;
    public GameEnding gameEnding;

    void Update() {
        Vector3 direction = player.position - transform.position + Vector3.up;
        Ray ray = new Ray(transform.position, direction);
        RaycastHit raycastHit;
        if(Physics.Raycast(ray, out raycastHit)){
            if(LookForPlayer() /*&& raycastHit.collider.transform == player*/){
                gameEnding.CaughtPlayer();
            }
        }
    }


    public bool LookForPlayer(){
        Vector3 enemyPosition = transform.position;
        Vector3 toPlayer = player.transform.position - enemyPosition;
        toPlayer.y = 0;

        if (toPlayer.magnitude <= detectionRadius) {
            if (Vector3.Dot(toPlayer.normalized, transform.forward) > Mathf.Cos(detectionAngle * 0.5f * Mathf.Deg2Rad)) {
                    return true;
            }
        }
        return false;
    }




#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Color c = new Color(0.8f, 1, 0, 0.4f);
        UnityEditor.Handles.color = c;

        Vector3 rotatedForward = Quaternion.Euler(
            0,
            -detectionAngle * 0.5f,
            0) * transform.forward;

        UnityEditor.Handles.DrawSolidArc(
            transform.position,
            Vector3.up,
            rotatedForward,
            detectionAngle,
            detectionRadius);

    }
#endif
//https://www.youtube.com/watch?v=MB7d3MdVHwU
}

//Note: Must change gargoyle's point of view cone down to the ground in order for this to work for both enemies 
