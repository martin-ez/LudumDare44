using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] dragonPrefabs;
    public DragonIndex dragonBabyIndex = DragonIndex.Type1BabyRed;
    public DragonIndex dragonAdolescentIndex = DragonIndex.Type1AdolescentRed;
    public string dragonName;

    public static GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        if (gameManager == null)
        {
            gameManager = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDragon(DragonType type, DragonColor color)
    {
        switch (type)
        {
            case DragonType.Type1:
                switch (color)
                {
                    case DragonColor.Green:
                        dragonBabyIndex = DragonIndex.Type1BabyGreen;
                        dragonAdolescentIndex = DragonIndex.Type1AdolescentGreen;
                        break;
                    case DragonColor.Purple:
                        dragonBabyIndex = DragonIndex.Type1BabyPurple;
                        dragonAdolescentIndex = DragonIndex.Type1AdolescentPurple;
                        break;
                    case DragonColor.Red:
                        dragonBabyIndex = DragonIndex.Type1BabyRed;
                        dragonAdolescentIndex = DragonIndex.Type1AdolescentRed;
                        break;
                    default:
                        break;
                }
                break;
            case DragonType.Type2:
                switch (color)
                {
                    case DragonColor.Green:
                        dragonBabyIndex = DragonIndex.Type2BabyGreen;
                        dragonAdolescentIndex = DragonIndex.Type2AdolescentGreen;
                        break;
                    case DragonColor.Purple:
                        dragonBabyIndex = DragonIndex.Type2BabyPurple;
                        dragonAdolescentIndex = DragonIndex.Type2AdolescentPurple;
                        break;
                    case DragonColor.Red:
                        dragonBabyIndex = DragonIndex.Type2BabyRed;
                        dragonAdolescentIndex = DragonIndex.Type2AdolescentRed;
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
    }
}

public enum DragonIndex
{
    Type1AdolescentGreen,
    Type1AdolescentPurple,
    Type1AdolescentRed,
    Type1BabyGreen,
    Type1BabyPurple,
    Type1BabyRed,
    Type2AdolescentGreen,
    Type2AdolescentPurple,
    Type2AdolescentRed,
    Type2BabyGreen,
    Type2BabyPurple,
    Type2BabyRed
}

public enum DragonType
{
    Type1,
    Type2
}

public enum DragonColor
{
    Green,
    Purple,
    Red
}
