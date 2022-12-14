using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Final_OperationDungeon
{
    class Brutus : Character
    {
        /// <summary>
        /// Makes Brutus who he is
        /// </summary>
        public Brutus(GroupBox box, PictureBox picture):
            base (15, 10, 5, "Brutus", 25, "Bad", true, box, picture)
        {
            AttackName = "Earthquake";
            SpecialName = "Perks Be Gone";
            picture.BackgroundImage = global::Final_OperationDungeon.Properties.Resources.Brutus;
            picture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
        }

        /// <summary>
        /// Brutus's attack
        /// </summary>
        /// <param name="target"></param>
        public override void Attack(Character target)
        {
            target.HitPoints -= 5;
        }

        /// <summary>
        /// Brutus's special attack
        /// </summary>
        /// <param name="target"></param>
        public override void Special(Character target)
        {
            target.HitPoints -= Strength;
        }

        /// <summary>
        /// Brutus's defense
        /// </summary>
        /// <param name="target"></param>
        public override void Defend(Character target)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Brutus taking a turn
        /// </summary>
        /// <param name="target"></param>
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