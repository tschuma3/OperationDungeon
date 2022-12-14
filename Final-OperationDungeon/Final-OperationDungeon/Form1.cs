using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace Final_OperationDungeon
{
    public partial class Form1 : Form
    {
        Gameplay theGameplay;
        int[] gameRecordsData = new int[2]; // index: 0 = Highest Score, 1 = Highest Level

        /// <summary>
        /// Main method
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            theGameplay = new Gameplay(teamInfo, enemyInfo, mainBackground);
            theGameplay.form = this;
            SetButtons();
            RefreshInfo();     
        }

        /// <summary>
        /// Sets the buttons on the form
        /// </summary>
        public void SetButtons()
        {
            attackButton.Text = theGameplay.theQueue[theGameplay.currentTurn].AttackName;
            specialButton.Text = theGameplay.theQueue[theGameplay.currentTurn].SpecialName;
            Enemy2.Enabled = false;
            attackButton.Click += new EventHandler(OnButton1Click);
            specialButton.Click += new EventHandler(OnButton2Click);
            
            for (int i = 0; i < theGameplay.theQueue.Count; i++)
            {
                theGameplay.theQueue[i].Attacked += new System.EventHandler(OnTargetClick);
            }
            NewTurn();
        }

        /// <summary>
        /// Button click for the attack
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnButton1Click(object sender, EventArgs e)
        {
            theGameplay.queuedAction = "attack";
            textBox1.AppendText($"\r\nYou chose to attack, now choose a target!");
            attackButton.Enabled = false;
            specialButton.Enabled = false;
            defendButton.Enabled = false;
            Enemy1.Enabled = true;
            Enemy2.Enabled = true;
            Enemy3.Enabled = true;
        }

        /// <summary>
        /// Button click for the special attack
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnButton2Click(object sender, EventArgs e)
        {
            theGameplay.queuedAction = "special";
            textBox1.AppendText($"\r\nYou chose to use your special, now choose a target!");
            attackButton.Enabled = false;
            specialButton.Enabled = false;
            defendButton.Enabled = false;
            Enemy1.Enabled = true;
            Enemy2.Enabled = true;
            Enemy3.Enabled = true;
        }

        /// <summary>
        /// Button click for the defense
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnButton3Click(object sender, EventArgs e)
        {
            theGameplay.queuedAction = "defend";
            textBox1.AppendText($"\r\nYou chose to defend! ");
            attackButton.Enabled = false;
            specialButton.Enabled = false;
            defendButton.Enabled = false;
            Player1.Enabled = true;
            Player2.Enabled = true;
            Player3.Enabled = true;
        }

        /// <summary>
        /// On the target clicks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnTargetClick(object sender, EventArgs e)
        {
            textBox1.AppendText("\r\n" + theGameplay.TakeTurn((Character)sender));
            attackButton.Enabled = true;
            specialButton.Enabled = true;
            defendButton.Enabled = true;
            Enemy1.Enabled = false;
            Enemy2.Enabled = false;
            Enemy3.Enabled = false;
            
            // Adds to current turn
            theGameplay.currentTurn++;

            // If the current turn is equal to the amount in the queue, set to 0
            if (theGameplay.currentTurn >= theGameplay.theQueue.Count)
            {
                theGameplay.currentTurn = 0;
            }

            // Display current turn to console
            Console.WriteLine(theGameplay.currentTurn);

            // Variable
            bool isDown = false;

            // For everyone in theQueue
            for (int i = 0; i < theGameplay.theQueue.Count; i++)
            {
                // If bad set isDown to true
                if (theGameplay.theQueue[i].GoodBad == "Bad")
                {
                    isDown = true;
                }
            }

            // If not isDown, then the next round occurs
            if (!isDown)
            {
                NewRound();
            }

            // Refreshes info
            RefreshInfo();

            // Calls new turn method
            NewTurn();
        }

        /// <summary>
        /// Refreshes info
        /// </summary>
        public void RefreshInfo()
        {
            for (int i = 0; i < theGameplay.theQueue.Count; i++)
            {
                theGameplay.theQueue[i].Labels.Text = theGameplay.theQueue[i].Name + ": " + theGameplay.theQueue[i].HitPoints.ToString();
            }
        }

        /// <summary>
        /// Sets up the new turn
        /// </summary>
        public void NewTurn()
        {
            // If the current player is alive
            if (theGameplay.theQueue[theGameplay.currentTurn].Alive == true)
            {
                // Display the correct text
                groupBox1.Text = theGameplay.theQueue[theGameplay.currentTurn].Name;
                attackButton.Text = theGameplay.theQueue[theGameplay.currentTurn].AttackName;
                specialButton.Text = theGameplay.theQueue[theGameplay.currentTurn].SpecialName;

                // If the current turn is bad
                if (theGameplay.theQueue[theGameplay.currentTurn].GoodBad == "Bad")
                {
                    // The AI takes their turn
                    AI();

                    // Adds to current turn
                    theGameplay.currentTurn++;

                    // If the current turn is equal to the amount in the queue, set to 0
                    if (theGameplay.currentTurn >= theGameplay.theQueue.Count)
                    {
                        theGameplay.currentTurn = 0;
                    }

                    // Recursion
                    NewTurn();
                }

                // Checks who is alive
                theGameplay.CheckAlive();           
            }

            // Variable
            bool isDown = false;

            // For everyone in theQueue
            for (int i = 0; i < theGameplay.theQueue.Count; i++)
            {
                // If bad set isDown to true
                if (theGameplay.theQueue[i].GoodBad == "Bad")
                {
                    isDown = true;
                }
            }

            // If not isDown, then the next round occurs
            if (!isDown)
            {
                NewRound();
            }

            // Refreshes info
            RefreshInfo();
        }

        /// <summary>
        /// Creates a new round
        /// </summary>
        public void NewRound()
        {
            enemyInfo = new GroupBox();

            // Adds enemies to theQueue
            theGameplay.AddEnemies(enemyInfo, mainBackground);

            // Increase the level and displays to the console
            theGameplay.level++;
            gameRecordsData[1] = theGameplay.level;
            WriteRecords();
            Console.WriteLine("Level: " + theGameplay.level);

            // Writes the score to records
            gameRecordsData[0] = theGameplay.score;
            WriteRecords();

            // Refreshes info
            RefreshInfo();
        }

        /// <summary>
        /// AI take there turn
        /// </summary>
        public void AI()
        {
            // Variable
            Random rand = new Random();
            int randomAction = rand.Next(0, 3);
            List<Character> goodGuys = new List<Character>();
            int numberOfGood = 0;
                       
            // Based off the number, do an action
            if (randomAction == 1)
            {
                theGameplay.queuedAction = "attack";
            }
            else if (randomAction == 2)
            {
                theGameplay.queuedAction = "special";
            }
            else
            {
                theGameplay.queuedAction = "defend";
            }

            // For every character in theQueue
            for (int i = 0; i < theGameplay.theQueue.Count; i++)
            {
                // If the character is good, adds to goodGuys and increase numberOfGood
                if (theGameplay.theQueue[i].GoodBad == "Good")
                {
                    goodGuys.Add(theGameplay.theQueue[i]);
                    numberOfGood++;
                }
            }
            
            // Chooses who to attack
            int whoTo = rand.Next(0, numberOfGood);

            // Writes the action
            textBox1.AppendText("\r\n" + theGameplay.TakeTurn(theGameplay.theQueue[theGameplay.theQueue.IndexOf(goodGuys[whoTo])]));
            
        }

        /// <summary>
        /// Reads the records text file
        /// </summary>
        private void ReadRecords()
        {
            try
            {
                // Variables
                StreamReader reader = new StreamReader("Records.txt");
                string line = reader.ReadLine();
                int count = 0;

                Console.WriteLine("Reading from file");

                // While line is not null
                while (line != null)
                {
                    // Writes, parses, adds to count, and sets line
                    Console.WriteLine(line);
                    gameRecordsData[count] = int.Parse(line);
                    count++;
                    line = reader.ReadLine();
                }
                // Closes
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Writes to the records text file
        /// </summary>
        private void WriteRecords()
        {
            try
            {
                // Variables
                StreamWriter writer = new StreamWriter("Records.txt");

                Console.WriteLine("Writing to file");

                // For each line in the game records
                foreach (int i in gameRecordsData)
                {
                    // Writes into the file
                    writer.WriteLine(i);
                }
                // Closes
                writer.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        #region Other Events
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Enemy2_Click(object sender, EventArgs e)
        {

        }

        #endregion

        /// <summary>
        /// Closes the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Restarts the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        /// <summary>
        /// Shows the high score and highest level
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void recordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReadRecords();
            MessageBox.Show("Highest Score: " + gameRecordsData[0] + "\nHighest Level: " + gameRecordsData[1]);
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1. Left click on a desired attack or special  " +
                "\n2. Left click on the desired target" +
                "\n3. Last as long as you can" +
                "\n4. Quick Revivial Heals");
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1. Tyler Schumacher wrote the code  " +
                "\n2. Written on May 13, 2020  " +
                "\n3. Written for CS3020");
        }
    }
}
