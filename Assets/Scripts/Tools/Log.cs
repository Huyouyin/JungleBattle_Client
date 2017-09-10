using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Log  {
    static bool openLog = true;
	public static void i(string log)
    {
        if(openLog)
        {
            Debug.Log(log);
        }
    }
    public static void Warning(string log)
    {
        if(openLog)
        {
            Debug.LogWarning(log);
        }
    }
    public static void Error(string log)
    {
        if(openLog)
        {
            Debug.LogError(log);
        }
    }
    public static void Exception(Exception log)
    {
        if(openLog)
        {
            Debug.LogException(log);
        }
    }
}
