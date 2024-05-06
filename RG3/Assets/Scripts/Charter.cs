using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

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
public class Charter : MonoBehaviour
{
    private string _Mstitle;
    private string _Filepath;
    public Chart Obj;
    [SerializeField] private GameObject Lnpref;
    [SerializeField] private GameObject Unpref;
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
            GameObject Notes;
            if (Obj.Notes[i].SNM == 0) 
            {
                Notes = GameObject.Instantiate(Lnpref, new Vector3(Destinationx, 0.001f, 51.5f), Quaternion.identity);
            }//Lower
            else if (Obj.Notes[i].SNM == 1) 
            {
                Notes = GameObject.Instantiate(Unpref, new Vector3(Destinationx, 0.001f, 51.5f), Quaternion.identity);
            }//Upper
            StartCoroutine(Wait(Movestarttime, Destinationx, Notelength));
        }
    }
    IEnumerator Wait(float Movestarttime, float  Destinationx, float Notelength)
    {
        yield return new WaitForSeconds(Movestarttime);

    }
}