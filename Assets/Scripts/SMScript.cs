using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SMScript : MonoBehaviour
{
    public GameObject leftFishPrefab;
    public GameObject rightFishPrefab;
    public GameObject jellyfishPrefab;
    System.Random rnd = new System.Random();
    public bool playing;
    public Text scoreBoard;
    public int score;
    public double fishSpawnPercent; // Percent chance a fish spawns on a given fixed update
    double _originalFishSpawnRate;
    public double jellyfishSpawnPercent; // Percent chance a jellyfish spawns on a given fixed update
    double _originalJellyFishSpawnRate;
    public double minFishSize;
    public double maxFishSize;
    double _originalMaxFishSize;

    float _startTime = -1;
    double _secsElapsed;
    // Start is called before the first frame update
    void Start()
    {
        playing = true;
        score = 0;
        _startTime = Time.time;
        _originalJellyFishSpawnRate = jellyfishSpawnPercent;
        _originalMaxFishSize = maxFishSize;
        _originalFishSpawnRate = fishSpawnPercent;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("TitleScene");
        }

        if (!playing)
        {
            scoreBoard.text = "Score: " + score + "\nGAME OVER\npress ESC to quit";
            return;
        }

        _secsElapsed = Time.time - _startTime;
        maxFishSize = _secsElapsed / 180 + _originalMaxFishSize; // Max Fish Size increases as time passes

        if (jellyfishSpawnPercent < 2.0)
        {
            jellyfishSpawnPercent = _originalJellyFishSpawnRate + _secsElapsed/120; // Jellyfish spawn odds increase as time goes on
        }

        if (fishSpawnPercent < 2.0)
        {
            fishSpawnPercent = _originalFishSpawnRate + _secsElapsed/240;
        }

        scoreBoard.text = "Score: " + score;
    }

    private void FixedUpdate()
    {
        spawnFish();
        spawnJellyfish();
    }

    private void spawnFish()
    {
        double rndFrame = rnd.NextDouble() * 100;
        float rndSize = (float)(rnd.NextDouble() * maxFishSize + minFishSize);
        float rndY = (float)(rnd.NextDouble() * 10 - 5);
        float rndVel = (float)(rnd.NextDouble() * 1.7 + 0.7);
        if (rndFrame < fishSpawnPercent)
        {
            GameObject _gameObject = Instantiate(rightFishPrefab, new Vector2(-13, rndY), Quaternion.identity);
            _gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(rndVel, 0);
            _gameObject.GetComponent<Transform>().localScale = new Vector3(rndSize, rndSize, rndSize);
        }
        else if (rndFrame > (100 - fishSpawnPercent))
        {
            GameObject _gameObject = Instantiate(leftFishPrefab, new Vector2(13, rndY), Quaternion.identity);
            _gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-rndVel, 0);
            _gameObject.GetComponent<Transform>().localScale = new Vector3(rndSize, rndSize, rndSize);
        } 
        // Guranteed spawning of little fish
        else if (rndFrame > fishSpawnPercent && rndFrame < 1.3 * fishSpawnPercent) {
            GameObject _gameObject = Instantiate(rightFishPrefab, new Vector2(-13, rndY), Quaternion.identity);
            _gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(rndVel, 0);
            _gameObject.GetComponent<Transform>().localScale = new Vector3(0.15f, 0.15f, 0.15f);
        }
        else if (rndFrame > 1.3 * fishSpawnPercent && rndFrame < 1.6 * fishSpawnPercent)
        {
            GameObject _gameObject = Instantiate(rightFishPrefab, new Vector2(-13, rndY), Quaternion.identity);
            _gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(rndVel, 0);
            _gameObject.GetComponent<Transform>().localScale = new Vector3(0.15f, 0.15f, 0.15f);
        }
    }

    private void spawnJellyfish()
    {
        double rndFrame = rnd.NextDouble() * 100;
        float rndX = (float)(rnd.NextDouble() * 22 - 11);
        if (rndFrame < jellyfishSpawnPercent)
        {
            GameObject _gameObject = Instantiate(jellyfishPrefab, new Vector2(rndX, -8), Quaternion.identity);
            _gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1);
        }
    }
}
