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
    public Rigidbody2D player;
    void Start()
    {
     //   skipDialog.SetActive(false);
        videoPlayer = this.GetComponent<VideoPlayer>();
        currentClipIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {

        bool flag=false;
        if(player.position.x>=9&&player.position.y>=4)
        {
            flag=true;       
        }
        Debug.Log(flag);
        if (videoPlayer.isPlaying)    
        {
            if(Input.GetKeyDown(KeyCode.S))
            {
                skipDialog.SetActive(false);
                Debug.Log("您按下了S键"); 
                videoPlayer.Stop();
                currentClipIndex++;
               
            }
           
        }    
        if (!videoPlayer.isPlaying &&flag&&currentClipIndex<=1)    
        {
            Debug.Log("ZZZ");
            skipDialog.SetActive(true); 
       //     currentClipIndex = currentClipIndex % videoClips.Length;
            videoPlayer.clip = videoClips[currentClipIndex];
            videoPlayer.Play();
        }
        if(Input.GetKeyDown(KeyCode.U))
        {
            Debug.Log("您按下了U键 "+currentClipIndex); 
        }
    }
}
