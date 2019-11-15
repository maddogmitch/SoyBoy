﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public AudioClip goalClip;

    void OnCollisionEnter2D(Collision2D collission)
    {
        if (collission.gameObject.tag == "Player")
        {
            var audioSource = GetComponent<AudioSource>();
            if (audioSource != null && goalClip != null)
            {
                audioSource.PlayOneShot(goalClip);
            }
            GameManager.instance.RestartLevel(1f);

            var timer = FindObjectOfType<Timer>();
            GameManager.instance.SaveTime(timer.time);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
