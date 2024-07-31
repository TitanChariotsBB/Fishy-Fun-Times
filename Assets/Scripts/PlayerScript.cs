using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D _rbody;
    Transform _transform;
    SpriteRenderer _spriteRenderer;
    public float speed;
    public float size;
    public AudioSource _audioSource;
    public AudioClip _nom;
    public AudioClip _bubble;

    void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        size = 0.25f;
    }

    // Update is called once per frame
    void Update()
    {
        AxisControl();
        _transform.localScale = new Vector3(size, size, size);

        if (Input.anyKeyDown)
        {
            _audioSource.PlayOneShot(_bubble);
        }
    }

    void AxisControl()
    {
        // base direction vector
        float xDir = Input.GetAxis("Horizontal");
        float yDir = Input.GetAxis("Vertical");
        Vector2 direction = (new Vector2(xDir, yDir)).normalized;

        if (xDir > 0)
        {
            _spriteRenderer.flipX = true;
        }
        if (xDir < 0)
        {
            _spriteRenderer.flipX = false;
        }

        // set direction based on direction
        _rbody.velocity = direction.normalized * speed;
        _rbody.rotation = 0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _audioSource.PlayOneShot(_nom);
    }
}
