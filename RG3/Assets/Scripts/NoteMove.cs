using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NoteMove : MonoBehaviour
{
    public static float Highspeed = 1f; //HiSpeed�̐ݒ�̒l
    public static float Movetime = 2f;//�b��,�v�Z�������
    private void Start()
    {

        Move();
    }
    public void Move()
    { 
        StartCoroutine(NM()); //Movetime�̓m�[�c���ړ����Ă��鎞�ԁA�{���͕ʃX�N���v�g����n�����B
    }

    IEnumerator NM() //�������X�N���v�g�A���ԂƂ��Ō������K�v���邩��
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