using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private float _rotatingAngle = -0.5f;
    [SerializeField] private gameManager _gameManager;
    [SerializeField] private AudioManager _audioManager;

    // Start is called before the first frame update
    void Start()
    {
        // get the audio manager and null check it.
        _audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        if (_audioManager == null)
        {
            Debug.Log("Asteroid.cs::==>>> AudioManager is missing");
        }

        // get the game manager and null check it.
        _gameManager = GameObject.Find("gameManager").GetComponent<gameManager>();
        if (_gameManager == null)
        {
            Debug.Log("Asteroid.cs::==>>> gameManager is missing");
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 0f, _rotatingAngle, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "laser")
        {
            _gameManager.hasGameStarted();
            explosionParticle();
            Destroy(other.gameObject);
        }
    }

    private void explosionParticle()
    {
        // stop rotation.
        transform.Rotate(0f, 0f, 0f, Space.Self);
        // play explosion sound
        _audioManager.explosionSound();
        // find the explosion particle, << it is a child of asteroid >>
        Transform explosionPart = this.gameObject.transform.GetChild(0);
        // set the particle to active.
        explosionPart.gameObject.SetActive(true);
        // destroy the asteroid 
        Destroy(this.gameObject, 1.5f);
    }

}
