using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMove : MonoBehaviour
{
    float HiSpeed = 1f; //HiSpeedの設定の値
    float Speed = 0.5f; //HiSpeed=1.0fの時の移動量
    float MoveLength; //1fごとの移動量

    float Movetime = 5f; //まだ渡し元がないので代用、実装時に消す

    private void Start()
    {
        MoveLength = HiSpeed * Speed;
        Move(Movetime);
    }
    public void Move(float Movetime)
    { 
        StartCoroutine(NM(Movetime)); //Movetimeはノーツが移動している時間、本来は別スクリプトから渡される。
    }

    IEnumerator NM(float WaitTime) //動かすスクリプト、時間とかで見直す必要あるかも
    {
        for (int i = 0; i < 150; i++)
        {
            Transform tN = this.gameObject.transform;
            tN.transform.position -= new Vector3(0, 0, MoveLength);
            yield return new WaitForSeconds(WaitTime / 150);
        }
    }
}
