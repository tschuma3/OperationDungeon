using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Final_OperationDungeon
{
    class Gameplay
    {
        // Variables
        public List<Character> theQueue = new List<Character>();
        public int currentTurn = 0;
        public string queuedAction;
        public Form form;
        public int level = 0;
        public int score = 0;

        /// <summary>
        /// Gameplay
        /// </summary>
        public Gameplay(GroupBox box, GroupBox boxE, Panel picture)
        {
            // Adds characters to the queue
            var findPlayer1 = picture.Controls.Find("Player1", true);
            PictureBox player1 = (PictureBox)findPlayer1.ElementAt(0);
            var findPlayer2 = picture.Controls.Find("Player2", true);
            PictureBox player2 = (PictureBox)findPlayer2.ElementAt(0);
            var findPlayer3 = picture.Controls.Find("Player3", true);
            PictureBox player3 = (PictureBox)findPlayer3.ElementAt(0);
            theQueue.Add(new TakeoMasaki(box, player1));
            theQueue.Add(new NikolaiBelinski(box, player2));
            theQueue.Add(new TankDempsey(box, player3));
            AddEnemies(boxE, picture);

            // Sorts by speed
            theQueue = LINQSort();

            // For everyone in theQueue, displays name to the console
            for (int i = 0; i <theQueue.Count; i++)
            {
                Console.WriteLine(theQueue[i].Name);
            }
        }

        /// <summary>
        /// Target being clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TargetClicked(object sender, EventArgs e)
        {
             TakeTurn((Character)sender);
        }

        /// <summary>
        /// Takes care of when the game ends
        /// </summary>
        public void EndGame()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Takes care of the start of the game
        /// </summary>
        public void StartGame()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Takes the turn
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string TakeTurn(Character target)
        {
            switch (queuedAction)
            {
                case "attack": // Case for the attack
                    theQueue[currentTurn].Attack(target);
                    return $"{theQueue[currentTurn].Name} attacked {target.Name} with {theQueue[currentTurn].AttackName}!";
                case "special": // Case for the special attack
                    theQueue[currentTurn].Special(target);
                    return $"{theQueue[currentTurn].Name} attacked {target.Name} with {theQueue[currentTurn].SpecialName}!";
                default: // Default case
                    return "Nothing Happened";
            }
        }

        /// <summary>
        /// Checks if everyone is alive
        /// </summary>
        public void CheckAlive()
        {
            // For every character in theQueue
            for (int i = 0; i < theQueue.Count; i++)
            {
                // If the hit points are less than or equal to 0
                if (theQueue[i].HitPoints <= 0)
                {
                    // Make the picture invisible and set alive to false
                    theQueue[i].Picture.Visible = false;
                    theQueue[i].Alive = false;
                    
                    // If bad, adds to the score and displays to the console
                    if (theQueue[i].GoodBad == "Bad")
                    {
                        score += 10;
                        Console.WriteLine("Score: " + score);
                    }

                    // Removes the character and decrease i
                    theQueue.RemoveAt(i);
                    i--;
                }
            }
        }

        public void CheckLevel()
        {
            for (int i = 0; i < theQueue.Count; i++)
            {
                if (theQueue[i].GoodBad == "Bad" && theQueue.Count <= 3)
                {
                    level++;
                }
            }
            Console.WriteLine("Level: " + level);
        }

        /// <summary>
        /// Sorts who goes in what order
        /// </summary>
        /// <returns></returns>
        public List<Character> LINQSort()
        {
            var sorted = from c in theQueue
                         orderby c.Speed descending
                         select c;

            return sorted.ToList<Character>();
        }

        /// <summary>
        /// Adds enemies to each wave
        /// </summary>
        public void AddEnemies(GroupBox boxE, Panel pictureE)
        {
            // Variable
            Random rand = new Random();

            // Finds pictureboxes
            var findEnemy1 = pictureE.Controls.Find("Enemy1", true);
            PictureBox enemy1 = (PictureBox)findEnemy1.ElementAt(0);
            var findEnemy2 = pictureE.Controls.Find("Enemy2", true);
            PictureBox enemy2 = (PictureBox)findEnemy2.ElementAt(0);
            var findEnemy3 = pictureE.Controls.Find("Enemy3", true);
            PictureBox enemy3 = (PictureBox)findEnemy3.ElementAt(0);

            // Number of enemies
            int numberOfEnemies = rand.Next(1, 4);

            // For the number of enemies
            for (int i = 0; i < numberOfEnemies; i++)
            {
                // Random type
                int EnemyType = rand.Next(1, 10);

                // Based off the type, put the character in the correct picturebox
                if (EnemyType >= 4 && EnemyType < 6)
                {
                    if (i == 0)
                    {
                        theQueue.Add(new Blightfather(boxE, enemy1));
                        enemy1.Visible = true;
                    }
                    else if (i == 1)
                    {
                        theQueue.Add(new Blightfather(boxE, enemy2));
                        enemy2.Visible = true;
                    }
                    else
                    {
                        theQueue.Add(new Blightfather(boxE, enemy3));
                        enemy3.Visible = true;
                    }
                }
                else if (EnemyType < 4)
                {
                    if (i == 0)
                    {
                        theQueue.Add(new Brutus(boxE, enemy1));
                        enemy1.Visible = true;
                    }
                    else if (i == 1)
                    {
                        theQueue.Add(new Brutus(boxE, enemy2));
                        enemy2.Visible = true;
                    }
                    else
                    {
                        theQueue.Add(new Brutus(boxE, enemy3));
                        enemy3.Visible = true;
                    }
                }
                else if (EnemyType >= 6)
                {
                    if (i == 0)
                    {
                        theQueue.Add(new NovaSix(boxE, enemy1));
                        enemy1.Visible = true;
                    }
                    else if (i == 1)
                    {
                        theQueue.Add(new NovaSix(boxE, enemy2));
                        enemy2.Visible = true;
                    }
                    else
                    {
                        theQueue.Add(new NovaSix(boxE, enemy3));
                        enemy3.Visible = true;
                    }
                }
            }
        }
    }
}