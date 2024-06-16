using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
    [SerializeField] private TextAsset Jsonfile;
    [SerializeField] private GameObject Lnpref;
    [SerializeField] private GameObject Unpref;
    [SerializeField] private GameObject Muse;
    public List<note> Notes = new List<note>();
    private void Awake()
    {
        Debug.Log("Charter awake");
        _Mstitle = "HammerGirl";
        _Filepath = _Mstitle + "/" + _Mstitle;
        var json = Resources.Load<TextAsset>(_Filepath).text;//ÉoÉOÇ†ÇË
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
            Notes.Add(new note{Mode = note.SNM, Length = Notelength, Xpos = Destinationx, Movestarttime =  Movestarttime, Judgeyet = true, Notep = null, Moveyet = true});
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
        GameObject Note;
        float time = Muse.GetComponent<AudioSource>().time;
        List<note> VNL = Notes.Where(note => note.Movestarttime <= time && note.Judgeyet).ToList();
        Debug.Log(Notes[0].Movestarttime +" " + Notes[0].Judgeyet );
        List<note> RNL = Notes.Where(note=> note.Judgeyet == false).ToList();
        Debug.Log("Time: " + time + " VNL_C: " + VNL.Count+" RNL_C "+RNL.Count);
        List<MoveNote> DNL = new List<MoveNote>();
        foreach (var note in VNL)
        {
            Debug.Log("View foreach");
            List<note> RN = new List<note>();
            if (note.Moveyet)
            {
                Debug.Log("Generate Start");
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
                if (RN.Count == 0)
                {
                    var DN = DNL.Find(note => note.Xpos == RN[0].Xpos && note.Movestarttime == RN[0].Movestarttime);
                    if (DN != null)
                    {
                        Debug.Log(DN);
                        DNL.Remove(DN);
                        DN.Mode = note.Mode;
                        DN.Length = note.Length;
                        DN.Xpos = note.Xpos;
                        DN.Movestarttime = note.Movestarttime;
                        DN.Judgeyet = true;
                        DN.Moveyet = true;
                        DNL.Add(DN);
                        RNL.Remove(RN[0]);
                        Notes.Remove(RN[0]);
                        DN.NoteMM.transform.position = new Vector3(DN.Xpos, 0.001f, 51.5f);
                    }
                    else { Debug.LogError("ERROR!!!!!!!!!!!!!!!!!!!!"); }//Ç±Ç±èCê≥ÇµÇÎ
                }
                else
                {
                    Debug.Log("instantiate");
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