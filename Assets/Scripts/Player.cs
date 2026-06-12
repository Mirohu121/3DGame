using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{


    PlayerInput playerInput;
    private float speedMax;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        var moveVec = playerInput.actions["Move"].ReadValue<Vector2>();
        var cameraDir = playerInput.camera.transform.forward;
        cameraDir.y = 0;
        cameraDir = cameraDir.normalized;
        var moveVec3D = new Vector3(moveVec.x * speedMax, 0, moveVec.y * speedMax);
        var cameraRight =playerInput.camera.transform.right;
        _ = cameraDir * moveVec.y * speedMax + cameraRight * moveVec.x * speedMax;
        transform.position = transform.position + moveVec3D * Time.deltaTime;

    }
}
