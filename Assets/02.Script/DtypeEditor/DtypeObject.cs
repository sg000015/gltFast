using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dtype
{
    public class DtypeObject : MonoBehaviour
    {
        public DtypeData d_data;

        public DtypeObject(ProductType type)
        {
            d_data = new DtypeData(type);
        }


        public void asdasd()
        {
            // EventSystem.current.IsPointerOverGameObject()
        }

        [ContextMenu("ExportFile")]
        void A()
        {
            System.IO.File.WriteAllBytes(Application.dataPath + "/DtypeData.bytes", d_data.data);
            Debug.Log(Application.dataPath + "/DtypeData.bytes");
        }



    }
}
