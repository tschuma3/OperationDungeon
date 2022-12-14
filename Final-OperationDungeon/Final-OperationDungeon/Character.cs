using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Final_OperationDungeon
{
    abstract class Character
    {
        // Variables
        private int defense;
        private int hitPoints;
        private int intelligence;
        private int speed;
        private int spellPoints;
        private int strength;
        private string name;
        private string attackName;
        private string specialName;
        private string goodBad;
        private bool alive;
        public System.Windows.Forms.Label Labels { get; set; } = new Label();
        public System.Windows.Forms.PictureBox Picture { get; set; } = new PictureBox();

        #region Properties

        /// <summary>
        /// Gets and sets hit points
        /// </summary>
        public int HitPoints { get => hitPoints; set => hitPoints = value; }

        /// <summary>
        /// Gets and sets spell points
        /// </summary>
        public int SpellPoints { get => spellPoints; set => spellPoints = value; }

        /// <summary>
        /// Gets and sets speed
        /// </summary>
        public int Speed { get => speed; set => speed = value; }

        /// <summary>
        /// Gets and sets defense
        /// </summary>
        public int Defense { get => defense; set => defense = value; }

        /// <summary>
        /// Gets and sets strength
        /// </summary>
        public int Strength { get => strength; set => strength = value; }

        /// <summary>
        /// Gets and sets intelligence
        /// </summary>
        public int Intelligence { get => intelligence; set => intelligence = value; }

        /// <summary>
        /// Gets and sets the name of the character
        /// </summary>
        public string Name { get => name; set => name = value; }

        /// <summary>
        /// Gets and sets attack name
        /// </summary>
        public string AttackName { get => attackName; set => attackName = value; }

        /// <summary>
        /// Gets and sets special attack name
        /// </summary>
        public string SpecialName { get => specialName; set => specialName = value; }

        /// <summary>
        /// Gets and sets a character being good and bad
        /// </summary>
        public string GoodBad { get => goodBad; set => goodBad = value; }

        /// <summary>
        /// Gets and sets a character being dead or alive
        /// </summary>
        public bool Alive { get => alive; set => alive = value; }

        /// <summary>
        /// Eventhandler on attacked
        /// </summary>
        public EventHandler Attacked; 

        #endregion

        /// <summary>
        /// Gives the character characteristics
        /// </summary>
        /// <param name="strength"></param>
        /// <param name="intelligence"></param>
        /// <param name="speed"></param>
        /// <param name="name"></param>
        /// <param name="hitPoints"></param>
        /// <param name="goodBad"></param>
        public Character(int strength, int intelligence, int speed, string name, int hitPoints, string goodBad, bool alive, GroupBox box, PictureBox picture)
        {
            this.Strength = strength;
            this.Intelligence = intelligence;
            this.Speed = speed;
            this.Name = name;
            this.HitPoints = hitPoints;
            this.GoodBad = goodBad;
            this.Alive = true;
            Labels.Location = new System.Drawing.Point(50, 50 * box.Controls.Count + 20);
            box.Controls.Add(Labels);
            Picture = picture;
            Picture.Click += new System.EventHandler(this.buttonClick);
        }

        /// <summary>
        /// Clicking a character
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CharacterClick(object sender, EventArgs e)
        {
            Console.WriteLine(this.name);
        }

        /// <summary>
        /// Clicking a button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClick(object sender, EventArgs e)
        {
            this.CharacterClick(sender, e);

            if (this.Attacked != null)
            {
                this.Attacked.Invoke(this, new EventArgs());
            }
        }

        /// <summary>
        /// Attacking
        /// </summary>
        /// <param name="target"></param>
        public abstract void Attack(Character target);

        /// <summary>
        /// Defending
        /// </summary>
        public abstract void Defend(Character target);
 
        /// <summary>
        /// Special Attack
        /// </summary>
        /// <param name="target"></param>
        public abstract void Special(Character target);

        /// <summary>
        /// Takes a turn
        /// </summary>
        /// <param name="target"></param>
        public abstract void TakeTurn(Character target);

        public void Death()
        {
            throw new System.NotImplementedException();
        }

        public void TakeDamage()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Returns a name
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return name;
        }
    }
}