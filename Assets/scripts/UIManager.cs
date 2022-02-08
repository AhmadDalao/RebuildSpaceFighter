using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private GameObject _doubleScoreText;
    [SerializeField] private Text _gameOverScore;
    [SerializeField] private GameObject _gameOverText;
    [SerializeField] private playerMovement _player;
    [SerializeField] private Sprite[] _livesSprits;
    [SerializeField] private SpriteRenderer _spriteRender;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _settingScreen;
    [SerializeField] private Button _settingsButtonPauseScreen;
    [SerializeField] private Button _backButtonSettingScreen;
    [SerializeField] private GameObject _pauseScreen;
    [SerializeField] private gameManager _gameManager;
    [SerializeField] private Button _playAgainButton;
    [SerializeField] private Button _quitGameButton;
    [SerializeField] private Button _quitGameFromPause;
    [SerializeField] private Button _restartButtonFromPause;
    [SerializeField] private Button _resumeButtonFromPause;
    [SerializeField] private Button _returnToMainMenuButtton;
    [SerializeField] private Button _returnToMainMenuGameOver;
    private bool _isGameOver = false;
    private Button _playButtonFromMenu;

    // Start is called before the first frame update
    void Start()
    {
        // null check _player.
        if (_player == null)
        {
            Debug.Log("UIManager.cs :: ====>>>> playerMovement is missing");
        }
        else
        {
            _player.gameObject.GetComponent<playerMovement>();
        }

        // null check _spriteRender.
        if (_spriteRender == null)
        {
            Debug.Log("UIManager.cs :: ====>>>> spriteRender is missing");
        }
        else
        {
            _spriteRender.gameObject.GetComponent<SpriteRenderer>();
        }

        // null check UIManager.
        if (_gameManager == null)
        {
            Debug.Log("UIManager.cs ::==>> gameManager is missing");
        }
        else
        {
            _gameManager.gameObject.GetComponent<gameManager>();
        }

        // attach listener to the settings button on pause screen.
        _settingsButtonPauseScreen.GetComponent<Button>().onClick.AddListener(settingScreen);

        // attach listener to the settings button on settings screen.
        _backButtonSettingScreen.GetComponent<Button>().onClick.AddListener(settingScreenClose);

        // attach listener to play agin button from game over screen.
        _playAgainButton.GetComponent<Button>().onClick.AddListener(_gameManager.playAgainButton);

        // attach listener to quit game button from game over screen.
        _quitGameButton.GetComponent<Button>().onClick.AddListener(gameManager.quitGameButton);

        // attach listener to quit game button from paused game screen.
        _quitGameFromPause.GetComponent<Button>().onClick.AddListener(gameManager.quitGameButton);

        // attach listener to restart game button from paused game screen.
        _restartButtonFromPause.GetComponent<Button>().onClick.AddListener(_gameManager.playAgainButton);

        // attach listener to resume game button from paused game screen.
        _resumeButtonFromPause.GetComponent<Button>().onClick.AddListener(_gameManager.displayPauseScreen);

        // attack listener to return to menu button from pasused game screen.
        _returnToMainMenuButtton.GetComponent<Button>().onClick.AddListener(_gameManager.returnToMenu);

        // attach listener to return to menu button from game over screen.
        _returnToMainMenuGameOver.GetComponent<Button>().onClick.AddListener(_gameManager.returnToMenu);
    }

    // Update is called once per frame
    void Update()
    {
        // change the player score on the screen.
        _scoreText.text = "Score: " + _player.currentScore().ToString();
        // change the sprite image based on the player's lives remaining.
        _spriteRender.sprite = _livesSprits[_player.numberOfLives()];
    }

    public void gameOverScreen()
    {
        _isGameOver = true;
        StartCoroutine(gameOverFlicker());
        _gameOverPanel.SetActive(true);
        _gameOverScore.text = "Score: " + _player.currentScore().ToString();
    }

    private IEnumerator gameOverFlicker()
    {
        while (_isGameOver)
        {
            _gameOverText.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            _gameOverText.SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void doubleScoreTextActive(int doubledBy)
    {
        _doubleScoreText.SetActive(true);
        _doubleScoreText.gameObject.GetComponent<Text>().text = "Score " + doubledBy.ToString() + "x";
    }

    public void doubleScoreTextDisabled()
    {
        _doubleScoreText.SetActive(false);
    }

    private void settingScreen()
    {
        _settingScreen.SetActive(true);
    }

    public void settingScreenClose()
    {
        _settingScreen.SetActive(false);
    }

    public void pauseScreenActice()
    {
        _pauseScreen.SetActive(true);
    }

    public void pauseScreenDisabled()
    {
        _pauseScreen.SetActive(false);
    }

    public bool isPauseActiveInHierarchy()
    {
        return _pauseScreen.activeInHierarchy;
    }
}
