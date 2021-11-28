using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] PlayerStats playerStats;
    public ExperienceController expControl;

    private static GameManager GM;
    private Fader fader;

    public int currentSouls = PlayerCollectibles.instance.soulNumber;



    private void Awake()
    {
        if (GM == null)
        {
            GM = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
    }

    public static void RegisterFader(Fader fd)
    {
        if (GM == null)
            return;
        GM.fader = fd;
    }

    public static void ManagnerLoadLevel(int index)
    {
        if (GM == null)
            return;
        GM.fader.SetLevel(index);
    }

    public static void ManagerRestartLevel()
    {
        if (GM == null)
            return;
        GM.fader.RestartLevel();
    }

    public static void ManagerLoadLevel(int index)
    {
        if (GM == null)
            return;
        //GM.fader.SetLevel(index);
        GM.fader.SetLevel(index);
        
    }

    public PlayerStats GetPlayerStats()
    {
        return playerStats;
    }
    public ExperienceController GetExperienceControllers()
    {
        return expControl;
    }

}
