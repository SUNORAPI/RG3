using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

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
public class note
{
    public int Mode;
    public float Length;
    public float Xpos;
    public float Movestarttime;
    public bool Judgeyet;
    public GameObject Notep;
    public bool Moveyet;
}
public class MoveNote
{
    public int Mode;
    public float Length;
    public float Xpos;
    public float Movestarttime;
    public bool Judgeyet;
    public GameObject NoteMM;
    public bool Moveyet;
}
public class Charter : MonoBehaviour
{
    private string _Mstitle;
    private string _Filepath;
    public Chart Obj;
    public Note Note;
    [SerializeField] private GameObject Lnpref;
    [SerializeField] private GameObject Unpref;
    [SerializeField] private GameObject Muse;
    public List<note> Notes = new List<note>();
    private void Awake()
    {
        _Mstitle = "HammerGirl";
        _Filepath = _Mstitle + "/" + _Mstitle;
        var json = Resources.Load<TextAsset>(_Filepath).text;
        Obj = JsonUtility.FromJson<Chart>(json);
    }
    void Start()
    {
        for (int i = 0; i < Obj.Notes.Count; i++)
        {
            float Movestarttime = (Obj.Notes[i].SNY - NoteMove.Movetime);
            float Destinationx = (-5 + Obj.Notes[i].SNX * 0.625f);
            float Notelength = (Obj.Notes[i].SNL * 0.625f);
            Notes.Add(new note{Mode = Obj.Notes[i].SNM, Length = Notelength, Xpos = Destinationx, Movestarttime =  Movestarttime, Judgeyet = true, Notep = null, Moveyet = true});
            GameObject Notesd;
            if (Obj.Notes[i].SNM == 0) 
            {
                
            }//Lower
            else if (Obj.Notes[i].SNM == 1) 
            {
                Notesd = GameObject.Instantiate(Unpref, new Vector3(Destinationx, 0.001f, 51.5f), Quaternion.identity);
            }//Upper

        }
    }
    private void Update()
    {
        GameObject Note;
        float time = Muse.GetComponent<AudioSource>().time;
        List<note> VNL = Notes.Where(note => note.Movestarttime <= time && note.Judgeyet).ToList();
        List<note> RNL = Notes.Where(note=> note.Judgeyet = false).ToList();
        List<MoveNote> DNL = new List<MoveNote>();
        foreach (var note in VNL)
        {
            List<note> RN = new List<note>();
            if (note.Moveyet)
            {
                if (note.Mode == 0)
                {
                    note.Notep = Lnpref;
                    RN = RNL.Where(note => note.Mode == 0).ToList();
                }
                else if (note.Mode == 1)
                {
                    note.Notep = Unpref;
                    RN = RNL.Where(note => note.Mode == 1).ToList();
                }
                if (RN != null)
                {
                    List<MoveNote> DN = DNL.Where(note => note.Xpos == RN[0].Xpos && note.Movestarttime == RN[0].Movestarttime).ToList();
                    if (DN != null)
                    {
                        DNL.Remove(DN[0]);
                        DN[0].Mode = note.Mode;
                        DN[0].Length = note.Length;
                        DN[0].Xpos = note.Xpos;
                        DN[0].Movestarttime = note.Movestarttime;
                        DN[0].Judgeyet = true;
                        DN[0].Moveyet = true;
                        DNL.Add(DN[0]);
                        RNL.Remove(RN[0]);
                        Notes.Remove(RN[0]);
                    }
                }
                else
                {
                    Note = GameObject.Instantiate(note.Notep, new Vector3(note.Xpos, 0.001f, 51.5f), Quaternion.identity);
                    MoveNote MN = new();
                    MN.Mode = note.Mode;
                    MN.Length = note.Length;
                    MN.Xpos = note.Xpos;
                    MN.Movestarttime = note.Movestarttime;
                    MN.Judgeyet = true;
                    MN.NoteMM = Note;
                    MN.Moveyet = true;
                    DNL.Add(MN);
                }
            }
        }
    }
}