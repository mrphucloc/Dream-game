using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveWalk = 300f;
    [SerializeField] float moveSpeed = 1f;
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
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.right * horizontalInput +  transform.forward * verticalInput;

        character.Move(moveDirection.normalized *  moveWalk* Time.deltaTime);
       if(anim != null)
        {
            anim.SetTrigger("walk");
        }

    }
}
