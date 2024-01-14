using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMove : MonoBehaviour
{
    float HiSpeed = 1f; //HiSpeed�̐ݒ�̒l
    float Speed = 0.5f; //HiSpeed=1.0f�̎��̈ړ���
    float MoveLength; //1f���Ƃ̈ړ���

    float Movetime = 5f; //�܂��n�������Ȃ��̂ő�p�A�������ɏ���

    private void Start()
    {
        MoveLength = HiSpeed * Speed;
        Move(Movetime);
    }
    public void Move(float Movetime)
    { 
        StartCoroutine(NM(Movetime)); //Movetime�̓m�[�c���ړ����Ă��鎞�ԁA�{���͕ʃX�N���v�g����n�����B
    }

    IEnumerator NM(float WaitTime) //�������X�N���v�g�A���ԂƂ��Ō������K�v���邩��
    {
        for (int i = 0; i < 150; i++)
        {
            Transform tN = this.gameObject.transform;
            tN.transform.position -= new Vector3(0, 0, MoveLength);
            yield return new WaitForSeconds(WaitTime / 150);
        }
    }
}
