using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    [SerializeField] private float _moveSpeedEnemy = 3f;
    private playerMovement _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovement>();
        if (_player == null)
        {
            Debug.Log("enemy.cs::==>>> playerMovement is missing");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // move the enemy down
        transform.Translate(Vector3.down * _moveSpeedEnemy * Time.deltaTime);
        // randomly respawn the enemy at different position instead of destroying it.
        if (transform.position.y < -5f)
        {
            transform.position = new Vector3(Random.Range(-9f, 9f), 8f, 0);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "laser")
        {
            // destroy the laser
            Destroy(other.gameObject);
            // add 10 points to the score using script communcation;
            _player.playerScore();
            // destroy the enemy gameObject
            Destroy(this.gameObject);
        }

        if (other.tag == "Player")
        {
            Debug.Log("enemy hit the player");
            // destroy the enemy gameObject.
            Destroy(this.gameObject);
            // damage the player
            _player.playerTakeDamage();
        }

    }

}
