using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour
{
    // 1) 나한테 RigidBody가 있어야 한다 (IsKinematic: Off)
    // 2) 나한테 Collider가 있어야 한다. (IsKinematic: Off)
    // 3) 상대한테 Collider가 있어야 한다. (IsKinematic: Off)
    private void OnCollisionEnter(Collision collision) {
        Debug.Log("Collision !");
    }

    //
    private void OnTriggerEnter(Collider other) {
        Debug.Log("Trigger");
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
