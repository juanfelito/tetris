using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public static SoundManager Instance = null;

    public AudioClip GameOver;
    public AudioClip ShapeMove;
    public AudioClip ShapeStop;
    public AudioClip RowDelete;
    public AudioClip ShapeRotate;
    public AudioClip Tetris;

    private AudioSource sourceComp;

    void Start() {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }

        sourceComp = GetComponent<AudioSource>();
    }

    public void PlayASound(AudioClip clip) {
        sourceComp.PlayOneShot(clip);
    }
}
