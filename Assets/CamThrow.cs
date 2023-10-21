using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamThrow : MonoBehaviour
{
    public float throwForce = 10f;
    private bool isGrabbed = false;
    private int score = 0;
    public Text scoreText;

    void Start()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        scoreText.text = "Score: 0";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (!isGrabbed)
            {
                
                if (TryGetComponent<Rigidbody>(out var rb))
                {
                    rb.isKinematic = true;
                    isGrabbed = true;
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.G) && isGrabbed)
        {
            
            if (TryGetComponent<Rigidbody>(out var rb))
            {
                rb.isKinematic = false;
                rb.AddForce(transform.forward * throwForce, ForceMode.Impulse);
                isGrabbed = false;
            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collided with object: {collision.gameObject.name}");
        if (collision.gameObject.name.Equals("Target"))
        {
            Debug.Log($"Old score: {scoreText.text}");

            Debug.Log("Can hit the target!");
            score++;
            scoreText.text = "Score: " + score.ToString();

            Debug.Log($"New score: {scoreText.text}");
        }
    }
}
