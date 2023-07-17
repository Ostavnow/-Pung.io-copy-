using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Abilities : MonoBehaviour
{
    public delegate Closures AbilityDelegate(PlayerController playerController,bool isActtive);
    public static AbilityDelegate[] _abilitiesDelegate = new AbilityDelegate[33];
    [SerializeField]
    public List<Ability> _abilities = new List<Ability>();
    public delegate float[] Closures();
    public GameObject _prefabAbilityCell;
    void Awake()
    {
        _abilitiesDelegate[0] = Dush;
        _abilitiesDelegate[1] = PunchSwarm;
        _abilitiesDelegate[2] = LongPunch;
        _abilitiesDelegate[3] = Fourarms;
        _abilitiesDelegate[4] = BigPlayer;
        _abilitiesDelegate[5] = ExsperienceBoost;
        _abilitiesDelegate[6] = Shield;
    }
    //numdrs[0] = Time of action; numbers[1] = recovery Time;
    public static Closures Dush(PlayerController player,bool isActive)
    {
        float [] numbers = new float[2];
        numbers[0] = 0.4f;
        numbers[1] = 15f;
        float[] Dush()
        {
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
        return Dush;
    }
    public static Closures PunchSwarm(PlayerController player,bool isActive)
    {
        float [] numbers = new float[2];
        numbers[0] = 10f;
        numbers[1] = 25f;
        float [] PunchSwarm()
        {
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
        return PunchSwarm;
    }
    public static Closures LongPunch(PlayerController player,bool isActive)
    {
        float [] numbers = new float[2];
        numbers[0] = 10f;
        numbers[1] = 25f;
        float [] LongPunch()
        {
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
        return LongPunch;
    }
    public static Closures Fourarms(PlayerController player,bool isActive)
    {
        float [] numbers = new float[2];
        numbers[0] = 10f;
        numbers[1] = 25f;
        bool isWillNextBlowBeRightSide = true;
        float timeFourarms = 1;
        float[] FourarmsClosures()
        {
        timeFourarms += Time.deltaTime;
        if(isActive & timeFourarms > player.timeAfterWhichBeNextBlow)
        {
            timeFourarms = 0;
            Vector3 randomPosition;
            float handRadius = player.hand.GetComponent<CircleCollider2D>().radius;
            Vector3 randomValue = new Vector3(Random.Range(-0.25f,0.25f),Random.Range(-0.25f,0.25f));
            if(isWillNextBlowBeRightSide)
            {
                randomPosition = new Vector3(player.handRightPoint.position.x + randomValue.x,player.handRightPoint.position.y + randomValue.y,0);
                isWillNextBlowBeRightSide = false;
                player.HandCreate(randomPosition,ref isWillNextBlowBeRightSide);
            }
            else
            {
                randomPosition = new Vector3(player.handLeftPoint.position.x + randomValue.x,player.handLeftPoint.position.y + randomValue.y,0);
                isWillNextBlowBeRightSide = true;
                player.HandCreate(randomPosition,ref isWillNextBlowBeRightSide);
            }
        }
        else
        {

        }
        return numbers;
        }
        return FourarmsClosures;
    }
    public static Closures BigPlayer(PlayerController player,bool isActive)
    {
        float [] numbers = new float[2];
        numbers[0] = 10f;
        numbers[1] = 20f;
        float []  BigPlayer()
        {
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
        return BigPlayer;
    }
    public static Closures ExsperienceBoost(PlayerController player,bool isActive)
    {
        float [] numbers = new float[2];
        numbers[0] = 10f;
        numbers[1] = 20f;
        float [] ExsperienceBoost()
        {
            if(isActive)
            {
                player.player._experienceImprovementMultiplier = 2f;
            }
            else
            {
                player.player._experienceImprovementMultiplier = 1f;
            }
        return numbers;
        }
        return ExsperienceBoost;
    }
    public static Closures Shield(PlayerController player,bool isActive)
    {
        float [] numbers = new float[2];
        numbers[0] = 5f;
        numbers[1] = 15f;
        float [] Shield()
        {
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
        return Shield;
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
