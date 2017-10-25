using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="StatSciptable", menuName = "Variables/StatSciptable", order = 1)]
public class StatSciptable : ScriptableObject
{
    public List<FloatVariable> StatList;
    public Dictionary<string, FloatVariable> StatDictionary;

    void OnEnable()
    {
        StatDictionary = new Dictionary<string, FloatVariable>();
        foreach (var Stat in StatList)
        {
            StatDictionary.Add(Stat.name, Stat);
        }
    }

    public FloatVariable GetStat(string value)
    {
        return StatDictionary[value];
    }

    public bool _Alive;
}
