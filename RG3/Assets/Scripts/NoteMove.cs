using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NoteMove : MonoBehaviour
{
    public static float Highspeed = 1f; //HiSpeedの設定の値
    public static float Movetime = 2f;//暫定,計算式を作れ
    private void Start()
    {

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
            transform.position = Vector3.Lerp(startPos, endPos, (Time.time - startTime) / Movetime);
            yield return null;
        }
        transform.position = endPos;
    }
}