using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Dtype;

public class DataManager : Singleton<DataManager>
{

    public DtypeSaveData dtypeSaveData = new DtypeSaveData();
    public SampleData sampleData = new SampleData();




    [Serializable]
    public class DtypeSaveData
    {
        public List<DtypeData> dtypeDatas = new List<DtypeData>();
    }


    [Serializable]
    public class SampleData
    {
        public string sample = "sample";
    }




}
