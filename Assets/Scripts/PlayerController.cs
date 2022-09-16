using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class PlayerController : MonoBehaviour
{

    public float speed = 0;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI livesText;
    public GameObject LoseTextObject;
    public GameObject winTextObject;
    // Change win text message!
    private Rigidbody rb;
    private int count;
    private int lives;
    private float movementX;
    private float movementY;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        // Amount of lives off the rip
        rb=GetComponent<Rigidbody>();
        lives = 3;
          
        //win and lose text false and should not tirgger
        SetCountText();
        winTextObject.SetActive(false);

        SetCountText();
        LoseTextObject.SetActive(false);

    }

    void SetCountText()
    {
        countText.text = "Count:" + count.ToString();
        if (count >= 21)
        {
            winTextObject.SetActive(true);
        }

        // if player runs out of lives player is blown up and lose text triggers
        livesText.text = "lives " + lives.ToString();
        if (lives == 0)
        {
            LoseTextObject.SetActive(true);
            Destroy(gameObject);
        }
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
        }

        // When player runs into an enemy
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            lives = lives - 1;
            
            SetCountText();
        }

        // Teleport to new level
        else if (count == 13)
        {
            transform.position = new Vector3(50f, 0.5f, 0f);
        }

    }


}
