using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Abilities : MonoBehaviour
{
    [SerializeField]
    public List<Ability> _abilities = new List<Ability>();
    public GameObject _prefabAbilityCell;
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
