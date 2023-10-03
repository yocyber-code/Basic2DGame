using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MyScript : MonoBehaviour
{
    private float moveX;
    private Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    private bool isJumping;
    private int score;
    public Text scoreText;

    public AudioClip coinAFX;
    public AudioClip jumpAFX;
    private AudioSource audioSource;

    private void Start()
    {
        speed = Random.Range(10, 16);
        jumpForce = 400f;
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            audioSource.PlayOneShot(jumpAFX);
            rb.AddForce(new Vector2(0f, jumpForce));
        }
    }

    private void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.CompareTag("Floor"))
        {
            isJumping = false;
        }
    }

    private void OnCollisionExit2D(Collision2D target)
    {
        if (target.gameObject.CompareTag("Floor") && !isJumping)
        {
            isJumping = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.CompareTag("Item"))
        {
            Destroy(target.gameObject);
            score++;
            scoreText.text = "Score : " + score.ToString();
            audioSource.PlayOneShot(coinAFX);
        }
        if (target.gameObject.CompareTag("Enemy"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (target.gameObject.CompareTag("Door"))
        {
            SceneManager.LoadScene("Level2");
        }
    }
}
