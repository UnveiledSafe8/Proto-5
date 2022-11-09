using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targets : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameManager gameManager;
    public ParticleSystem explosionParticle;
    [SerializeField] private float minForce = 15;
    [SerializeField] private float maxForce = 25;
    [SerializeField] private float maxTorque = 15;
    [SerializeField] private float xRange = 6;
    [SerializeField] private float ypos = -6;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerRb.AddForce(RandomForce(), ForceMode.Impulse);
        playerRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque());
        transform.position = RandomPos();
    }
    private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minForce, maxForce);
    }
    private float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }
    private Vector3 RandomPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ypos);
    }
    private void OnMouseDown()
    {
        if (gameManager.isGameActive)
        {
            if (gameObject.CompareTag("Bomb"))
            {
                gameManager.ScoreUpdate(-10);
            }
            if (gameObject.CompareTag("Pear"))
            {
                gameManager.ScoreUpdate(5);
            }
            if (gameObject.CompareTag("RedApple"))
            {
                gameManager.ScoreUpdate(10);
            }
            if (gameObject.CompareTag("GreenApple"))
            {
                gameManager.ScoreUpdate(15);
            }
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bomb"))
        {
            gameManager.GameEnd();
        }
    }
}
