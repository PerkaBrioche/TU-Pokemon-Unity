
using System;
using UnityEngine;

namespace _2023_GC_A2_Partiel_POO.Level_2
{
    /// <summary>
    /// Définition des types dans le jeu
    /// </summary>
    public enum TYPE { NORMAL, WATER, FIRE, GRASS }

    public class TypeResolver : MonoBehaviour
    {

        /// <summary>
        /// Récupère le facteur multiplicateur pour la résolution des résistances/faiblesses
        /// WATER faible contre GRASS, resiste contre FIRE
        /// FIRE faible contre WATER, resiste contre GRASS
        /// GRASS faible contre FIRE, resiste contre WATER
        /// </summary>
        /// <param name="attacker">Type de l'attaque (le skill)</param>
        /// <param name="receiver">Type de la cible</param>
        /// <returns>
        /// Normal returns 1 if attacker or receiver
        /// 0.8 if resist
        /// 1.0 if same type
        /// 1.2 if vulnerable
        /// </returns>
        /// 
        public static float GetFactor(TYPE attacker, TYPE receiver)
        {
            switch (attacker)
            {
                case TYPE.NORMAL:
                    return 1.0f;
                case TYPE.WATER:
                    if (receiver == TYPE.GRASS)
                    {
                        return 2f;
                    }
                    if (receiver == TYPE.FIRE)
                    {
                        return 0.5f;
                    }
                    return 1.0f;
                case TYPE.FIRE:
                    if (receiver == TYPE.WATER)
                    {
                        return 4;
                    }
                    if (receiver == TYPE.GRASS)
                    {
                        return 3f;
                    }
                    return 1.0f;
                case TYPE.GRASS:
                    if (receiver == TYPE.FIRE)
                    {
                        return 2;
                    }
                    if (receiver == TYPE.WATER)
                    {
                        return 2;
                    }
                    return 1.0f;
                default:
                    return 1.0f;
            }
        }

    }
}
