﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
public class play : MonoBehaviour
{
    // Start is called before the first frame update
    private VideoPlayer videoPlayer;
    public VideoClip[] videoClips;
    private int currentClipIndex;

    public GameObject seaDialog;
    public GameObject skipDialog;

    void Start()
    {
        skipDialog.SetActive(true);
        videoPlayer = this.GetComponent<VideoPlayer>();
        currentClipIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (videoPlayer.isPlaying && Input.GetKeyDown (KeyCode.S))    
        {
            skipDialog.SetActive(false);
            //Debug.Log("您按下了S键"); 
            videoPlayer.Stop();
        }    
        if (!videoPlayer.isPlaying && seaDialog.activeSelf && Input.GetKeyDown (KeyCode.E))    
        {
            skipDialog.SetActive(true);
            currentClipIndex++;
            currentClipIndex = currentClipIndex % videoClips.Length;
            videoPlayer.clip = videoClips[currentClipIndex];
            videoPlayer.Play();
        }
        if (currentClipIndex == 1 && Input.GetKeyDown(KeyCode.S))
        {
            skipDialog.SetActive(true);
            currentClipIndex++;
            currentClipIndex = currentClipIndex % videoClips.Length;
            videoPlayer.clip = videoClips[currentClipIndex];
            videoPlayer.Play();
        }
    }
}