using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TheoryResearch : ResearchNode
{
    public override string DescribeUnlock()
    {
        return "This is a theory research (It won't unlock anything).";
    }
}
