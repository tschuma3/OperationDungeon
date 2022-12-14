using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Final_OperationDungeon
{
    class TakeoMasaki : Character
    {
        /// <summary>
        /// Makes Takeo who he is
        /// </summary>
        public TakeoMasaki(GroupBox box, PictureBox picture):
            base (5, 15, 15, "Takeo Masaki", 20, "Good", true, box, picture)
        {
            AttackName = "Quick Revivial";
            SpecialName = "Path of Sorrows";
            picture.BackgroundImage = global::Final_OperationDungeon.Properties.Resources.TakeoMasaki;
            picture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
        }

        /// <summary>
        /// Takeo's attack
        /// </summary>
        /// <param name="target"></param>
        public override void Attack(Character target)
        {
            target.HitPoints += Strength * 2;
        }

        /// <summary>
        /// Takeo's special attack
        /// </summary>
        /// <param name="target"></param>
        public override void Special(Character target)
        {
            target.HitPoints -= Strength * 2;
        }

        /// <summary>
        /// Takeo's defense
        /// </summary>
        /// <param name="target"></param>
        public override void Defend(Character target)
        {
            throw new NotImplementedException();
        }

        public override void TakeTurn(Character target)
        {
            throw new NotImplementedException();
        }
    }
}