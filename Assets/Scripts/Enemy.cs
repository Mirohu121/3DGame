using R3;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] float moveSpeed = 3;
    public Collider playerCollider { get; set; }
    Rigidbody rb;
    [SerializeField] float rotateSpeed = 20;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //var subVec = playerCollider.bounds.center - rb.position;
        //subVec.y = 0;
        //rb.linearVelocity = subVec.normalized * moveSpeed;

        var direction =playerCollider.bounds.center -rb.position;
        bool isSeenPlayer = true;

        if (Physics.Raycast(rb.position, direction.normalized, out var hitInfo))
        {
            isSeenPlayer = false;
        }

        if (isSeenPlayer)
        {
            var subVec = direction;
            subVec.y = 0;
            rb.linearVelocity = subVec.normalized * moveSpeed;
        }

        var rotateTarget = subVec.normalized;
        Vector3 forward = transform.forward;
        transform.forward = Vector3.Slerp(forward, rotateTarget,
            rotateSpeed * Time.deltaTime);
    }
}
