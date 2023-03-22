using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CardInstanceMoveList
{
    static Dictionary<int, CardInstanceMove> card_instance_move_list = new Dictionary<int, CardInstanceMove>();
    public static Dictionary<int, CardInstanceMove> cardInstanceMoves { get { return card_instance_move_list; } set { card_instance_move_list = value; } }
    static bool is_update = false;
    static CardInstanceMoveList instance;
    public static CardInstanceMoveList Instance { get { return instance; } set { instance = value; } }

    public static void setCardInstanceMove(GameObject obj, CardInstanceMove move)
    {
        CardInstanceMoveList.card_instance_move_list.Add(obj.GetInstanceID(), move);
    }

    public static void Remove(GameObject obj)
    {
        CardInstanceMoveList.card_instance_move_list.Remove(obj.GetInstanceID());
        if (CardInstanceMoveList.card_instance_move_list.Count != 0)
        {
            return;
        }
        StartCountAnimationManager.startAnimationInstance();
    }
}
