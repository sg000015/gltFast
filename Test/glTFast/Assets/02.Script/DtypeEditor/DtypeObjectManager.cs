using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


namespace Dtype
{
    public class DtypeObjectManager : Singleton<DtypeObjectManager>
    {

        #region field

        public List<DtypeObject> d_ObjList = new List<DtypeObject>();


        public static Transform Dt_Transorm
        {
            get { return DtypeObjectManager.Inst.transform; }
        }

        HighlightPlus.HighlightEffect highlightEfx;


        #endregion

        void Start()
        {
            highlightEfx = GetComponent<HighlightPlus.HighlightEffect>();
        }





        ///<summary>
        /// Glb로드시, 강제 생성이긴한데 
        ///</summary>
        public void AddObject(byte[] data)
        {
            var dto = DtypeObjectManager.Inst.transform.
                      GetChild(DtypeObjectManager.Inst.transform.childCount - 1).
                      gameObject.AddComponent<DtypeObject>();

            dto.d_data = new DtypeData(ProductType.Object);
            dto.d_data.data = data;
            dto.d_data.dataLength = data.Length;

            // Utility.CopyComponent<HighlightPlus.HighlightEffect>(highlightEfx, dto.gameObject);

            DtypeObjectManager.Inst.d_ObjList.Add(dto);

            DataManager.Inst.dtypeSaveData.dtypeDatas.Add(dto.d_data);



        }



    }


}