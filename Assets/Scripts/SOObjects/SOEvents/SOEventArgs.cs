using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOEventArgs 
{
    
}


public class SOEventArgOne<T> : SOEventArgs
{
    public T arg;
    
    public SOEventArgOne(T _arg)
    {
        arg = _arg;
    }
}

public class SOEventArgTwo<T1, T2> : SOEventArgs
{
    public T1 arg1;
    public T2 arg2;

    public SOEventArgTwo(T1 _arg1, T2 _arg2)
    {
        arg1 = _arg1;
        arg2 = _arg2;
    }
}

public class SOEventArgThree<T1, T2, T3> : SOEventArgs
{
    public T1 arg1;
    public T2 arg2;
    public T3 arg3;

    public SOEventArgThree(T1 _arg1, T2 _arg2, T3 _arg3)
    {
        arg1 = _arg1;
        arg2 = _arg2;
        arg3 = _arg3;
    }
}