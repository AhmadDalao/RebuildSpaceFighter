using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    [SerializeField] private float _moveSpeed = 8f;
    [SerializeField] private GameObject _myLaserPrefab;
    [SerializeField] private spawnManager _spawnManager;
    [SerializeField] private UIManager _UIManager;
    [SerializeField] private AudioManager _AudioManager;
    [SerializeField] private GameObject _playerShield;
    [SerializeField] private GameObject _tripleShotPowerUp;
    [SerializeField] private gameManager _gameManager;
    [SerializeField] private GameObject _settingScreen;
    private float _horizontalMovement;
    private float _verticalMovement;
    private float _lastFire;
    private float _fireRate = 0.25f;
    private int _score = 0;
    private int _playerLives = 3;
    private bool _speedUpActice = false;
    private bool _isShieldActice = false;
    private bool _isTripleShotActive = false;
    private bool _isDoubleScoreActive = false;
    private int _playerScoreDoubledBy;
    private bool _isSlowMotion = false;
    [SerializeField] private SpriteRenderer _playerSpriteRender;
    [SerializeField] private Sprite _playerSprite;
    [SerializeField] private Sprite _playerSpriteLeft;
    [SerializeField] private Sprite _playerSpriteRight;

    // Start is called before the first frame update
    void Start()
    {
        // position the player at the specified starting position once the game run.
        transform.Translate(new Vector3(0, -3f, 0));

        // null checking for the UI manager
        if (_UIManager != null)
        {
            _UIManager.gameObject.GetComponent<UIManager>();
        }
        else
        {
            Debug.Log("playerMovement.cs UIManager is missing");
        }

        // null checking for the spawn manager
        if (_spawnManager != null)
        {
            _spawnManager.gameObject.GetComponent<spawnManager>();
        }
        else
        {
            Debug.Log("playerMovement.cs _spawnManager is missing");
        }

        // null check audio manager
        if (_AudioManager != null)
        {
            _AudioManager.gameObject.GetComponent<AudioManager>();
        }
        else
        {
            Debug.Log("playerMovement.cs AudioManager is missing");
        }

        // null check game manager.
        if (_gameManager != null)
        {
            _gameManager.gameObject.GetComponent<gameManager>();
        }
        else
        {
            Debug.Log("playerMovement.cs gameManager is missing");
        }
    }

    // Update is called once per frame
    void Update()
    {
        playerMove();
        // the space key will shot the laser
        // after adding the cooldown system to prevent the player from spamming.
        // checking time scale will prevent player from shooting while game is paused
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _lastFire && Time.timeScale == 1)
        {
            _lastFire = _fireRate + Time.time;

            // play laser sound.
            _AudioManager.laserSound();
            laserFire();
        }

        // teleport the player to help him dodge the enemies.
        // Teleport to the **-- Right --**
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = new Vector3(transform.position.x + 3f, transform.position.y, 0);
        }

        // teleport the player to help him dodge the enemies.
        // Teleport to the **-- Left --**
        if (Input.GetKeyDown(KeyCode.E))
        {
            transform.position = new Vector3(transform.position.x - 3f, transform.position.y, 0);
        }

        // show the pause menu screen.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _gameManager.displayPauseScreen();
            _UIManager.settingScreenClose();
        }

        // use backspace button to close the settings screen. if it's active
        if (Input.GetKeyDown(KeyCode.Backspace) && _settingScreen.activeInHierarchy)
        {
            _UIManager.settingScreenClose();
        }

        // there is a better way to do this
        if (Input.GetKeyDown(KeyCode.F))
        {
            _isSlowMotion = true;
            StartCoroutine(slowMotion());
            Debug.Log("slow motion started");
        }

        // stop slow motion once player un-hold F button
        if (Input.GetKeyUp(KeyCode.F))
        {
            Time.timeScale = 1f;
            _moveSpeed = 8f;
            Debug.Log("slow motion ended");
        }

    }
    private IEnumerator slowMotion()
    {
        while (_isSlowMotion)
        {
            _moveSpeed = 16f;
            Time.timeScale = 0.5f;
            yield return null;
            _isSlowMotion = false;
        }
    }

    private void playerMove()
    {
        // movement taken from the player using GetAxis
        _horizontalMovement = Input.GetAxis("Horizontal");
        _verticalMovement = Input.GetAxis("Vertical");
        // multiplay the number with the speed and deltaTime to make the moveSpeed 1M per second
        Vector3 movement = new Vector3(_horizontalMovement * _moveSpeed * Time.deltaTime, _verticalMovement * _moveSpeed * Time.deltaTime, 0);
        // move the player
        transform.Translate(movement);

        // prevent the player from moving of the screen from the Y axis
        if (transform.position.y < -3.5f)
        {
            transform.position = new Vector3(transform.position.x, -3.5f, 0);
        }
        if (transform.position.y > 5f)
        {
            transform.position = new Vector3(transform.position.x, 5f, 0);
        }

        // teleport the player using the X axis 

        if (transform.position.x < -12f)
        {
            transform.position = new Vector3(12f, transform.position.y, 0);
        }

        if (transform.position.x > 12f)
        {
            transform.position = new Vector3(-12f, transform.position.y, 0);
        }

        // change the sprit to the left sprit to give the imprison of player moving to the left.
        // Also return the original  sprite once button is up ( ideal )
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _playerSpriteRender.sprite = _playerSpriteLeft;
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            _playerSpriteRender.sprite = _playerSprite;
        }

        // change the sprit to the right sprit to give the imprison of player moving to the right.
        // Also return the original  sprite once button is up ( ideal )
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            _playerSpriteRender.sprite = _playerSpriteRight;
        }

        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            _playerSpriteRender.sprite = _playerSprite;
        }
    }

    private void laserFire()
    {
        if (_isTripleShotActive)
        {
            Vector3 laserPosition = new Vector3(transform.position.x, transform.position.y + 1.2f, 0);
            Instantiate(_tripleShotPowerUp, laserPosition, Quaternion.identity);
        }
        else
        {
            Vector3 laserPosition = new Vector3(transform.position.x, transform.position.y + 1.2f, 0);
            Instantiate(_myLaserPrefab, laserPosition, Quaternion.identity);
        }
    }

    public void playerScore(int doubledBy)
    {
        if (_isDoubleScoreActive)
        {
            _score += doubledBy;
            Debug.Log(" the score is doubled now ==>>> " + _score);
        }
        else
        {
            _score += 1;
        }
        // this function will check player score and add extra life if it hits the target
        playerGainLife();
        Debug.Log("score:: ===>>> " + _score);
    }

    private void playerGainLife()
    {
        if (currentScore() >= 50)
        {
            if (_playerLives < 3)
            {
                _playerLives++;
                _score = 0;
            }
        }
    }

    public int currentScore()
    {
        return _score;
    }

    public void playerTakeDamage()
    {
        if (!_isShieldActice)
        {
            // player lose 1 live per damage taken
            _playerLives--;
            // player hurt sound
            _AudioManager.playerTakingDamageSound();
            // player loses 10 points upon taking damage..
            playerDeductScore();
            Debug.Log("number of lives is::=== " + _playerLives);
            if (_playerLives == 0)
            {
                _UIManager.gameOverScreen();
                // destroy enemies and power-ups since player is dead.
                _spawnManager.playerDead();
                //  play explosion sound on player death.
                _AudioManager.explosionSound();
                _AudioManager.stopBackgroundMusic();
                // destroy the player game object.
                Destroy(this.gameObject);
            }
        }
        else
        {
            _isShieldActice = false;
            _playerShield.SetActive(false);
        }
    }

    private void playerDeductScore()
    {
        if (_score != 0)
        {
            _score -= 1;
        }
        else
        {
            _score = 0;
        }
    }

    public int numberOfLives()
    {
        return _playerLives;
    }

    public void moveSpeedPowerUp()
    {
        _speedUpActice = true;
        StartCoroutine(speedCoroutine());
    }

    private IEnumerator speedCoroutine()
    {
        while (_speedUpActice)
        {
            _moveSpeed = _moveSpeed * 2f;
            yield return new WaitForSeconds(6f);
            _speedUpActice = false;
            _moveSpeed = 8f;
        }
    }

    public void shieldPowerUp()
    {
        _isShieldActice = true;
        _playerShield.SetActive(true);
    }

    public void tripleShotPowerUp()
    {
        _isTripleShotActive = true;
        StartCoroutine(tripleShotCoroutine());
    }

    private IEnumerator tripleShotCoroutine()
    {
        while (_isTripleShotActive)
        {
            yield return new WaitForSeconds(5f);
            _isTripleShotActive = false;
        }
    }

    public void addHealthPowerUp()
    {
        if (_playerLives != 3)
        {
            _playerLives++;
            Debug.Log("number of lives is::=== " + _playerLives);
        }
    }

    public void doublePlayerScore(int doubledBy)
    {
        _playerScoreDoubledBy = doubledBy;
        _isDoubleScoreActive = true;
        _UIManager.doubleScoreTextActive(doubledBy);
        StartCoroutine(doublePlayerScoreRoutine());
    }

    public int playerScoreIsDoubledBy()
    {
        return _playerScoreDoubledBy;
    }

    private IEnumerator doublePlayerScoreRoutine()
    {
        while (_isDoubleScoreActive)
        {
            yield return new WaitForSeconds(10f);
            _isDoubleScoreActive = false;
            _UIManager.doubleScoreTextDisabled();
        }
    }

}
