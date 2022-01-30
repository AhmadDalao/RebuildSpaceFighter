using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject[] _powerups;
    private bool _isPlayerAlive = true;

    void Start()
    {
        StartCoroutine(spawnEnemy());
        StartCoroutine(spawnPowerUp());
        StartCoroutine(spawnHealth());
    }

    // Update is called once per frame
    void Update()
    {

    }

    // spawn the enemy on the map.
    private IEnumerator spawnEnemy()
    {
        while (_isPlayerAlive)
        {
            Vector3 enemyPosition = new Vector3(Random.Range(-9f, 9f), 8f, 0);
            Instantiate(_enemyPrefab, enemyPosition, Quaternion.identity);
            yield return new WaitForSeconds(5f);
        }
    }

    public void playerDead()
    {
        _isPlayerAlive = false;
        destroyEnemies();
        destroyPowerUps();
    }

    private void destroyEnemies()
    {
        GameObject[] _enemies = GameObject.FindGameObjectsWithTag("enemy");
        foreach (var enemy in _enemies)
        {
            Destroy(enemy.gameObject);
        }
    }

    private void destroyPowerUps()
    {
        GameObject[] _powerUps = GameObject.FindGameObjectsWithTag("powerups");
        foreach (var powerUp in _powerUps)
        {
            Destroy(powerUp.gameObject);
        }
    }

    // spawn the speed , tripleShot and shield power-ups
    private IEnumerator spawnPowerUp()
    {
        while (_isPlayerAlive)
        {
            Vector3 powerUpPosition = new Vector3(Random.Range(-9f, 9f), 8f, 0);
            int randomPowerUp = (int)Random.Range(0f, 4f);
            Debug.Log("the power up picked is " + randomPowerUp);
            Instantiate(_powerups[randomPowerUp], powerUpPosition, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(5f, 10f));
        }
    }

    // spawn the health while player is alive.
    private IEnumerator spawnHealth()
    {
        while (_isPlayerAlive)
        {
            Vector3 healthPosition = new Vector3(Random.Range(-9f, 9f), 8f, 0);
            Instantiate(_powerups[_powerups.Length - 1], healthPosition, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(5f, 10f));
        }
    }

}
