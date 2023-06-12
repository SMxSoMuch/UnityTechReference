using System;
using System.Linq;

using UnityEditor;
using UnityEngine;


[InitializeOnLoad]
public class InstantiateObjectsTest
{
    interface ISomeObj { public void Exe();}
    class SomeObj : ISomeObj { public void Exe() { } }
    class SomeObj2 : ISomeObj { public void Exe() { } }
    class SomeObj3 : ISomeObj { public void Exe() { } }
    
    public InstantiateObjectsTest
    {
        //normal instantiate by class
        SomeObj someObj = new SomeObj();
        someObj.Exe();

        //instantiate by Type
        Type t = typeof(SomeObj);
        object obj = Activator.CreateInstance(t, true);
        //since Activator.CreateInstance() return a System.object
        //you need to cast it as your usable type
        SomeObj sobj = obj as SomeObj;
        sobj.Exe();

        //instantiate an array of class by Type
        Type[] objs = { 
            typeof(SomeObj),
            typeof(SomeObj2),
            typeof(SomeObj3)
        };
        foreach (Type o in objs)
        {
            SomeObj arrsobj = Activator.CreateInstance(o, true) as SomeObj;
            arrsobj.Exe();
        }

        //Get all class in this assemblies that implement interface
        Type i = typeof(ISomeObj);
        ISomeObj[] pwids = 
            AppDomain.CurrentDomain.GetAssemblies().
            SelectMany(s => s.GetTypes()).
            Where(w => i.IsAssignableFrom(w) && w.IsClass && !w.IsAbstract).
            Select(s => Activator.CreateInstance(s, true) as ISomeObj).
            ToArray();
        foreach (ISomeObj iSomeObj in pwids)
            iSomeObj.Exe();
    }
}
