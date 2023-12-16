using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UI;
namespace AbilitiesSystem
{
    [RequireComponent(typeof(AbilityUI))]
    [RequireComponent(typeof(AbilityEditorData))]
    [System.Serializable]
    public class Abilities : MonoBehaviour
    {
        [SerializeField]
        public List<Ability> _listAbilities = new List<Ability>();
        private void OnEnable()
        {

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
}