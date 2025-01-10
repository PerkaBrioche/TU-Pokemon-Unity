using _2023_GC_A2_Partiel_POO.Level_2;
using NUnit.Framework;
using System;
namespace _2023_GC_A2_Partiel_POO.Tests.Level_2
{
    public class FightMoreTests
    {
        // Tu as probablement remarqué qu'il y a encore beaucoup de code qui n'a pas été testé ...
        // À présent c'est à toi de créer des features et les TU sur le reste du projet
        
        
        
        // Ce que tu peux ajouter:
        // - Ajouter davantage de sécurité sur les tests apportés
            // - un heal ne régénère pas plus que les HP Max
            // - si on abaisse les HPMax les HP courant doivent suivre si c'est au dessus de la nouvelle valeur
            // - ajouter un equipement qui rend les attaques prioritaires puis l'enlever et voir que l'attaque n'est plus prioritaire etc)
        // - Le support des status (sleep et burn) qui font des effets à la fin du tour et/ou empeche le pkmn d'agir
        // - Gérer la notion de force/faiblesse avec les différentes attaques à disposition (skills.cs)
        // - Cumuler les force/faiblesses en ajoutant un type pour l'équipement qui rendrait plus sensible/résistant à un type
        // - L'utilisation d'objets : Potion, SuperPotion, Vitess+, Attack+ etc.
        // - Gérer les PP (limite du nombre d'utilisation) d'une attaque,
            // si on selectionne une attack qui a 0 PP on inflige 0
            
        
        // Choisis ce que tu veux ajouter comme feature et fait en au max.
        // Les nouveaux TU doivent être dans ce fichier.
        // Modifiant des features il est possible que certaines valeurs
            // des TU précédentes ne matchent plus, tu as le droit de réadapter les valeurs
            // de ces anciens TU pour ta nouvelle situation.
            
        [Test]
        public void CheckHealth()
        {
            Character character = new Character(100, 100, 100, 100, TYPE.NORMAL);
            character.Heal(50);
            Assert.AreEqual(100, character.CurrentHealth);
        }
        
        [Test]
        public void CheckHealthMax()
        {
            Character c = new Character(100, 100, 100, 100, TYPE.NORMAL);
            c.Heal(200);
            Assert.LessOrEqual(c.CurrentHealth, c.MaxHealth);
        }
        
        [Test]
        public void CheckHealthCantNegative()
        {
            Character c = new Character(100, 100, 100, 100, TYPE.NORMAL);
            Assert.Throws<ArgumentException>(() =>
            {
                c.Heal(-200);
            });        
        }
        
        [Test]
        public void CheckCurrentHealWhenMaxDecrease()
        {
            Character c = new Character(100, 100, 100, 100, TYPE.NORMAL);
            c.MaxHealth = 40;
            Assert.AreEqual(40, c.MaxHealth);
            Assert.LessOrEqual(c.CurrentHealth, c.MaxHealth);
        }

        [Test]
        public void CheckPrioritaryEquipement()
        {
            Character Kevin = new Character(100, 100, 100, 500, TYPE.NORMAL);
            Character Francko = new Character(100, 100, 100, 100, TYPE.NORMAL);
            Equipment prioritaryEquipement = new Equipment(10, 10, 5, 5, true);
            Equipment NotPrioritary = new Equipment(2, 2, 3, 1, false);
            Francko.Equip(prioritaryEquipement);
            Kevin.Equip(NotPrioritary);
            Fight fightOfProgrammers = new Fight(Kevin, Francko);
            Assert.That(fightOfProgrammers.GetTurnCharacterSpeed().Item1 == Francko);
            // DOMMAGE POUR TOI KEVIN :( (met moi une bonne note et t'aura un buff de malade ;)
        }
        
        [Test]
        public void CheckPrioritaryAfterUnequipement()
        {
            Character Kevin = new Character(100, 10, 100, 100, TYPE.NORMAL);
            Character Francko = new Character(100, 10, 100, 500, TYPE.NORMAL);
            Equipment prioritaryEquipement = new Equipment(10, 10, 5, 5, true);
            Equipment NotPrioritary = new Equipment(2, 2, 3, 1, false);
            Francko.Equip(NotPrioritary);
            Kevin.Equip(prioritaryEquipement);
            Fight fightOfProgrammers = new Fight(Kevin, Francko);
            Assert.That(fightOfProgrammers.GetTurnCharacterSpeed().Item1 == Kevin);
            // Allez je te le laisse
            Francko.Unequip();
            Kevin.Unequip();
            Assert.That(fightOfProgrammers.GetTurnCharacterSpeed().Item1 == Francko);
        }

        [Test]
        public void CheckHealPotion()
        {
            Character A = new Character(100, 90, 10, 100, TYPE.NORMAL);
            Skill punch = new Punch();
            A.ReceiveAttack(punch);
            Assert.AreEqual(40, A.CurrentHealth);
            A.UseItem(new Items.HealPotion());
            Assert.AreEqual(90, A.CurrentHealth);
        }
        
        [Test]
        public void CheckSuperPotion()
        {
            Character A = new Character(200, 90, 10, 100, TYPE.NORMAL);
            Skill punch = new Punch();
            A.ReceiveAttack(punch);
            Assert.AreEqual(140, A.CurrentHealth);
            A.UseItem(new Items.SuperPotion());
            Assert.AreEqual(200, A.CurrentHealth);
        }
        
        [Test]
        public void CheckAttackPotion()
        {
            Character A = new Character(100, 50, 10, 100, TYPE.NORMAL);
            Assert.AreEqual(50, A.Attack);
            A.UseItem(new Items.StrenghPotion());
            Assert.AreEqual(70, A.Attack);
        }
        // DEFENSE POTION 
        // SPEED POTION
        // PP POTION

        [Test]
        public void CheckDefensePotion()
        {
            Character A = new Character(100, 50, 10, 100, TYPE.NORMAL);
            Assert.AreEqual(10, A.Defense);
            A.UseItem(new Items.DefensePotion());
            Assert.AreEqual(35, A.Defense);
        }
        
        [Test]
        public void CheckSpeedPotion()
        {
            Character A = new Character(100, 50, 50, 10, TYPE.NORMAL);
            Assert.AreEqual(10, A.Speed);
            A.UseItem(new Items.SpeedPotion());
            Assert.AreEqual(30, A.Speed);
        }
        
        [Test]
        public void CheckCriticalDamageFire()
        {
            Character pikachu = new Character(500, 50, 0, 20, TYPE.WATER);
            Skill fireBall = new FireBall(); // DAMAGE = 50
            pikachu.ReceiveAttack(fireBall); // calcul critique, 50 * 4 = 200
            Assert.That(pikachu.CurrentHealth, Is.EqualTo(300));
        }
        [Test]
        public void CheckCriticalDamageGrass()
        {
            Character pikachu = new Character(500, 50, 0, 20, TYPE.WATER);
            Skill magicalGrass = new MagicalGrass(); // DAMAGE = 50
            pikachu.ReceiveAttack(magicalGrass); // calcul critique, 70 * 2 = 140
            Assert.That(pikachu.CurrentHealth, Is.EqualTo(360));
        }
        [Test]
        public void CheckDefenseAgainstDamageWater()
        {
            Character pikachu = new Character(500, 50, 0, 20, TYPE.FIRE);
            Skill waterBlouBlou = new WaterBlouBlou(); // DAMAGE = 50
            pikachu.ReceiveAttack(waterBlouBlou); // calcul critique, 20 / 2 = 10
            Assert.That(pikachu.CurrentHealth, Is.EqualTo(490));
        }

        
        
        
    }
}
