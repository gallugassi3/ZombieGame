using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZombieApp
{
    public partial class FormLabel : Form
    {
        private int Score { get; set; }

        public FormLabel(int score)
        {
            InitializeComponent();
            SaveButton.Click += SaveButton_Click;
            Score = score;
        }

        private void FormLabel_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Name = textBox1.Text;

            // Add the score to the DataGridView
            dataGridView1.Rows.Add(Name, Score.ToString());
        }

        private void iDelete()
        {
            foreach (DataGridViewRow item in this.dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.RemoveAt(item.Index);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
        }
        private void MeExit()
        {
            DialogResult iExit;
            iExit = MessageBox.Show("Confirm if you want to exit", "Save DataGirdView", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (iExit == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MeExit();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            iDelete();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Create a StringBuilder to store the leaderboard data
                StringBuilder leaderboardData = new StringBuilder();

                // Iterate through each row in the DataGridView
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    // Get the name and score values from the row
                    string name = row.Cells[0].Value.ToString();
                    string score = row.Cells[1].Value.ToString();

                    // Append the name and score to the StringBuilder
                    leaderboardData.AppendLine($"{name},{score}");
                }
                // Save the leaderboard data to a file
                string filePath = @"C:\\Users\\Gallu\\LeadboardFile"; // Replace with your desired file path
                File.WriteAllText(filePath, leaderboardData.ToString());

                // Display a success message to the user
                MessageBox.Show("Leaderboard saved successfully.", "Save Leaderboard", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Display an error message if the save operation fails
                MessageBox.Show($"Error saving leaderboard: {ex.Message}", "Save Leaderboard", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
