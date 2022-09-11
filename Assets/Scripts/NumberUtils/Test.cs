
using UnityEngine;
using UnityEditor;
using StringUtility;
using System.Diagnostics;
using System;
public class Test : MonoBehaviour
{
    public string inputStr;
    public string sortStr;


    public void Runtest()
    {
        byte[] inputAndOutputBytes = System.Text.Encoding.ASCII.GetBytes(inputStr);
        byte[] sortBytes = System.Text.Encoding.ASCII.GetBytes(sortStr);
        Stopwatch sw = new Stopwatch();
        sw.Start();
        int testamount = 1;//2.09//0.0021
        for(int i = 0; i < testamount; i++)
        {
            StringUtils.SortLetters2(ref inputAndOutputBytes, sortBytes);
        }
        sw.Stop();
        TimeSpan ts = sw.Elapsed;
        UnityEngine.Debug.Log(ts.TotalMilliseconds / testamount);
        //StringUtils.DebugTest(inputAndOutputBytes);
    }
}

[CustomEditor(typeof(Test))]
public class TestEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Test test = (Test)target;

        if (GUILayout.Button("Test"))
        {
            test.Runtest();
        }
    }
}
