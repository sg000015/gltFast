using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace Dtype
{



    [Serializable]
    public class DtypeData      //최상위 데이터
    {

        public ProductType type;
        public DtypeTransform transform;
        public ProductInfo info;

        [HideInInspector] //렉걸려서 가림
        public byte[] data;

        [SerializeField] //data대신 확인, 저장X
        private int DataLength; public int dataLength { get { return DataLength; } set { DataLength = value; } }


        public DtypeData()
        {
            type = ProductType.Null;
            transform = new DtypeTransform();
            info = new ProductInfo();
        }

        public DtypeData(ProductType _type)
        {
            type = _type;
            transform = new DtypeTransform();
            info = new ProductInfo();
        }
    }




    [Serializable]
    public enum ProductType
    {
        Null,
        Paint,
        Video,
        Object,
    }


    [Serializable]
    public class DtypeTransform
    {
        //저장되는변수
        public Vector3 Position;
        public Vector3 Rotation;
        public Vector3 Scale;

        //get,set property
        public Vector3 postion { get { return Position; } set { CheckPos(value); } }
        public Vector3 rotation { get { return Rotation; } set { CheckRot(value); } }
        public Vector3 scale { get { return Scale; } set { CheckScale(value); } }


        void CheckPos(Vector3 value)    // 가로세로는 제한동일
        {
            int limitX = 500;
            int limitY = 100;
            if (value.x > limitX) { value.x = limitX; } else if (value.x < -limitX) { value.x = -limitX; }
            if (value.y > limitY) { value.y = limitY; } else if (value.y < -limitY) { value.y = -limitY; }
            if (value.z > limitX) { value.z = limitX; } else if (value.z < -limitX) { value.z = -limitX; }
            Position = value;
        }
        void CheckRot(Vector3 value)    //-180 ~ 180
        {
            int limit = 180;
            if (value.x > limit) { value.x = limit; } else if (value.x < -limit) { value.x = -limit; }
            if (value.y > limit) { value.y = limit; } else if (value.y < -limit) { value.y = -limit; }
            if (value.z > limit) { value.z = limit; } else if (value.z < -limit) { value.z = -limit; }
            Rotation = value;
        }
        void CheckScale(Vector3 value)  //반대크기로도 설정가능
        {
            int limit = 5;
            if (value.x > limit) { value.x = limit; } else if (value.x < -limit) { value.x = -limit; }
            if (value.y > limit) { value.y = limit; } else if (value.y < -limit) { value.y = -limit; }
            if (value.z > limit) { value.z = limit; } else if (value.z < -limit) { value.z = -limit; }
            Scale = value;
        }

    }


    [Serializable]
    public class ProductInfo
    {
        //공용데이터
    }

    [Serializable]
    public class PaintInfo : ProductInfo
    {

    }
    [Serializable]
    public class VideoInfo : ProductInfo
    {

    }

    [Serializable]
    public class ObjectInfo : ProductInfo
    {

    }








}
