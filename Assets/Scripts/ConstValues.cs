using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstValues
{
    public const float DEFAULT_SWOON_TIME = 3;
    public const int SWOON_UPDATE_COLOR_ALPHA = 2;
    public const float DEFAULT_MOVE_SPEED = 10.0f;

    // �A��̃{�C�X
    public const string GO_HOME_CLUB_ATTACK_01 = "SE/TestSe01.mp3";
    public const string GO_HOME_CLUB_ATTACK_02 = "SE/TestSe02.mp3";
    public const string GO_HOME_CLUB_ATTACK_03 = "SE/TestSe03.mp3";
    public const string GO_HOME_CLUB_ATTACK_04 = "SE/TestSe04.mp3";
    public const string GO_HOME_CLUB_ATTACK_05 = "SE/TestSe05.mp3";
    public const string GO_HOME_CLUB_DRAW_BATTLE = "SE/TestSe06.mp3";
    // �������̃{�C�X
    public const string KENDO_CLUB_ATTACK_01 = "SE/TestSe01.mp3";
    public const string KENDO_CLUB_ATTACK_02 = "SE/TestSe02.mp3";
    public const string KENDO_CLUB_ATTACK_03 = "SE/TestSe03.mp3";
    public const string KENDO_CLUB_ATTACK_04 = "SE/TestSe04.mp3";
    public const string KENDO_CLUB_ATTACK_05 = "SE/TestSe05.mp3";
    public const string KENDO_CLUB_DRAW_BATTLE = "SE/TestSe06.mp3";


    // �A��̃{�C�X�̊Ǘ�����l
    public const int GO_HOME_CLUB_ATTACK_01_VALUE = 1;
    public const int GO_HOME_CLUB_ATTACK_02_VALUE = 2;
    public const int GO_HOME_CLUB_ATTACK_03_VALUE = 3;
    public const int GO_HOME_CLUB_ATTACK_04_VALUE = 4;
    public const int GO_HOME_CLUB_ATTACK_05_VALUE = 5;
    public const int GO_HOME_CLUB_DRAW_BATTLE_VALUE = 6;
    // �������̃{�C�X�̊Ǘ�����l
    public const int KENDO_CLUB_ATTACK_01_VALUE = 7;
    public const int KENDO_CLUB_ATTACK_02_VALUE = 8;
    public const int KENDO_CLUB_ATTACK_03_VALUE = 9;
    public const int KENDO_CLUB_ATTACK_04_VALUE = 10;
    public const int KENDO_CLUB_ATTACK_05_VALUE = 11;
    public const int KENDO_CLUB_DRAW_BATTLE_VALUE = 12;

    // �X�e�[�W
    public const int FIRST_STAGE = 1;

    // �I�u�W�F�N�g��
    public const string CANVAS_OBJECT_NAME = "Canvas";
    public const string RIGHT_EFFECT_OBJECT_NAME = "RightEffect";
    public const string COMBO_TEXT_OBJECT_NAME = "ComboText";
    public const string DRAW_BATTLE_TEXT_OBJECT_NAME = "DrawBattleText";
    public const string START_COUNT_TECT_OBJECT_NAME = "StartCountText";
    public const string ENEMY_HP_OBJECT_NAME = "EnmeyHp";
    public const string ENEMY_HP_GAGE_OBJECT_NAME = "EnemyHpGage";
    public const string PLAYER_HP_OBJECT_NAME = "PlayerHp";
    public const string PLYAER_HP_GAGE_OBJECT_NAME = "PlayerHpGage";
    public const string FINISH_TEXT_OBJECT_NAME = "FinishText";

    // �|�W�V�����֘A
    public const int PLAYABKE_POS_Z = 20;

    // ����SE�֘A
    public const string DRAW_BATTLE_RESULT_WIN = "SE/TestSe08.mp3";
    public const string DRAW_BATTLE_RESULT_LOSE = "SE/TestSe09.mp3";

    // ����SE�̒l
    public const int DRAW_BATTLE_RESULT_WIN_VALUE = 1;

    // �V�[����
    public const string BATTLE_SCENE = "Battle";

    // �摜�̐ݒ蔻��
    public const int NONE_SPRITE_VALUE = -1;

    public const int SERIF_SPLIT_SIZE = 9;
    public const int SERIF_SPLIT_SIZE_FOR_WINDOW = 18;

    // �A��̃G�t�F�N�g�̊Ǘ�����l
    public const int GO_HOME_CLUB_ATTACK_EFFECT_01_VALUE = 1;
    public const int GO_HOME_CLUB_ATTACK_EFFECT_02_VALUE = 2;
    public const int GO_HOME_CLUB_ATTACK_EFFECT_03_VALUE = 3;
    public const int GO_HOME_CLUB_ATTACK_EFFECT_04_VALUE = 4;
    public const int GO_HOME_CLUB_ATTACK_EFFECT_05_VALUE = 5;
    public const int GO_HOME_CLUB_DRAW_EFFECT_BATTLE_VALUE = 6;


    // �A��̃G�t�F�N�g�I�u�W�F�N�g
    public const string GO_HOME_CLUB_EFFECT_ATTACK_01 = "Prefabs/Effect/GoHome/GoHomeEffect01.prefab";
    public const string GO_HOME_CLUB_EFFECT_ATTACK_02 = "Prefabs/Effect/GoHome/GoHomeEffect02.prefab";
    public const string GO_HOME_CLUB_EFFECT_ATTACK_03 = "Prefabs/Effect/GoHome/GoHomeEffect03.prefab";
    public const string GO_HOME_CLUB_EFFECT_ATTACK_04 = "Prefabs/Effect/GoHome/GoHomeEffect04.prefab";
    public const string GO_HOME_CLUB_EFFECT_ATTACK_05 = "Prefabs/Effect/GoHome/GoHomeEffect05.prefab";
    public const string GO_HOME_CLUB_DRAW_EFFECT_BATTLE = "Prefabs/Effect/GoHome/GoHomeEffectDraw.prefab";


    // �������̃G�t�F�N�g�̊Ǘ�����l
    public const int KENDO_CLUB_EFFECT_ATTACK_01_VALUE = 7;
    public const int KENDO_CLUB_EFFECT_ATTACK_02_VALUE = 8;
    public const int KENDO_CLUB_EFFECT_ATTACK_03_VALUE = 9;
    public const int KENDO_CLUB_EFFECT_ATTACK_04_VALUE = 10;
    public const int KENDO_CLUB_EFFECT_ATTACK_05_VALUE = 11;
    public const int KENDO_CLUB_DRAW_BATTLE_EFFECT_VALUE = 12;

    // �������̃G�t�F�N�g�I�u�W�F�N�g
    public const string KENDO_CLUB_EFFECT_ATTACK_01 = "Prefabs/Effect/KendoClub/KendoClubEffect01.prefab";
    public const string KENDO_CLUB_EFFECT_ATTACK_02 = "Prefabs/Effect/KendoClub/KendoClubEffect02.prefab";
    public const string KENDO_CLUB_EFFECT_ATTACK_03 = "Prefabs/Effect/KendoClub/KendoClubEffect03.prefab";
    public const string KENDO_CLUB_EFFECT_ATTACK_04 = "Prefabs/Effect/KendoClub/KendoClubEffect04.prefab";
    public const string KENDO_CLUB_EFFECT_ATTACK_05 = "Prefabs/Effect/KendoClub/KendoClubEffect05.prefab";
    public const string KENDO_CLUB_DRAW_BATTLE_EFFECT = "Prefabs/Effect/KendoClub/KendoClubEffectDraw.prefab";

}
