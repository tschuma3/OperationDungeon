using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Final_OperationDungeon
{
    class NikolaiBelinski : Character
    {
        /// <summary>
        /// Makes Nikolai who he is
        /// </summary>
        public NikolaiBelinski(GroupBox box, PictureBox picture) :
            base(10, 10, 10, "Nikolai Belinski", 20, "Good", true, box, picture)
        {
            AttackName = "PPSh-41";
            SpecialName = "Ice Staff";
            picture.BackgroundImage = global::Final_OperationDungeon.Properties.Resources.NikolaiBelinski;
            picture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
        }

        /// <summary>
        /// Nikolai's attack
        /// </summary>
        /// <param name="target"></param>
        public override void Attack(Character target)
        {
            target.HitPoints -= Strength / 2;
        }

        /// <summary>
        /// Nikolai's special attack
        /// </summary>
        /// <param name="target"></param>
        public override void Special(Character target)
        {
            target.HitPoints -= Strength;
        }

        /// <summary>
        /// Nikolai's defense
        /// </summary>
        /// <param name="target"></param>
        public override void Defend(Character target)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Nikolai taking a turn
        /// </summary>
        /// <param name="target"></param>
        public override void TakeTurn(Character target)
        {
            throw new NotImplementedException();
        }
    }
}