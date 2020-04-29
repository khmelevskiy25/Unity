using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float force;

    private Rigidbody2D body;

    [SerializeField] private int points;

    private bool isDead = false;

    public bool IsDead => isDead;

    private Animator animator;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip flapSound;

    [SerializeField]
    private AudioClip getPointsSound;

    [SerializeField]
    private AudioClip deathSound;

    private ScoreDisplayer displayer;

    public int Points => points;

    private void Start()
    {
        isDead = false;
        displayer = FindObjectOfType<ScoreDisplayer>();
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        displayer.GetComponent<Text>().text = "0";
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isDead)
        {
            var vel = body.velocity;
            vel.y = 0;
            body.velocity = vel;

            body.AddForce(transform.up * force, ForceMode2D.Impulse);
            //birdFly.Play();

            audioSource.PlayOneShot(flapSound);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Вы Проиграли!");
        animator.Play("Death");
        Destroy(gameObject, .5f);
        isDead = true;

        audioSource.PlayOneShot(deathSound);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isDead)
            return;

        audioSource.PlayOneShot(getPointsSound);
        points++;
        displayer.GetComponent<Text>().text = points.ToString();
    }

}
