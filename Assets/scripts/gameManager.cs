using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    private bool _hasGameStarted = false;
    [SerializeField] private spawnManager _spawnManager;
    [SerializeField] private GameObject _pauseScreen;
    [SerializeField] private AudioManager _AudioManager;
    // Start is called before the first frame update
    void Start()
    {
        // null check spawnManager.
        _spawnManager = GameObject.Find("spawnManager").GetComponent<spawnManager>();
        if (_spawnManager == null)
        {
            Debug.Log("gameManager.cs::==>> _spawnManager is missing");
        }

        // null check audio manager.
        _AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        if (_AudioManager == null)
        {
            Debug.Log("gameManager.cs ::==>> _audioManager is missing");
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    // load the current scene on button click 
    public void playAgainButton()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    // Quit the game on button click.
    public void quitGameButton()
    {
        Application.Quit();
    }


    // play game from main menu 
    public void playGameButton()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void hasGameStarted()
    {
        _spawnManager.gameStarted();
    }

    public void displayPauseScreen()
    {
        if (!_pauseScreen.activeInHierarchy)
        {
            Debug.Log("game has been paused");
            _pauseScreen.SetActive(true);
            _AudioManager.pauseBackgroundMusic();
            pauseGame();
        }
        else
        {
            Debug.Log("game has been unpaused");
            _pauseScreen.SetActive(false);
            _AudioManager.playBackgroundMusic();
            unPauseGame();
        }
    }


    private void pauseGame()
    {
        Time.timeScale = 0;
    }

    private void unPauseGame()
    {
        Time.timeScale = 1;
    }

}
