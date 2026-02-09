using System.Collections.Generic;
using UnityEngine;

public class SolutionOne : MonoBehaviour
{
    [Header("Character Info")]
    public string characterName;
    public string characterClass;
    public string race;
    public int level;
    public int constitutionScore;

    [Header("Feats")]
    public bool hasTough;
    public bool hasStout;

    [Header("HP Calculation")]
    public bool useAveragedHP; // true = averaged, false = rolled

    private Dictionary<string, int> classHitDice;

    void Start()
    {
        InitializeHitDice();
        CalculateHP();
    }

    void InitializeHitDice()
    {
        classHitDice = new Dictionary<string, int>()
        {
            { "Barbarian", 12 },
            { "Fighter", 10 },
            { "Paladin", 10 },
            { "Ranger", 10 },
            { "Cleric", 8 },
            { "Rogue", 8 },
            { "Bard", 8 },
            { "Druid", 8 },
            { "Monk", 8 },
            { "Wizard", 6 },
            { "Sorcerer", 6 }
        };
    }

    void CalculateHP()
    {
        int hitDie = classHitDice[characterClass];
        int conModifier = (constitutionScore - 10) / 2;
        int totalHP = 0;

        for (int i = 1; i <= level; i++)
        {
            int hpGain;

            if (i == 1)
            {
                hpGain = hitDie + conModifier;
            }
            else
            {
                if (useAveragedHP)
                {
                    float expectedValue = (hitDie + 1) / 2f;
                    hpGain = Mathf.FloorToInt(expectedValue) + conModifier;
                }
                else
                {
                    hpGain = Random.Range(1, hitDie + 1) + conModifier;
                }
            }

            totalHP += hpGain;
        }

        // Race bonus
        if (race == "Dwarf")
        {
            totalHP += 2 * level;
        }
        else if (race == "Orc" || race == "Goliath")
        {
            totalHP += 1 * level;
        }

        // Feat bonuses
        if (hasTough)
        {
            totalHP += 2 * level;
        }

        if (hasStout)
        {
            totalHP += 1 * level;
        }

        Debug.Log($"{characterName} has {totalHP} HP.");
    }
}
