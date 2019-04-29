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

        DontDestroyOnLoad(this.gameObject);
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
    Type1AdolescentGreen = 0,
    Type1AdolescentPurple = 1,
    Type1AdolescentRed = 2,
    Type1BabyGreen = 3,
    Type1BabyPurple = 4,
    Type1BabyRed = 5,
    Type2AdolescentGreen = 6,
    Type2AdolescentPurple = 7,
    Type2AdolescentRed = 8,
    Type2BabyGreen = 9,
    Type2BabyPurple = 10,
    Type2BabyRed = 11
}

public enum DragonType
{
    Type1 = 0,
    Type2 = 1
}

public enum DragonColor
{
    Green,
    Purple,
    Red
}
