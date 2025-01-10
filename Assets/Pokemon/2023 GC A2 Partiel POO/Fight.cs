
using System;

namespace _2023_GC_A2_Partiel_POO.Level_2
{
    public class Fight
    {
        public Fight(Character character1, Character character2)
        {
            if (character1 == null || character2 == null)
            {
                throw new ArgumentNullException("Les deux pokemon doivent être non null");
            }

            Character1 = character1;
            Character2 = character2;
        }

        public Character Character1 { get; }
        public Character Character2 { get; }
        /// <summary>
        /// Est-ce la condition de victoire/défaite a été rencontré ?
        /// </summary>
        public bool IsFightFinished => !Character1.IsAlive || !Character2.IsAlive;

        /// <summary>
        /// Jouer l'enchainement des attaques. Attention à bien gérer l'ordre des attaques par apport à la speed des personnages
        /// </summary>
        /// <param name="skillFromCharacter1">L'attaque selectionné par le joueur 1</param>
        /// <param name="skillFromCharacter2">L'attaque selectionné par le joueur 2</param>
        /// <exception cref="ArgumentNullException">si une des deux attaques est null</exception>
        public void ExecuteTurn(Skill skillFromCharacter1, Skill skillFromCharacter2)
        {
            Character1.skillUsed = skillFromCharacter1;
            Character2.skillUsed = skillFromCharacter2;
            if (skillFromCharacter1 == null || skillFromCharacter2 == null)
            {
                throw new ArgumentNullException("Les deux attaques doivent être non null");
            }
            
            var fastestCharacter = GetTurnCharacterSpeed().Item1;
            var slowestCharacter = GetTurnCharacterSpeed().Item2;

           // if (fastestCharacter.CurrentStatus.CanAttack) // SI L'EFFET SUR LE POKEMON LUI PERMET D'ATTAQUER
          //  {
                slowestCharacter.ReceiveAttack(fastestCharacter.skillUsed);
           // }
            if (!slowestCharacter.IsAlive)
            {
                return;
            }
           // if (slowestCharacter.CurrentStatus.CanAttack) // SI L'EFFET SUR LE POKEMON LUI PERMET D'ATTAQUER
          //  {
                fastestCharacter.ReceiveAttack(slowestCharacter.skillUsed);
           // }
            //fastestCharacter.ApplyEffect();
            //slowestCharacter.ApplyEffect();
        }

        public (Character, Character) GetTurnCharacterSpeed()
        {
            if (Character1.CurrentEquipment is { IsPrioritary : true } &&
                Character2.CurrentEquipment is { IsPrioritary: false })
            {
                // PRIORITE AU CHARACTER 1
                return  (Character1, Character2);
            }
            else if(     Character2.CurrentEquipment is { IsPrioritary : true } && 
                         Character1.CurrentEquipment is { IsPrioritary: false })
            {
                // PRIORITE AU CHARACTER 1
                return  (Character2, Character1);
            }
            // RETOURNE LE PLUS RAPIDE EN PREMS ET LE PLUS LENT EN DEUXIEME

            return Character1.Speed > Character2.Speed ? (Character1, Character2) : (Character2, Character1);
        }
    }
}
