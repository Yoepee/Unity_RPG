using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// 1. ds위치 벡터
// 2. 방향 벡터
// struct MyVector {
//     public float x;
//     public float y;
//     public float z;

//     //
//     public float magnitude { get { return Mathf.Sqrt(x*x + y*y + z*z); } }
//     public MyVector normalized { get { return new MyVector(x/magnitude, y/magnitude, z/magnitude); } }

//     public MyVector(float x, float y, float z) { this.x = x; this.y = y; this.z = z; }

//     public static MyVector operator +(MyVector a, MyVector b) {
//         return new MyVector(a.x + b.x, a.y + b.y, a.z + b.z);
//     }

//     public static MyVector operator -(MyVector a, MyVector b) {
//         return new MyVector(a.x - b.x, a.y - b.y, a.z - b.z);
//     }

//     public static MyVector operator *(MyVector a, float b) {
//         return new MyVector(a.x * b, a.y * b, a.z * b);
//     } 
// }

// class Tank {
//     public float speed = 10.0f;
//     Passenger passenger; // 포함관계 Nested(중첩된) Prefab(Pre-Fabrication)
// }

// class FastTank: Tank {

// }

// class Passenger {

// }


public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 10.0f;

    bool _moveToDest = false;
    Vector3 _destPos;
    void Start()
    {
        // MyVector pos = new MyVector(0.0f, 10.0f, 0.0f);
        // MyVector to = new MyVector(10.0f, 0.0f, 0.0f);
        // MyVector from = new MyVector(5.0f, 0.0f, 0.0f);
        // MyVector dir = to - from;

        // dir = dir.normalized;

        // MyVector newPos = from + dir * _speed;

        // 방향 벡터
        // 1. 거리 (크기) 5 magnitude
        // 2. 실제 방향 -> normalized

        // Input 리스너 패턴
        Managers.Input.KeyAction -= OnKeyBoard;
        Managers.Input.KeyAction += OnKeyBoard;
        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;
    }

    // GameObject(Player)
    // Transform
    // PlayerController (*)
    float _yAngle = 0.0f;
    float wait_run_ratio = 0;
    bool isJumping;
    bool isFalling;
    bool isSkillCasting;
    bool isSkillChanneling;

    public enum PlayerState {
        Die,
        Moving,
        Idle,
        Jumping,
    }

    void UpdateState(PlayerState state) {
        switch (state) {
            case PlayerState.Die:
                UpdateDie();
                break;
            case PlayerState.Moving:
                UpdateMoving();
                break;
            case PlayerState.Idle:
                UpdateIdle();
                break;
            default:
                break;
        }
    }
    void UpdateDie() {

    }

    void UpdateIdle()
    {
        wait_run_ratio = Mathf.Lerp(wait_run_ratio, 0.0f, 10.0f * Time.deltaTime);
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("wait_run_ratio", wait_run_ratio);
        anim.Play("WAIT_RUN");
    }

    void UpdateMoving()
    {
        wait_run_ratio = Mathf.Lerp(wait_run_ratio, 1.0f, 10.0f * Time.deltaTime);
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("wait_run_ratio", wait_run_ratio);
        anim.Play("WAIT_RUN");
    }

    PlayerState _state = PlayerState.Idle;
    void Update()
    {
        // Local -> Global
        // TransformDirection

        // Global -> Local
        // InverseTransformDirection

        // transform.rotation
        _yAngle += Time.deltaTime * _speed;

        // 절대 회전값
        // transform.eulerAngles = new Vector3(0.0f, _yAngle, 0.0f);

        // +- delta
        // transform.Rotate(new Vector3(0.0f, Time.deltaTime * 100.0f, 0.0f));

        // transform.rotation = Quaternion.Euler(new Vector3(0.0f, _yAngle, 0.0f));

        // ===============================================================
        // if (Input.GetKey(KeyCode.W)) {
        //     // transform.position += transform.TransformDirection(Vector3.forward * Time.deltaTime * _speed);
        //     // transform.rotation = Quaternion.LookRotation(Vector3.forward);
        //     transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
        //     transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        // }

        // if (Input.GetKey(KeyCode.S)) {
        //     // transform.position += transform.TransformDirection(Vector3.back * Time.deltaTime * _speed);
        //     // transform.rotation = Quaternion.LookRotation(Vector3.back);
        //     transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
        //     transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        // }

        // if (Input.GetKey(KeyCode.A)) {
        //     // transform.position += transform.TransformDirection(Vector3.left * Time.deltaTime * _speed); 
        //     // transform.rotation = Quaternion.LookRotation(Vector3.left);
        //     transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
        //     transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        // }

        // if (Input.GetKey(KeyCode.D)) {
        //     // transform.position += transform.TransformDirection(Vector3.right * Time.deltaTime * _speed); 
        //     transform.rotation = Quaternion.LookRotation(Vector3.right);
        //     transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
        //     transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        // }
        // =================================================================
        // transform

        // if (_moveToDest) {
        if (_state == PlayerState.Moving) {
            Vector3 dir = _destPos - transform.position;

            // 도착 상태
            if (dir.magnitude < 0.0001f) {
                // _moveToDest = false;
                _state = PlayerState.Idle;
            } else {
                float moveDist = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);
                
                transform.position = transform.position + dir.normalized * moveDist;
                // transform.LookAt(_destPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
            }
        }

        // if (_moveToDest) {
        //     wait_run_ratio = Mathf.Lerp(wait_run_ratio, 1.0f, 10.0f * Time.deltaTime);
        //     Animator anim = GetComponent<Animator>();
        //     anim.SetFloat("wait_run_ratio", wait_run_ratio);
        //     // anim.Play("RUN");
        //     anim.Play("WAIT_RUN");
        // } else {
        //     wait_run_ratio = Mathf.Lerp(wait_run_ratio, 0.0f, 10.0f * Time.deltaTime);
        //     Animator anim = GetComponent<Animator>();
        //     anim.SetFloat("wait_run_ratio", wait_run_ratio);
        //     // anim.Play("WAIT");
        //     anim.Play("WAIT_RUN");
        // }

        switch (_state) {
            case PlayerState.Die:
                UpdateDie();
                break;
            case PlayerState.Moving:
                UpdateMoving();
                break;
            case PlayerState.Idle:
                UpdateIdle();
                break;
            default:
                break;
        }
    }

    void OnKeyBoard()
    {
        if (_state == PlayerState.Die) {
            return;
        }

        if (Input.GetKey(KeyCode.W))
        {
            // transform.position += transform.TransformDirection(Vector3.forward * Time.deltaTime * _speed);
            // transform.rotation = Quaternion.LookRotation(Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            // transform.position += transform.TransformDirection(Vector3.back * Time.deltaTime * _speed);
            // transform.rotation = Quaternion.LookRotation(Vector3.back);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            // transform.position += transform.TransformDirection(Vector3.left * Time.deltaTime * _speed); 
            // transform.rotation = Quaternion.LookRotation(Vector3.left);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            // transform.position += transform.TransformDirection(Vector3.right * Time.deltaTime * _speed); 
            transform.rotation = Quaternion.LookRotation(Vector3.right);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        }

        // _moveToDest = false;
        _state = PlayerState.Idle;
        UpdateState(_state);
    }

    void OnMouseClicked(Define.MouseEvnet evt)
    {
        // if (evt != Define.MouseEvnet.Click)
        // {
        //     return;
        // }
        if (_state == PlayerState.Die) {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        LayerMask mask = LayerMask.GetMask("Wall");
        // int mask = (1 << 8) | (1 << 9);
        if (Physics.Raycast(ray, out hit, 100.0f, mask))
        {
            _destPos = hit.point;
            // _moveToDest = true;
            _state = PlayerState.Moving;
        }
    }
}
