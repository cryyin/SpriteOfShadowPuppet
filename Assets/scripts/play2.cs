using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class play2 : MonoBehaviour
{
    // Start is called before the first frame update
    private VideoPlayer videoPlayer;
    public VideoClip[] videoClips;
    private int currentClipIndex;

    public GameObject seaDialog;
    public GameObject skipDialog;

    void Start()
    {
        skipDialog.SetActive(false);
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
        if (!videoPlayer.isPlaying && Input.GetKeyDown (KeyCode.Z))    
        {
            skipDialog.SetActive(true);
            videoPlayer.clip = videoClips[currentClipIndex];
            videoPlayer.Play();
            
        }
        if(Input.GetKeyDown(KeyCode.U))
        {
            Debug.Log("您按下了U键 "+currentClipIndex); 
        }
    }
}
