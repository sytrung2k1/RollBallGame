using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Text scoreText;
    public Text winText;
    public Material[] pickUpMaterials;
    public GameObject panel;

    private Rigidbody rb;
    private int score;
    private int currentMaterialIndex;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        score = 0;
        SetScoreText();
        winText.text = "";
        currentMaterialIndex = Random.Range(0, pickUpMaterials.Length);
        ChangePlayerColor();
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pick Up"))
        {
            PickUpController pickUp = other.GetComponent<PickUpController>();
            if(pickUp != null)
            {
                if(pickUp.pickupColorIndex == currentMaterialIndex)
                {
                    IncreaseScore();
                    StartCoroutine(pickUp.RespawnPickUp());
                    ChangePlayerColor();
                }
                else
                {
                    Invoke("CallMenu", 3f);
                    panel.SetActive(true);
                    GameOver();
                }
            }
        }
    }

    void IncreaseScore()
    {
        currentMaterialIndex = Random.Range(0, pickUpMaterials.Length);
        ChangePlayerColor();
        score++;
        SetScoreText();
        if (score >= 10)
        {
            panel.SetActive(true);
            Win();
            Invoke("CallMenu", 3f);
        }
    }

    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    void Win()
    {
        // Hiển thị thông báo Game Over
        winText.text = "Win";

        // Vô hiệu hóa player
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    void GameOver()
    {
        // Hiển thị thông báo Game Over
        winText.text = "Game Over";

        // Vô hiệu hóa player
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    void ChangePlayerColor()
    {
        Renderer rend = GetComponent<Renderer>();
        rend.material = pickUpMaterials[currentMaterialIndex]; // Thay đổi Material của Player
    }

    void CallMenu()
    {
        GameManager.gm.EndGame();
    }

}
