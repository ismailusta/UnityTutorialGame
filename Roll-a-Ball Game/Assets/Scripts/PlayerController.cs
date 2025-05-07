using UnityEngine;
using UnityEngine.InputSystem; // Ensure you have the Input System package installed
using TMPro; // Ensure you have the TextMeshPro package installed

public class PlayerController : MonoBehaviour
{
    public float speed; // Speed of the player
    private Rigidbody rb;
    private int count; // Counter for the number of collectibles collected
    public TextMeshProUGUI countText;
    public GameObject winText; // Reference to the UI Text element to display the count
    private float movementX;
    private float movementY;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0; // Initialize the counter to 0
        SetCountText();
        winText.SetActive(false); // Hide the win text at the start
    }
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp")) // Check if the collided object has the "Collectible" tag
        {
            Debug.Log("Collected: " + other.gameObject.name); // Log the name of the collected object
            other.gameObject.SetActive(false); // Deactivate the collected object
            count++; // Increment the counter
            SetCountText();
        }
    }
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString(); // Update the UI Text element with the current count
        if (count >= 12) // Check if the player has collected all collectibles
        {
            winText.SetActive(true); // Show the win text
            Destroy(GameObject.FindGameObjectWithTag("Enemy")); // Destroy the enemy object
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject); // Destroy the player object if it collides with an enemy
            winText.SetActive(true); // Show the win text if the player is destroyed
            winText.GetComponent<TextMeshProUGUI>().text = "You Lose!"; // Update the win text to indicate game over
        }
    }
}
