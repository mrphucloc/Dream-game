using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float rotationSpeed = 360f;
    CharacterController character;
    Animator anim;
    
    void Start()
    {
        character = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) 
            return;

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        character.Move(moveDirection * moveSpeed * Time.deltaTime);

        if (moveDirection.magnitude > 0.1f) // Chỉ xoay nếu có di chuyển
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        if (anim != null)
        {
            anim.SetFloat("walk", moveDirection.magnitude);
        }
    }
}
