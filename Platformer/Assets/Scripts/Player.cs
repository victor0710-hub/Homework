using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class Player : MonoBehaviour
{
    public Transform LifeBar;
    public float movementSpeed = 5.0f;
    public float rotationSpeed = 100.0f;
    public float jumpHeight = 5.0f;
    [SerializeField] public Life LifeData;
    public TextMeshProUGUI Points;

    private Animator animator;
    private Rigidbody rb;
    private bool isGrounded;
    private int counter = 0;

    private float blendTreeSpeedMax = 0.5f;
    private bool isTakingDamage = false;  // Flag to control the coroutine

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            animator = GetComponent<Animator>(); // Ensure Animator is attached
        }

        rb = GetComponent<Rigidbody>(); // Ensure there's a Rigidbody component attached

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Points.text = "Coins: " + counter.ToString();
    }

    void Update()
    {
        MovePlayer();
        RotatePlayer();
        HandleJump();
    }

    void MovePlayer()
    {
        float moveInput = Input.GetAxis("Vertical");
        float moveSpeed = moveInput * movementSpeed * Time.deltaTime;

        transform.Translate(Vector3.forward * moveSpeed);

        float animationSpeed = Mathf.Clamp(moveInput, 0, blendTreeSpeedMax);
        animator.SetFloat("Speed", animationSpeed);
    }

    void RotatePlayer()
    {
        float rotateInput = Input.GetAxis("Mouse X");
        float rotationAmount = rotateInput * rotationSpeed * Time.deltaTime;

        transform.Rotate(Vector3.up * rotationAmount);
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            animator.SetTrigger("Jump");
            isGrounded = false; // Reset grounded status
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Damage")
        {
            if (LifeData.currentHealth > 0 && !isTakingDamage)
            {
                isTakingDamage = true;
                StartCoroutine(StartTakingDamage());
            }
        }
    }

    private IEnumerator StartTakingDamage()
    {
        while (isTakingDamage)  // Keep applying damage while the flag is true
        {
            yield return new WaitForSeconds(2);

            LifeData.TakeDamage(2);
            Vector3 newScale = new Vector3(LifeBar.localScale.x - 0.003f, LifeBar.localScale.y, LifeBar.localScale.z);
            LifeBar.localScale = newScale;

            if (LifeData.currentHealth <= 0)
            {
                isTakingDamage = false;  // Stop taking damage if health reaches 0
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Damage")
        {
            Debug.Log("Out");
            isTakingDamage = false;  // Set the flag to false to stop the coroutine
        }
    }
}
