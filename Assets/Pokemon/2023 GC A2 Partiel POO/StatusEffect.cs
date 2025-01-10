
using System;
using UnityEngine;

namespace _2023_GC_A2_Partiel_POO.Level_2
{
    /// <summary>
    /// Enum des status de chaque attaque (voir plus bas)
    /// </summary>
    public enum StatusPotential
    {
        NONE, 
        SLEEP, 
        BURN, 
        CRAZY, 
        GLUER
    }
    public class StatusEffect 
    {
        /// <summary>
        /// Factory retournant un nouvel objet représentant le statut généré
        /// </summary>
        /// <param name="s">le statut à générer</param>
        /// <returns>Le statut à appliquer sur le character ciblé</returns>
        public static StatusEffect GetNewStatusEffect(StatusPotential s)
        {
            switch (s)
            {
                case StatusPotential.SLEEP:
                    return new SleepStatus();
                case StatusPotential.BURN:
                    return new BurnStatus();
                case StatusPotential.CRAZY:
                    return new CrazyStatus();
                case StatusPotential.NONE:
                    return new None();
                case StatusPotential.GLUER:
                    return new Glue();
                default:
                    return null;
            }
        }

        public StatusEffect GiveStatus(StatusPotential s)
        {
            return GetNewStatusEffect(s);
        }

        /// <summary>
        /// Un Status ne peut etre crée que par une classe enfant (voir plus bas)
        /// </summary>
        /// <param name="remainingTurn">Nombre de tour de l'effet</param>
        /// <param name="damageEachTurn">Nombre de dégât à la fin de chaque tour</param>
        /// <param name="canAttack">Le personnage peut-il attaquer ?</param>
        /// <param name="damageOnAttack">Portion de l'attaque auto-infligé au moment de l'attaque( 1f:100%, 0.5f:50% etc</param>
        protected StatusEffect(int remainingTurn, int damageEachTurn, bool canAttack, float damageOnAttack, int SpeedReducer = 0)
        {
            RemainingTurn = remainingTurn;
            DamageEachTurn = damageEachTurn;
            DamageOnAttack = damageOnAttack;
            CanAttack = canAttack;
            SpeedReducedEndRound = SpeedReducer;
        }

        /// <summary>
        /// Nombre de tour de l'effet
        /// </summary>
        public int RemainingTurn { get; protected set; }
        /// <summary>
        /// Nombre de dégât à la fin de chaque tour
        /// </summary>
        public int DamageEachTurn { get; protected set; }
        /// <summary>
        /// Le personnage peut-il attaquer ?
        /// </summary>
        public bool CanAttack { get; protected set; }
        /// <summary>
        /// Portion de l'attaque auto-infligé au moment de l'attaque( 1f:100%, 0.5f:50% etc
        /// </summary>
        public float DamageOnAttack { get; protected set; }
        /// <summary>
        /// Speed perdu à chaque fin de round
        /// </summary>
        public int SpeedReducedEndRound { get; protected set; }

        /// <summary>
        /// Méthode enclenché par le système de combat à la fin de chaque tour
        /// Vous pouvez ajouter du contenu si besoin
        /// </summary>
        public virtual void EndTurn()
        {
            if (RemainingTurn > 0)
            {
                RemainingTurn--;
            }
        }
    }

    /// <summary>
    /// Endormi, le personnage ne peut pas attaquer
    /// </summary>
    public class SleepStatus : StatusEffect
    {
        public SleepStatus() : base(5, 0, false, 0f)
        {
        }
    }
    public class None : StatusEffect
    {
        public None() : base(0, 0, true, 0f)
        {
        }
    }
    /// <summary>
    /// Brûlé, le personnage perd des points de vie à la fin de chaque tour
    /// </summary>
    public class BurnStatus : StatusEffect
    {
        public BurnStatus() : base(5, 10, true, 0f)
        {
        }
    }

    /// <summary>
    /// Folie, le personnage s'attaque contre-lui même (on skip la notion attaque-defense au profit d'une portion de la stat d'attaque
    /// </summary>
    public class CrazyStatus : StatusEffect
    {
        public CrazyStatus() : base(1, 0, false, 0.3f)
        {
            
        }
    }
    
    public class Glue : StatusEffect
    {
        // LA GLUE DURE 5 TOURS MAIS EST ENLEVE A CHAQUE FOIS QU'IL ATTAQUE
        public Glue() : base(5, 0, false, 0f, 15)
        {
            
        }
    }

}
