using UnityEngine;


[System.Serializable]
public class StoryParam
{
    public string serif_message;
    public int left_character_sprite_number;
    public ScenarioIconManager.CharacterType left_character_type;
    public int right_character_sprite_number;
    public ScenarioIconManager.CharacterType right_character_type;
    public int center_character_sprite_number;
    public ScenarioIconManager.CharacterType center_character_type;
    public int back_ground_sprite_number;
    public AudioClip serif_voice;
    public CharacterNames.Character serif_character_type;
}
