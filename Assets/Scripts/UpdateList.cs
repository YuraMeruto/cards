using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateList
{
    List<UpdateBase> updates = new List<UpdateBase>();

    public void Add(UpdateBase add)
    {
        updates.Add(add);
    }
    public void Update()
    {
        for (var index = 0; index < updates.Count; index++)
        {
            updates[index].Update();
        }
    }
}
