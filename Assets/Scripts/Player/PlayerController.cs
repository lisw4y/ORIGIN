using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float rotationSpeed = 240.0f;
    public float jumpSpeed = 7.0f;
    public float gravity = 20.0f;
    public GameObject hand;
    public GameObject inventoryPanel;
    
    Vector3 movementDirection;
    Animator animator;
    CharacterController characterController;
    PlayerStats playerStats;
    CharacterCombat characterCombat;
    Interactable interactable;
    bool isInteracting = false;
    public bool IsRunning { get; private set; } = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        playerStats = GetComponent<PlayerStats>();
        characterCombat = GetComponent<CharacterCombat>();
    }

    void FixedUpdate()
    {
        if (inventoryPanel.activeSelf || animator.GetCurrentAnimatorStateInfo(0).IsName("Pickup"))
            return;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Move(h, v);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isInteracting)
        {
            interactable = other.gameObject.GetComponent<Interactable>();
            if (interactable != null)
            {
                interactable.ShowInfo();
                isInteracting = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (isInteracting)
        {
            interactable.CloseInfo();
            isInteracting = false;
        }
    }

    void Move(float h, float v)
    {
        Vector3 camForward_Dir = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 move = v * camForward_Dir + h * Camera.main.transform.right;
        if (move.magnitude > 1f) move.Normalize();
        move = transform.InverseTransformDirection(move);
        float turnAmount = Mathf.Atan2(move.x, move.z);

        transform.Rotate(0, turnAmount * rotationSpeed * Time.deltaTime, 0);

        if (characterController.isGrounded)
        {
            movementDirection = transform.forward * move.magnitude;
            movementDirection *= speed;

            if (Input.GetButton("Jump"))
            {
                animator.SetBool("is_in_air", true);
                movementDirection.y = jumpSpeed;

            }
            else
            {
                animator.SetBool("is_in_air", false);
                animator.SetBool("run", move.magnitude > 0);
            }
        }

        movementDirection.y -= gravity * Time.deltaTime;
        characterController.Move(movementDirection * Time.deltaTime);
    }

    public void ToggleInventoryPanel()
    {
        if (!inventoryPanel.activeSelf)
        {
            inventoryPanel.SetActive(true);
        } else
        {
            inventoryPanel.SetActive(false);
        }
    }

    public void Pickup()
    {
        if (isInteracting &&
            interactable.interactableType == InteractableType.DroppedItem)
        {
            animator.SetTrigger("tr_pickup");
            interactable.Interact();
            interactable.CloseInfo();
            isInteracting = false;
        }
    }

    public void Act()
    {
        if (characterCombat.Attackable())
        {
            animator.SetTrigger("attack_1");
            if (isInteracting &&
                (interactable.interactableType == InteractableType.Resource || interactable.interactableType == InteractableType.Enemy))
            {
                interactable.Interact();
            }
            characterCombat.ResetCooldown();
        }
    }
}
