using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 4f;
    public float rotationSpeed = 10f;

    CharacterController controller;
    Animator animator;
    Transform cam;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        cam = Camera.main.transform;
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 input = new Vector3(h, 0, v);

        if (input.magnitude < 0.1f)
        {
            animator.SetFloat("Speed", 0f);
            return;
        }

        Vector3 camForward = cam.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = cam.right;

        Vector3 moveDir = (camForward * v + camRight * h).normalized;


        controller.Move(moveDir * speed * Time.deltaTime);

        Quaternion targetRotation = Quaternion.LookRotation(moveDir);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );

        animator.SetFloat("Speed", 1f);
    }
}
