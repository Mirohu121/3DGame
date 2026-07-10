using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{


    PlayerInput playerInput;
    private float speedMax;
     [SerializeField] float accel;
    [SerializeField] float rotateSpeed;
    [SerializeField] Animator animator;
   [SerializeField] float jumpSpeed;

    [SerializeField] float accel;
    Rigidbody rb;
    Vector3 rotateTarget;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var accelVec = playerInput.actions["Move"].ReadValue<Vector2>();

        var cameraDir = playerInput.camera.transform.forward;
        cameraDir.y = 0;
        cameraDir = cameraDir.normalized;

        var cameraRight = playerInput.camera.transform.right;

        var accelVec3D =
            cameraDir * accelVec.y * accel
            + cameraRight * accelVec.x * accel;
        rb.AddForce(accelVec3D, ForceMode.Acceleration);

        //プレイヤーの向きを変える
        if(accelVec3D !=Vector3.zero)
        {
            rotateTarget =accelVec3D.normalized;
        }

        Vector3 forward =transform.forward;

        transform.up =Vector3.up;

        transform.forward =Vector3.Slerp(forward,rotateTarget,rotateSpeed*Time.deltaTime);

        Vector3 velocityXZ =rb.linearVelocity;
        velocityXZ.y =0;
        animator.SetFloat("MoveSpeed",velocityXZ.magnitude);

        if(playerInput.actions["Jump"].WasPressedThisFrame())
        {
            Vector3 jumpVec =new Vector3(0,jumpSpeed,0);
            rb.AddForce(jumpVec,ForceMode.VelocityChange);
        }
    }
}
