using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class AnimationUpdateList 
{
    Dictionary<int, AnimationUpdateBase> animation_update_bases = new Dictionary<int, AnimationUpdateBase>();
    public Dictionary<int, AnimationUpdateBase> AnimationUpdateBases { get { return animation_update_bases; } }
    List<int> remove_list = new List<int>();
    Dictionary<int, AnimationUpdateBase> add_update_bases = new Dictionary<int, AnimationUpdateBase>();

    public void Add(GameObject obj,AnimationUpdateBase add)
    {
        add_update_bases.Add(obj.GetInstanceID(), add);
        
    }

    // Update is called once per frame
    public void Update()
    {
        foreach (var animation in animation_update_bases)
        {
            animation.Value.Update();
        }
        Remove();
        addUpdateBases();
    }

    void addUpdateBases()
    {
        foreach(var val in add_update_bases)
        {
            animation_update_bases.Add(val.Key,val.Value);
        }
        add_update_bases.Clear();
    }

    public void addRemove(GameObject obj)
    {
        remove_list.Add(obj.GetInstanceID());
    }


    void Remove()
    {
        foreach (var val in remove_list){
            animation_update_bases[val].Finish();
            if (!animation_update_bases.Remove(val))
            {
                continue;
            }
        }
        remove_list.Clear();
    }
}
