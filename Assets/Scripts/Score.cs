using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 玩家得分显示
/// </summary>
public class Score : MonoBehaviour
{
    public int score = 0;                   // 玩家当前分数

    public int previousScore = 0;          // 前一帧中的得分

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 设置分数文本
        GetComponent<Text>().text = "Score: " + score;

        if (previousScore != score)
        {
            // 执行角色控制脚本中的嘲笑协程， 随机播放音效
            // 记住当前分数值
            previousScore = score;
        }

    }
}
