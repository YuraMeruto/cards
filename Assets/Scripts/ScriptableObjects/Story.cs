using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Story")]
public class Story : ScriptableObject
{
   public List<StoryParam> list = new List<StoryParam>();

}
