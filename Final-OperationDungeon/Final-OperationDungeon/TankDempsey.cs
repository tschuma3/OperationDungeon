using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Final_OperationDungeon
{
    class TankDempsey : Character
    {
        /// <summary>
        /// Makes Tank who he is
        /// </summary>
        public TankDempsey(GroupBox box, PictureBox picture):
            base (15, 5, 5, "Tank Dempsey", 20, "Good", true, box, picture)
        {
            AttackName = "Grenade";
            SpecialName = "Juggernog";
            picture.BackgroundImage = global::Final_OperationDungeon.Properties.Resources.TankDempsey;
            picture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
        }

        /// <summary>
        /// Tank's attack
        /// </summary>
        /// <param name="target"></param>
        public override void Attack(Character target)
        {
            target.HitPoints -= Strength / 3;
        }

        /// <summary>
        /// Tank's special attack
        /// </summary>
        /// <param name="target"></param>
        public override void Special(Character target)
        {
            target.HitPoints -= Strength;
        }

        /// <summary>
        /// Tank's defense
        /// </summary>
        /// <param name="target"></param>
        public override void Defend(Character target)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Tank taking a turn
        /// </summary>
        /// <param name="target"></param>
        public override void TakeTurn(Character target)
        {
            throw new NotImplementedException();
        }
    }
}