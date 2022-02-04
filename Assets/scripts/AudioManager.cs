using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip _laserSound;
    [SerializeField] private AudioClip _explosionSound;
    [SerializeField] private AudioClip _powerUpSound;
    [SerializeField] private AudioClip _playerHurtSound;
    [SerializeField] private GameObject _backgroundMusic;
    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // laser sound function
    public void laserSound()
    {
        _audioSource.clip = _laserSound;
        _audioSource.Play();
    }

    // explosion sound function
    public void explosionSound()
    {
        _audioSource.clip = _explosionSound;
        _audioSource.Play();
    }

    public void powerUpSound()
    {
        _audioSource.clip = _powerUpSound;
        _audioSource.Play();
    }

    public void stopBackgroundMusic()
    {
        _backgroundMusic.GetComponent<AudioSource>().Stop();
    }

    public void playerTakingDamageSound()
    {
        _audioSource.clip = _playerHurtSound;
        _audioSource.Play();
    }

    public void pauseBackgroundMusic()
    {
        _backgroundMusic.GetComponent<AudioSource>().Pause();
    }

    public void playBackgroundMusic()
    {
        _backgroundMusic.GetComponent<AudioSource>().Play();
    }

}
