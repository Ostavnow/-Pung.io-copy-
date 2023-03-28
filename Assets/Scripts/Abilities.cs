using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class Abilities : MonoBehaviour
{
    public delegate float[] AbilityDelegate(PlayerController playerController,bool isActtive);
    public static AbilityDelegate[] abilitiesDelegate = new AbilityDelegate[33];
    [SerializeField]
    public List<Ability> abilities = new List<Ability>();
    public GameObject prefabAbilityCell;
    void Awake()
    {
        abilitiesDelegate[0] = Dush;
        abilitiesDelegate[1] = PunchSwarm;
        abilitiesDelegate[2] = LongPunch;
        abilitiesDelegate[3] = Fourarms;
        abilitiesDelegate[4] = BigPlayer;
        abilitiesDelegate[5] = ExsperienceBoost;
        abilitiesDelegate[6] = Shield;
    }
    //numdrs[0] = Time of action; numbers[1] = recovery Time;
    public static float[] Dush(PlayerController player,bool isActive)
    {
        float [] numbers = new float[2];
        numbers[0] = 0.4f;
        numbers[1] = 15f;
        if(isActive)
        {
            player.speed = 15f;
            player.isActiveDash = true;
        }
        else
        {
            player.speed = 5f;
            player.isActiveDash = false;
        }
        return numbers;
    }
    public static float[] PunchSwarm(PlayerController player,bool isActive)
    {
        float [] numbers = new float[2];
        numbers[0] = 10f;
        numbers[1] = 25f;
        if(isActive)
        {
            player.isActivePunchSwarm = true;
        }
        else
        {
            player.isActivePunchSwarm = false;
        }
        return numbers;
    }
    public static float[] LongPunch(PlayerController player,bool isActive)
    {
        float [] numbers = new float[2];
        numbers[0] = 10f;
        numbers[1] = 25f;
        if(isActive)
        {
            player.flightDistance = 20f;
        }
        else
        {
            player.flightDistance = 2.5f;
        }
        return numbers;
    }
    public static float[] Fourarms(PlayerController player,bool isActive)
    {
        float [] numbers = new float[2];
        numbers[0] = 10f;
        numbers[1] = 25f;
        if(isActive)
        {
            player.isFourarms = true;
        }
        else
        {
            player.isFourarms = false;
        }
        return numbers;
    }
    public static float[] BigPlayer(PlayerController player,bool isActive)
    {
        float [] numbers = new float[2];
        numbers[0] = 10f;
        numbers[1] = 20f;
        if(isActive)
        {
            player.gameObject.transform.localScale = new Vector3(2f,2f,1f);
            player.hand.transform.localScale = new Vector3(2f,2f,1f);
        }
        else
        {
            player.gameObject.transform.localScale = new Vector3(1f,1f,1f);
            player.hand.transform.localScale = new Vector3(1f,1f,1f);
        }
        return numbers;
    }
    public static float[] ExsperienceBoost(PlayerController player,bool isActive)
    {
        float [] numbers = new float[2];
        numbers[0] = 10f;
        numbers[1] = 20f;
        if(isActive)
        {
            player.player.experienceImprovementMultiplier = 2f;
        }
        else
        {
            player.player.experienceImprovementMultiplier = 1f;
        }
        return numbers;
    }
    public static float[] Shield(PlayerController player,bool isActive)
    {
        float [] numbers = new float[2];
        numbers[0] = 5f;
        numbers[1] = 15f;
        if(isActive)
        {
            player.shield.SetActive(true);
        }
        else
        {
            player.shield.SetActive(false);
        }
        return numbers;
    }
}
public enum AbilitiesEnum
{
    Dash,PunchSwarm,LongPunch,
    Fourarms,BigPlayer,EXPBoost,
    Shield,IcePunch,Magnet,
    ThanosBlackHole,STRBoost,ElectricPunch,
    Invisible,RocketPunch,Gear,
    Heart,NuclearPunch,
    SpinAttack,IceDash,Genie,
    Robot,RobotV2,RoboticPunch,
    PortalOfTheLegends,ElectroWave,
    TimePortal,BarrelTransform,
    ReflectPunch,AbilityStoppingBomb,
    GasSplash,FirePunch,Smoke,
    AGIBoost
}
