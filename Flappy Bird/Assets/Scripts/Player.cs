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

    private AudioSource BirdFly;

    private ScoreDisplayer displayer;

    public int Points => points;

    private void Start()
    {
        isDead = false;
        displayer = FindObjectOfType<ScoreDisplayer>();
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        BirdFly = GetComponent<AudioSource>();
        displayer.GetComponent<Text>().text = "0";
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && !isDead)
        {
            var vel = body.velocity;
            vel.y = 0;
            body.velocity = vel;

            body.AddForce(transform.up * force, ForceMode2D.Impulse);
            BirdFly.Play();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Вы Проиграли!");
        animator.Play("Death");
        Destroy(gameObject, .5f);

        isDead = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isDead)
            return;

        points++;
        displayer.GetComponent<Text>().text = points.ToString();
        Debug.Log(points);
    }

}
