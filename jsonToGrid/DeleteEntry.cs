using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO; //FILE

namespace jsonToGrid
{
    public partial class DeleteEntry : Form
    {
        public DeleteEntry()
        {
            InitializeComponent();
            InitializeLabel(); 
        }
        private void InitializeLabel()
        {
            string fileName = Properties.Settings.Default.FileName;
            var jsonObject = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText(fileName));

            int rotation = jsonObject["Rotation"];
            label1.Text = "What rotation would you like to select? (Most recent is " + rotation + ")";
        }
        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            getDelInfo();
        }
        private void getDelInfo()
        {
            string fileName = Properties.Settings.Default.FileName;
            var jsonObject = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText(fileName));

            int rotation = jsonObject["Rotation"];
            int step = jsonObject["Step"];

            userInput.Focus();
            switch (counter)
            {
                case 0:
                    //Get the rotation/step index to delete at
                    if (int.TryParse(userInput.Text, out Form1.delRotation))
                    {
                        if (Form1.delRotation <= rotation && Form1.delRotation >= 0)
                        {
                            ++counter;
                            userInput.Text = "";
                            label1.Text = "What row would you like to select? (Most recent is " + step + ")";
                        }
                        else { userInput.Text = "Invalid!"; }
                    }
                    else { userInput.Text = "Invalid!"; }
                    break;
                case 1:
                    //Get the mission/key/row name to delete at
                    if (int.TryParse(userInput.Text, out Form1.delRow))
                    {
                        if (Form1.delRow <= Form1.rowNum && Form1.delRow > 0)
                        {
                            counter = 0;
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else { userInput.Text = "Invalid!"; }
                    }
                    else { userInput.Text = "Invalid!"; }
                    break;
                //We should never need to go here
                default:
                    counter = 0;
                    break;
            }
        }
        //If the user presses enter in the textbox
        private void userInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                getDelInfo();
            }
        }
        //Used to determine how much input we got from the user
        private static int counter = 0;
    }
}
