using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInspectable
{
    public void Highlight(Material material);
    public virtual void Unhighlight() { }
}
