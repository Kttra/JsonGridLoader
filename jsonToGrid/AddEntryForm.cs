using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO; //FILE
using Newtonsoft.Json;

namespace jsonToGrid
{
    public partial class AddEntryForm : Form
    {
        public AddEntryForm()
        {
            InitializeComponent();
            InitializeLabels();
            InitializeTextBoxes();
        }
        //Update the Labels
        private void InitializeLabels()
        {
            try
            {
                //If you want to use exact names
                //var labels = new List<Label> { label0, label1, label2, label3, label4, label5, label6,
                //label7, label8};

                //If you don't care about order
                //var labels = Controls.OfType<Label>().Where(label => label.Name.StartsWith("label"));

                //Update the label names
                for (int i = 0; i < Form1.colNum; i++)
                {
                    this.Controls["label" + i].Text = Form1.colName[i];
                }

            }
            catch
            {

            }

            //Load the json file for info
            string fileName = Properties.Settings.Default.FileName;
            if (File.Exists(fileName))
            {
                var jsonObject = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText(fileName));

                int rotation = jsonObject["Rotation"];
                int step = jsonObject["Step"];

                //EntryLevel.Text = Form1.rowName[step] + " - Rotation " + rotation;
                //Check to see if we are out of bounds, then we need to reset cycle and go to next rotation
                if (step == Form1.rowName.Length)
                {
                    step = 0; //Since we are getting info from list, we need to reset to the index 0
                    rotation++;
                    EntryLevel.Text = Form1.rowName[step] + " - Rotation " + rotation; //Update the label
                }
                else
                {
                    EntryLevel.Text = Form1.rowName[step] + " - Rotation " + rotation; //Update the label
                }
            }
        }
        //Loads the format textboxes (right side) so the user has an idea of what to type
        private void InitializeTextBoxes()
        {
            string fileName = Properties.Settings.Default.FileName;
            if (File.Exists(fileName))
            {
                var jsonObject = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText(fileName));
                int rotation = jsonObject["Rotation"];
                rotation = rand.Next(rotation);
                int step = jsonObject["Step"];
                step = rand.Next(step - 1);

                for (int i = 0; i < 9; i++)
                {
                    try
                    {
                        this.Controls["TextB" + i].Text = jsonObject[Form1.rowName[step]][Form1.colName[i]][rotation];
                    }
                    catch { 
                    }
                    this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
                    this.BackColor = Color.Transparent;
                }
            }
            this.ActiveControl = textBox0; //Focus to the first textbox
        }
        //Confirm the changes
        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            //Initialize the variables
            //var textbox = Controls.OfType<TextBox>().Where(TextBox => TextBox.Name.StartsWith("textBox"));

            string fileName = Properties.Settings.Default.FileName;
            var jsonObject = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText(fileName));
            
            //Get step and rotation
            int step = jsonObject["Step"];
            int rotation = jsonObject["Rotation"];

            //Go to the next rotation
            if (step == Form1.rowNum)
            {
                step = 0;
                rotation++;
            }

            string rowName = Form1.rowName[step]; //The Step name, or the mission name
            //Checks user input for each column value
            for (int i = 0; i < Form1.colNum; i++)
            {
                string colName = Form1.colName[i];
                string jsonOutputStr = this.Controls["textBox" + i].Text;

                //If the number is an integer, set the value to be an integer
                if (isInteger(jsonOutputStr))
                {
                    int textBoxValue = int.Parse(jsonOutputStr);
                    jsonObject[rowName][colName].Add(textBoxValue);
                }
                //Input is a string
                else
                {
                    jsonObject[rowName][colName].Add(jsonOutputStr);
                }
            }
            
            jsonObject["Step"] = ++step; //Since we are writing to the json file, we need to add 1
            jsonObject["Rotation"] = rotation;

            Console.WriteLine(jsonObject);
            var text = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
            File.WriteAllText(Properties.Settings.Default.FileName, text);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        //Only checks whether the input should be a integer or a string
        private bool isInteger(dynamic value)
        {
            return int.TryParse(value, out int _);
        }
        //Auto fills the textboxes with random data from a random entry in the json file
        private void BtnAutoFill_Click(object sender, EventArgs e)
        {
            string fileName = Properties.Settings.Default.FileName;
            if (File.Exists(fileName))
            {
                var jsonObject = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText(fileName));
                int rotation = jsonObject["Rotation"];
                rotation = rand.Next(rotation);
                int step = jsonObject["Step"];
                step = rand.Next(step-1);

                for (int i = 0; i < 9; i++)
                {
                    try
                    {
                        //Fill in textbox with random entry
                        this.Controls["Textbox" + i].Text = jsonObject[Form1.rowName[step]][Form1.colName[i]][rotation];
                    }
                    catch
                    {
                    }
                }
            }
        }
        //Our random method
        private static Random rand = new Random();
    }
}
