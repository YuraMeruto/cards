using System.Collections.Generic;
using UnityEngine;
public class AnimationUpdateList
{

    List<int> remove_list = new List<int>();
    Dictionary<int, AnimationUpdateBase> animation_update_bases = new Dictionary<int, AnimationUpdateBase>();
    Dictionary<int, AnimationUpdateBase> add_update_bases = new Dictionary<int, AnimationUpdateBase>();

    public Dictionary<int, AnimationUpdateBase> AnimationUpdateBases { get { return animation_update_bases; } }


    public void Add(GameObject obj, AnimationUpdateBase add)
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
        AddUpdateBases();
    }

    public void AddRemove(GameObject obj)
    {
        remove_list.Add(obj.GetInstanceID());
    }

    void AddUpdateBases()
    {
        foreach (var val in add_update_bases)
        {
            animation_update_bases.Add(val.Key, val.Value);
        }
        add_update_bases.Clear();
    }

    void Remove()
    {
        foreach (var val in remove_list)
        {
            animation_update_bases[val].Finish();
            if (!animation_update_bases.Remove(val))
            {
                continue;
            }
        }
        remove_list.Clear();
    }
}
