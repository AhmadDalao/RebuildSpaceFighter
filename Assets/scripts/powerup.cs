using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerup : MonoBehaviour
{
    // Start is called before the first frame update
    private float _moveSpeed = 5f;
    [SerializeField] private int powerUpNumber;
    private playerMovement _player;
    private AudioManager _audioManager;
    void Start()
    {
        // null checking player object.
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovement>();
        if (_player == null)
        {
            Debug.Log("powerup.cs missing player reference");
        }
        // null checking audio manager.
        _audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        if (_audioManager == null)
        {
            Debug.Log("powerup.cs::==>>> AudioManager is missing");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _moveSpeed * Time.deltaTime);
        // if the power up leaves the screen destroy it. and spawn it later randomly
        // using the spawnManager.
        if (transform.position.y < -6f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // power up sound on pick up.
            _audioManager.powerUpSound();
            Destroy(this.gameObject);
            switch (powerUpNumber)
            {
                case 0:
                    _player.moveSpeedPowerUp();
                    break;
                case 1:
                    _player.shieldPowerUp();
                    break;
                case 2:
                    _player.tripleShotPowerUp();
                    break;
                case 3:
                    _player.addHealthPowerUp();
                    break;
                case 4:
                    _player.doublePlayerScore(Random.Range(1, 11));
                    break;
                default:
                    Debug.Log("no powerUp detected");
                    break;
            }
        }
    }
}
