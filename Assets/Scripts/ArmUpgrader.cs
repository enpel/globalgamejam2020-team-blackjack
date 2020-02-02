using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArmUpgrader : MonoBehaviour,IArmUpgrader
{
    private List<Arm> children;
    private void Start()
    {
        children = this.GetComponentsInChildren<Arm>().ToList();
        SetArmGrade(0);
    }

    public void SetArmGrade(int grade)
    {
        children.ForEach(x => x.gameObject.SetActive(grade == x.grade));
    }
}


public interface IArmUpgrader
{
    void SetArmGrade(int grade);
}