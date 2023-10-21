using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour
{
    // 1) 나 혹은 상대한테 RigidBody가 있어야 한다 (IsKinematic: Off)
    // 2) 나한테 Collider가 있어야 한다. (IsKinematic: Off)
    // 3) 상대한테 Collider가 있어야 한다. (IsKinematic: Off)
    // 피격 등 용도로 사용
    private void OnCollisionEnter(Collision collision) {
        Debug.Log($"Collision ! @ {collision.gameObject.name}");
    }

    // 1) 둘 다 Collider가 있어야 한다.
    // 2) 둘 중 하나는 IsTrigger: On
    // 3) 둘 중 하나는 RigidBody가 있어야 한다.
    // 물리와 관계없이 범위 내에 닿는지 확인하는 용도로 주로 사용
    // 무기가 닿아서 몬스터 가격, 바닥에 장판 공격 등
    private void OnTriggerEnter(Collider other) {
        Debug.Log($"Trigger @ {other.gameObject.name}");
    }
    void Start()
    {
        
    }

    void Update()
    {
        // Vector3 look = transform.TransformDirection(Vector3.forward);
        // Debug.DrawRay(transform.position + Vector3.up, look * 10.0f, Color.red);

        // // RaycastHit hit;
        // RaycastHit[] hits;
        // hits = Physics.RaycastAll(transform.position + Vector3.up, look, 10.0f);

        // foreach ( RaycastHit hit in hits ) {
        //     Debug.Log($"Raycast ! @{hit.collider.gameObject.name}");
        // }
        // // if(Physics.Raycast(transform.position, Vector3.forward, out hit, 10.0f)) {
        // //     Debug.Log($"Raycast ! @{hit.collider.gameObject.name}");
        // // }

        // Local <-> World <-> Viewport <-> Screen (화면)
        // Viewport와 Screen은 유사

        // Pixel 좌표 = Screen 좌표
        // Debug.Log(Input.mousePosition); // Screen

        // 화면 비율에 따라 표시하는 방식
        // Screen을 비율로 전환해서 나오는 것이기때문에 Viewport가 유사함
        // Debug.Log(Camera.main.ScreenToViewportPoint(Input.mousePosition)); // Viewport

        // GameObject.FindGameObjectsWithTag("Monster");

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            // Vector3 dir = mousePos - Camera.main.transform.position;
            // dir = dir.normalized;

            // Debug.DrawRay(Camera.main.transform.position, dir * 100.0f, Color.red, 1.0f);
            
            // RaycastHit hit;
            // if (Physics.Raycast(Camera.main.transform.position, dir, out hit, 100.0f))
            // {
            //     Debug.Log($"Paycast Camera @ {hit.collider.gameObject.name}");
            // }
            
            Debug.DrawLine(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);
            
            RaycastHit hit;

            LayerMask mask = LayerMask.GetMask("Monster");
            // int mask = (1 << 8) | (1 << 9);
            if(Physics.Raycast(ray, out hit, 100.0f, mask)) {
                // Debug.Log($"Paycast Camera @{hit.collider.gameObject.name}");
                Debug.Log($"Paycast Camera @{hit.collider.gameObject.tag}");
            }

        }
    }
}
