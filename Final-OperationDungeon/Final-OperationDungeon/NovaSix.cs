using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Final_OperationDungeon
{
    class NovaSix : Character
    {
        /// <summary>
        /// Makes Nova 6 Crawler who he is
        /// </summary>
        public NovaSix(GroupBox box, PictureBox picture):
            base (2, 10, 20, "Nova Six", 10, "Bad", true, box, picture)
        {
            AttackName = "6 Spit";
            SpecialName = "Nova Explosion";
            picture.BackgroundImage = global::Final_OperationDungeon.Properties.Resources.NovaSixCrawler;
            picture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
        }

        /// <summary>
        /// Nova 6's attack
        /// </summary>
        /// <param name="target"></param>
        public override void Attack(Character target)
        {
            target.HitPoints -= Strength;
        }

        /// <summary>
        /// Nova 6's  special attack
        /// </summary>
        /// <param name="target"></param>
        public override void Special(Character target)
        {
            target.HitPoints -= Strength * 3;
        }

        /// <summary>
        /// Nova 6's defense
        /// </summary>
        /// <param name="target"></param>
        public override void Defend(Character target)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Nova 6 taking a turn
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