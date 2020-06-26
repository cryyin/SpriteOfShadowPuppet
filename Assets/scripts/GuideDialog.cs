using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuideDialog : MonoBehaviour
{
    [Header("UI组件")]
    public Text textLabel;

    [Header("文本文件")]
    public TextAsset textFile;
    public int index;
    public float textSpeed;

    bool textFinished;//是否完成打字
    bool cancelTyping;//取消打字

    List<string> textList = new List<string>();

    void Awake()
    {
        GetTextFormFile(textFile);
    }

    private void OnEnable()
    {
        //textLabel.text = textList[index];
        //index++;
        textFinished = true;
        StartCoroutine(SetTextUI());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (index == textList.Count)
            {
                gameObject.SetActive(false);
                index = 0;
                return;
            }
            else if (textFinished)
            {
                //textLabel.text = textList[index];
                //index++;
                StartCoroutine(SetTextUI());
            }
        }
    }

    void GetTextFormFile(TextAsset file)
    {
        textList.Clear();
        index = 0;

        var lineData = file.text.Split('\n');

        foreach (var line in lineData)
        {
            textList.Add(line);
        }
    }

    IEnumerator SetTextUI()
    {
        textFinished = false;
        textLabel.text = "";

        for (int i = 0; i < textList[index].Length; i++)
        {
            textLabel.text += textList[index][i];

            yield return new WaitForSeconds(textSpeed);
        }
        textFinished = true;
        index++;
    }
}
