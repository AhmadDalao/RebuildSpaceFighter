using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    [SerializeField] private float _moveSpeedEnemy = 3f;
    private playerMovement _player;
    private AudioManager _audioManager;
    [SerializeField] private GameObject _enemyLaser;
    private bool _isEnemyAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        // null check the enemy laser prefab.
        if (_enemyLaser != null)
        {
            // start enemy laser coroutine
            StartCoroutine(enemyLaserCoroutine());
        }
        else
        {
            Debug.Log("enemy.cs::==>>>  enemy laser is missing");
        }

        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovement>();
        if (_player == null)
        {
            Debug.Log("enemy.cs::==>>> playerMovement is missing");
        }

        _audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        if (_audioManager == null)
        {
            Debug.Log("enemy.cs::==>>> AudioManager is missing");
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
        // slow the enemy on impact
        if (other.tag == "laser")
        {
            explosionParticle();
            // destroy the laser
            Destroy(other.gameObject);
            // play explosion sound after killing the enemy.
            _audioManager.explosionSound();
            // add 10 points to the score using script communcation;
            _player.playerScore(_player.playerScoreIsDoubledBy());
            // calling this function will stop the enemy from firing laser.
            enemyHasBeenDestroyed();
            // destroy the enemy gameObject
            Destroy(this.gameObject, 1f);
        }

        if (other.tag == "Player")
        {
            explosionParticle();
            Debug.Log("enemy hit the player");
            // play explosion sound after killing the enemy.
            _audioManager.explosionSound();
            // damage the player
            _player.playerTakeDamage();
            // calling this function will stop the enemy from firing laser.
            enemyHasBeenDestroyed();
            // destroy the enemy gameObject.
            Destroy(this.gameObject, 1f);
        }
    }

    private void explosionParticle()
    {
        _moveSpeedEnemy = 0f;
        // find the particle child and set it to active to display explosion
        Transform explisionParticle = this.gameObject.transform.GetChild(0);
        // set the particle active
        explisionParticle.gameObject.SetActive(true);
        // remove the the collision to prevent damaging the player twice.
        Destroy(this.gameObject.GetComponent<BoxCollider2D>());
    }


    private IEnumerator enemyLaserCoroutine()
    {
        while (_isEnemyAlive)
        {
            Vector3 enemyPosition = new Vector3(transform.position.x, transform.position.y * 0.02f, 0f);
            Instantiate(_enemyLaser, enemyPosition, Quaternion.identity);
            yield return new WaitForSeconds(2f);
        }
    }

    private void enemyHasBeenDestroyed()
    {
        _isEnemyAlive = false;
    }

}
