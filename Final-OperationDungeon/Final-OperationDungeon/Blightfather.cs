using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Final_OperationDungeon
{
    class Blightfather : Character
    {
        /// <summary>
        /// Makes Blightfather who he is
        /// </summary>
        public Blightfather(GroupBox box, PictureBox picture):
            base (20, 5, 2, "Blightfather", 40, "Bad", true, box, picture)
        {
            AttackName = "Spew";
            SpecialName = "Grab";
            picture.BackgroundImage = global::Final_OperationDungeon.Properties.Resources.BlightFather;
            picture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
        }

        /// <summary>
        /// Blightfather's attack
        /// </summary>
        /// <param name="target"></param>
        public override void Attack(Character target)
        {
            target.HitPoints -= Strength / 2;
        }

        /// <summary>
        /// Blightfather's special attack
        /// </summary>
        /// <param name="target"></param>
        public override void Special(Character target)
        {
            target.HitPoints -= Strength;
        }

        /// <summary>
        /// Blightfather's defense
        /// </summary>
        /// <param name="target"></param>
        public override void Defend(Character target)
        {
            throw new NotImplementedException();
        }

        public override void TakeTurn(Character target)
        {
            Random rand = new Random();
            if (rand.Next(2) % 2 == 0)
            {
                Attack(target);
            }
            else
            {
                Special(target);
            }
        }
    }
}