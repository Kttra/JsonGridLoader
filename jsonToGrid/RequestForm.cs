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
    public partial class RequestForm : Form
    {
        public RequestForm()
        {
            InitializeComponent();
        }
        //Check the input when the confirm button is pressed
        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            getInput();
            
        }
        //When the neter key is pressed check the input
        private void userInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                getInput();
            }
        }
        //Get input from the user for what tables they want to load
        private void getInput()
        {
            userInput.Focus();
            switch (counter)
            {
                case 0:
                    if (isValid())
                    {
                        label1.Text = "Enter the rotation you would want to see in the second table";
                        userInput.Text = "";
                        ++counter;
                    }
                    else
                    {
                        userInput.Text = "Invalid!";
                    }
                    break;
                case 1:
                    //label1.Text = "Enter the rotation you would want to see in the second table:";
                    if (isValid())
                    {
                        counter = 0;
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        userInput.Text = "Invalid!";
                    }
                    break;
                //Program shouldn't reach this section, but we have it here just in case
                default:
                    counter = 0;
                    this.Close();
                    break;
            }
        }
        //Check if the user typed in a number that is below the rotation count
        private bool isValid()
        {
            string fileName = Properties.Settings.Default.FileName;
            var jsonObject = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText(fileName));

            //Get step and rotation
            int rotation = jsonObject["Rotation"];
            bool isValid = int.TryParse(userInput.Text, out int rotationNum);
            //int rotationNum = int.Parse(userInput.Text);
            if (isValid)
            {
                if(counter == 0 && rotationNum <= rotation)
                {
                    Form1.grid1Rotation = rotationNum;
                }
                //The counter is set to 1
                else if(counter == 1 && rotationNum <= rotation)
                {
                    Form1.grid2Rotation = rotationNum;
                }
                else
                {
                    //Userinput is out of bounds
                    return false;
                }
            }

            return isValid;
        }
        //Used to determine how much inputs we got from the user
        private static int counter = 0;
    }
}
