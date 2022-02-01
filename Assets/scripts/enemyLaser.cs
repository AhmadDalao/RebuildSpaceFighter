using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyLaser : MonoBehaviour
{
    private float _moveSpeed = 8f;
    private playerMovement _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovement>();
        if (_player == null)
        {
            Debug.Log("enemyLaser.cs::==>>> playerMovement is missing");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _moveSpeed);
        // if the laser is below -7f AKA ( off the screen) destroy the laser
        if (transform.position.y < -7f)
        {
            // if the laser has parent destroy it and destroy the parent.
            Destroy(this.gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("enemyLaser.cs::==>>> enemy laser hit the player");
            // damage the player
            _player.playerTakeDamage();
            Destroy(this.gameObject);
        }
    }

}
