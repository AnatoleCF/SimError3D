using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ErrorJsonImporter : MonoBehaviour
{
    public TextAsset jsonFile;

    private struct Data
    {
        

        public int num;
        public string name;
        public string[] groups;

        public Subdata niv3;

    }

    private struct Subdata
    {

        public SubSubDataFuckYou[] checkboxes;

        public string[] correct_checkboxes;
        public string explanation;
        public string explanation_nonError;
    }

    private struct SubSubDataFuckYou
    {
        public string value;
        public string text;
    }

    [Button]
    public void Import()
    {
        Data[] datas = JsonUtility.FromJson<Data[]>(jsonFile.text);

        foreach(Data d in datas)
        {
            Debug.Log("======================");
            Debug.Log(d.num);
            Debug.Log(d.name);
            Debug.Log("==== 1 ====");
            foreach(string g in d.groups)
            {
                Debug.Log("g");
            }
            Debug.Log("====");

            Debug.Log("==== 2 ====");
            foreach(SubSubDataFuckYou ssdfy in d.niv3.checkboxes)
            {
                Debug.Log(ssdfy.value + " : " + ssdfy.text);
            }
            Debug.Log("====");
            Debug.Log("==== 3 ====");
            foreach(string s in d.niv3.correct_checkboxes)
            {
                Debug.Log("s");
            }
            Debug.Log("====");
            Debug.Log(d.niv3.explanation);
            Debug.Log(d.niv3.explanation_nonError);
            Debug.Log("======================");
        }
    }
}
