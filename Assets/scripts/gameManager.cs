using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    private bool _hasGameStarted = false;
    [SerializeField] private spawnManager _spawnManager;
    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("spawnManager").GetComponent<spawnManager>();
        if (_spawnManager == null)
        {
            Debug.Log("spawnManager.cs::==>> _spawnManager is missing");
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



}
