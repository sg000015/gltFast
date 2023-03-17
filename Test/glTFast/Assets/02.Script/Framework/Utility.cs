using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility : MonoBehaviour
{


    //컴포넌트 복사 
    public static T CopyComponent<T>(T origin, GameObject target) where T : Component
    {
        System.Type type = origin.GetType();
        Component copy = target.AddComponent(type);
        System.Reflection.FieldInfo[] fields = type.GetFields();
        foreach (var field in fields)
        {
            field.SetValue(copy, field.GetValue(origin));
        }
        return copy as T;
    }


}
