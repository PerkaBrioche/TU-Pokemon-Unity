using System;

namespace _2023_GC_A2_Partiel_POO.Level_2
{
    /// <summary>
    /// Définition d'un personnage
    /// </summary>
    public class Character
    {
        public StatusEffect CurrentStatus { get; set; }

        /// <summary>
        /// Stat de base, HP
        /// </summary>
        int _baseHealth;
        /// <summary>
        /// Stat de base, ATK
        /// </summary>
        int _baseAttack;
        /// <summary>
        /// Stat de base, DEF
        /// </summary>
        int _baseDefense;
        /// <summary>
        /// Stat de base, SPE
        /// </summary>
        int _baseSpeed;
        /// <summary>
        /// Type de base
        /// </summary>
        TYPE _baseType;

        public Skill skillUsed;
        public Character(int baseHealth, int baseAttack, int baseDefense, int baseSpeed, TYPE baseType)
        {
            _baseHealth = baseHealth;
            _baseAttack = baseAttack;
            _baseDefense = baseDefense;
            _baseSpeed = baseSpeed;
            _baseType = baseType;
            
            SetCharacterInfo();
            CurrentHealth = _baseHealth;
        }

        private void SetCharacterInfo()
        {
            MaxHealth = _baseHealth;
            Attack = _baseAttack;
            Defense = _baseDefense;
            Speed = _baseSpeed;
            BaseType = _baseType;
        }

        /// <summary>
        /// HP actuel du personnage
        /// </summary>
        public int CurrentHealth;

        public TYPE BaseType;

        /// <summary>
        /// HPMax, prendre en compte base et equipement potentiel
        /// </summary>
        private int _maxHealth;
        public int MaxHealth
        {
            get
            {
                return _maxHealth;
            }
            set
            {
                _maxHealth = value;
                if(_maxHealth < 0)
                {
                    throw new ArgumentException("MaxHealth can't be negative");
                }
                if(_maxHealth < CurrentHealth)
                {
                    CurrentHealth = MaxHealth;
                }
            }
        }

        /// <summary>
        /// ATK, prendre en compte base et equipement potentiel
        /// </summary>
        public int Attack;

        /// <summary>
        /// DEF, prendre en compte base et equipement potentiel
        /// </summary>
        public int Defense;

        /// <summary>
        /// SPE, prendre en compte base et equipement potentiel
        /// </summary>
        ///
        public int speed;
        public int Speed
        {
            get
            {
                return speed;
            }
            set
            {
                if (speed <= 1)
                {
                    speed = 1;
                }

                speed = value;
            }
        }
        /// <summary>
        /// Equipement unique du personnage
        /// </summary>
        public Equipment CurrentEquipment { get; private set; }
        /// <summary>
        /// null si pas de status
        /// </summary>
        public bool IsAlive => CurrentHealth > 0;


        /// <summary>
        /// Application d'un skill contre le personnage
        /// On pourrait potentiellement avoir besoin de connaitre le personnage attaquant,
        /// Vous pouvez adapter au besoin
        /// </summary>
        /// <param name="s">skill attaquant</param>
        /// <exception cref="NotImplementedException"></exception>
        public void ReceiveAttack(Skill s)
        {
            CurrentHealth -= GetDamageOfSkill(s);
            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
            }
        }
        
        private int GetDamageOfSkill(Skill s)
        {
            float damageMultiplier = TypeResolver.GetFactor(s.Type, BaseType);
            float finalDamage = s.Power * damageMultiplier;
            return ((int)finalDamage - Defense);
        }
        
        /// <summary>
        /// Equipe un objet au personnage
        /// </summary>
        /// <param name="newEquipment">equipement a appliquer</param>
        /// <exception cref="ArgumentNullException">Si equipement est null</exception>
        public void Equip(Equipment newEquipment)
        {

            
            if (newEquipment == null)
            {
                throw new ArgumentNullException();
            }
            
            CurrentEquipment = newEquipment;
            
            _baseHealth += CurrentEquipment.BonusHealth;
            _baseAttack += CurrentEquipment.BonusAttack;
            _baseDefense += CurrentEquipment.BonusDefense;
            _baseSpeed += CurrentEquipment.BonusSpeed;
            
            SetCharacterInfo();
        }
        /// <summary>
        /// Desequipe l'objet en cours au personnage
        /// </summary>
        public void Unequip()
        {
            _baseHealth -= CurrentEquipment.BonusHealth;
            _baseAttack -= CurrentEquipment.BonusAttack;
            _baseDefense -= CurrentEquipment.BonusDefense;
            _baseSpeed -= CurrentEquipment.BonusSpeed;
            SetCharacterInfo();
            CurrentEquipment = null;
        }


        public void Heal(int value)
        {
            if(value < 0)
            {
                throw new ArgumentException("Heal value can't be negative");
            }
            
            CurrentHealth += value;
            if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
        }

        public void ApplyEffect()
        {
            // BURN EFFECT
            CurrentHealth -= CurrentStatus.DamageEachTurn;
            // GLUE EFFECT
            Speed -= CurrentStatus.SpeedReducedEndRound;

        }

        public void UseItem(Item item)
        {
            switch (item.Effect)
            {
                case Item.ItemEffect.Heal:
                    Heal(item.BonusValue);
                    break;
                case Item.ItemEffect.Speed:
                    Speed += item.BonusValue;
                    break;
                case Item.ItemEffect.Strength:
                    Attack += item.BonusValue;
                    break;
                case Item.ItemEffect.Defense:
                    Defense += item.BonusValue;
                    break;
            }
            
        }
    }
}
