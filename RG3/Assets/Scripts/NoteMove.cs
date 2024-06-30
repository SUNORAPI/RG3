using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.XR;
using UnityEngine;

public class NoteMove : MonoBehaviour
{
    public static float Highspeed = 1f; //HiSpeedの設定の値
    public static float Movetime = 2f;//暫定,計算式を作れ
    public bool Endmove;
    private void Start()
    {
        Endmove = false;
        Move();
    }
    public void Move()
    { 
        StartCoroutine(NM()); //Movetimeはノーツが移動している時間、本来は別スクリプトから渡される。
    }

    IEnumerator NM() //動かすスクリプト、時間とかで見直す必要あるかも
    {
        var startTime = Time.time;
        var startPos = transform.position;
        var endPos = new Vector3(transform.position.x, transform.position.y, -1);
        while (Time.time < startTime + Movetime)
        {
            transform.position = Vector3.Lerp(startPos, endPos, (Time.time - startTime) / Movetime);//判定ライン上に来る時間は少しずれる。通り過ぎるようにしているため
            yield return null;
        }
        transform.position = endPos;
        Endmove = true;
    }
}