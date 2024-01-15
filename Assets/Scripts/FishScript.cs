using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishScript : MonoBehaviour
{
    public bool eatable;
    float _fishSize;
    float _playerSize;
    float _startTime = -1;
    double _secsElapsed;
    Rigidbody2D _rbody;
    GameObject _sceneManager;
    Vector2 _speed;

    // Start is called before the first frame update
    void Start()
    {
        _fishSize = GetComponent<Transform>().localScale.x;
        _startTime = Time.time;
        _rbody = GetComponent<Rigidbody2D>();
        _sceneManager = GameObject.Find("SceneManager");
    }

    // Update is called once per frame
    void Update()
    {
        _secsElapsed = Time.time - _startTime;
        float xVel = _rbody.velocity.x;
        // The y velocity is calculated based on the sine of the time elapsed,
        // resulting in a "bobbing" pattern
        float yVel = (float) (Math.Sin(_secsElapsed) * 0.8);
        _speed.x = xVel;
        _speed.y = yVel;

        _rbody.velocity = _speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player")) {
            _playerSize = collision.gameObject.GetComponent<PlayerScript>().size;
            if (_fishSize <= _playerSize)
            {
                _sceneManager.GetComponent<SMScript>().score += (int) (_fishSize * 100);
                collision.gameObject.GetComponent<PlayerScript>().size += 0.04f * _fishSize;
                Destroy(gameObject);
            }
            else
            {
                _sceneManager.GetComponent<SMScript>().playing = false;
                Destroy(collision.gameObject);
            }
        }
    }
}
