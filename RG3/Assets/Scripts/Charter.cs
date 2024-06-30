using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public enum Status
{
    Idle, Active, Move, Judged
}

public enum Type
{
    High, Low
}

[System.Serializable]
public class Chart
{
    public string Title;
    public string Composer;
    public string ChartMaker;
    public float BPM;
    public int Level;
    public List<Note> Notes;
}
[System.Serializable]
public class Note
{
    public int SNM;
    public int SNL;
    public int SNX;
    public float SNY;
}
public class MoveNote
{
    public Type Mode;
    public float Length;
    public float Xpos;
    public float Movestarttime;
    public Status Notestatus;
    public GameObject NoteObj;
}
public class Touch
{
    public float Posx;
    public float Posy;
    public float MusicTime;
}
public class Charter : MonoBehaviour
{
    private string _Mstitle;
    private string _Filepath;
    public Chart Obj;
    public Note Note;
    [SerializeField] private TextAsset Jsonfile;
    [SerializeField] private GameObject Lnpref;
    [SerializeField] private GameObject Unpref;
    [SerializeField] private GameObject Muse;
    [SerializeField] private Camera Cam;
    public List<MoveNote> Notes = new List<MoveNote>();
    public List<Touch> Touchs = new List<Touch>();
    List<MoveNote> DNL = new List<MoveNote>();
    private void Awake()
    {
        Debug.Log("Charter awake");
        _Mstitle = "HammerGirl";
        _Filepath = _Mstitle + "/" + _Mstitle;
        var json = Resources.Load<TextAsset>(_Filepath).text;//バグあり
        Obj = JsonUtility.FromJson<Chart>(json);
        Debug.Log(json.ToString());
    }
    void Start()
    {
        Debug.Log("Charter start");
        AudioSource audiosource = Muse.GetComponent<AudioSource>();
        audiosource.Play();
        Debug.Log(Obj.Notes.Count);
        foreach (var note in Obj.Notes) {
            float Movestarttime = (note.SNY - NoteMove.Movetime);
            float Destinationx = (-5 + note.SNX * 0.625f);
            float Notelength = (note.SNL * 0.625f);
            if(note.SNM == 0)
            {
                Notes.Add(new MoveNote { Mode = Type.Low, Length = Notelength, Xpos = Destinationx, Movestarttime = Movestarttime });
            }
            else if(note.SNM == 1)
            {
                Notes.Add(new MoveNote { Mode = Type.High, Length = Notelength, Xpos = Destinationx, Movestarttime = Movestarttime });
            }
            
            Debug.Log("Notes adding");
            //GameObject Notesd;
            //if (Obj.Notes[i].SNM == 0) 
            //{

            //}//Lower
            //else if (Obj.Notes[i].SNM == 1) 
            //{
            //    Notesd = GameObject.Instantiate(Unpref, new Vector3(Destinationx, 0.001f, 51.5f), Quaternion.identity);
            //}//Upper

        }
    }
    private void Update()
    {
        float time = Muse.GetComponent<AudioSource>().time;
        Notes.Where(note => note.Notestatus == Status.Idle && note.Movestarttime <= time).ToList().ForEach(note => note.Notestatus = Status.Active);
        List<MoveNote> M2JList = Notes.Where(note => note.Notestatus == Status.Move).ToList();
        foreach (var note in M2JList)
        {
            var K = note.NoteObj.GetComponent<NoteMove>();
            if (K.Endmove)
            {
                note.Notestatus = Status.Judged;
            }
        }
        List<MoveNote> ReuseList = Notes.Where(note => note.Notestatus == Status.Judged && note.NoteObj != null).ToList();
        List<MoveNote> ActiveList = Notes.Where(note => note.Notestatus == Status.Active).ToList();
        if (ReuseList.Count > 0)
        {
            int index = 0;
            
        }
        
        foreach (var note in ActiveList)
        {
            foreach (var reuse in ReuseList)
            {
                if(ReuseList.Count == 0)
                {
                    break;
                }
                reuse.NoteObj.transform.position = new Vector3(note.Xpos, 0.001f, 51.5f);//NoteのTypeで使いまわす種類を変えるようにしろ
                note.NoteObj = reuse.NoteObj;
                reuse.NoteObj = null;
                ReuseList.Remove(reuse);
                note.Notestatus = Status.Move;
                note.NoteObj.GetComponent<NoteMove>().Move();
            }
            if (note.Mode == Type.Low)
            {
                note.NoteObj = GameObject.Instantiate(Lnpref, new Vector3(note.Xpos, 0.001f, 51.5f), Quaternion.identity);
            }
            else if (note.Mode == Type.High)
            {
                note.NoteObj = GameObject.Instantiate(Unpref, new Vector3(note.Xpos, 0.001f, 51.5f), Quaternion.identity);
            }
            note.Notestatus = Status.Move;
        }
        
        if (Input.touchCount >= 1)
        {
            Touch T = new Touch();
            T.Posx = Cam.ScreenToWorldPoint(Input.GetTouch(0).position).x;
            T.Posy = Cam.ScreenToWorldPoint(Input.GetTouch(0).position).y;
            T.MusicTime = Muse.GetComponent<AudioSource>().time;
            Touchs.Add(T);
            Judge(T);
        }
    }

    void Judge(Touch T)
    {
        List<MoveNote> JNL = Notes.Where((note => T.MusicTime < note.Movestarttime + NoteMove.Movetime + 0.1 || T.MusicTime > note.Movestarttime - 0.1)/*ここにx座標でフィルタする条件を記入*/).ToList();
        foreach (MoveNote note in JNL)
        {
            
        }
    }
}