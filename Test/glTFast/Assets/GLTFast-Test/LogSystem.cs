using System.Collections;
using System.Collections.Generic;
using GLTFast.Logging;
using UnityEngine;


namespace GLTFast
{
    public class LogSystem : GLTFast.Logging.ICodeLogger
    {

        public void Error(LogCode code, params string[] messages)
        {
            Debug.LogError("AA");

        }

        public void Error(string message)
        {
            Debug.LogError("AA");
        }

        public void Info(LogCode code, params string[] messages)
        {
            throw new System.NotImplementedException();
        }

        public void Info(string message)
        {
            throw new System.NotImplementedException();
        }

        public void Warning(LogCode code, params string[] messages)
        {
            throw new System.NotImplementedException();
        }

        public void Warning(string message)
        {
            throw new System.NotImplementedException();
        }
    }
}