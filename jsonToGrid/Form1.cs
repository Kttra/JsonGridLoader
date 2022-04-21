using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO; //FILE
using Newtonsoft.Json;
//using System.Text.Json;
/*	The purpose of the program is to fill tables based off of json files.
 	
	The program expects the format of your json file to be a specific way. The way it is formmated
	is that it assumes you are trying to load a table for a competition. This competition changes weekly
	where each week is a different objective (this is called a step). One the competition goes through
	all objectives, it cycles back to the beginning. This is called one cycle or one rotation.

	In your json file, you should have the "rotation" and "step" keys so the program knows how far it should
	look for information. 

	Edit your key and value names at the lists provided near the bottom of this file.

	Rotation = The amount of cycles done
	Step = How far are you in the cycle or rotation
*/

namespace jsonToGrid
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			loadGrids();
			directoryTextbox.Text = Properties.Settings.Default.FileName;
		}

		//Initializes the Row Names in the Grids
		private void loadGrids()
        {
			int length = rowName.Length;

			for (int i = 0; i < length; i++)
            {
				dataGridView1.Rows.Add(rowName[i]);
				dataGridView2.Rows.Add(rowName[i]);
				dataGridView3.Rows.Add(rowName[i]);
			}
			//If you want to manually assign the rules yourself, here is an example
			//dataGridView1.Rows[8].Cells[0].Value = "Row Name";
		}
		//Clear the grids and return once finished
		private Task clearGrid()
		{
			dataGridView1.Rows.Clear();
			dataGridView2.Rows.Clear();
			dataGridView3.Rows.Clear();
			loadGrids();
			return Task.CompletedTask;
		}
		//Call for grid clear and then Loads the grid when that finishes
		private async void loadTable()
		{
			Task t = clearGrid();
			await t;

			//string fileName = @"d:\Users\kevin\Desktop\bing\JsonEditor\sampleAB2.json";
			string fileName = Properties.Settings.Default.FileName;
			if (File.Exists(fileName))
			{
				//If you want to assign the json to class instead, then you can call the value like this: jsonObject.Mission1.classUsed[0]
				//var jsonObjectEdit = JsonConvert.DeserializeObject<JsonValues>(File.ReadAllText(fileName));

				//We are going with dynamic instead as it's more flexible
				var jsonObject = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText(fileName));

				int rotation = jsonObject["Rotation"];
				int step = jsonObject["Step"];
				int grid2Step = colName.Length;

				//Load 1st table
				loadGridValues(jsonObject, 1, rotation, step);
				label1.Text = "Rotation " + rotation;

				//Load 2nd table if the previous rotation exists
				if (jsonObject["Rotation"] != "0")
				{
					loadGridValues(jsonObject, 2, rotation - 1, grid2Step);
					label2.Text = "Rotation " + (rotation - 1);
				}
                else
                {
					label2.Text = "Rotation DNE";
				}

				//Load 3rd table
				loadBestGrid(jsonObject);

				//var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
				//var text = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
				//File.WriteAllText(@"d:\Users\kevin\Desktop\bing\JsonEditor\sampleAB2.json", text);

				/*					System.Text.Json Serialize Method
				var options = new JsonSerializerOptions {WriteIndented = true};
				string jsonString = System.Text.Json.JsonSerializer.Serialize(jsonObjectEdit, options);
				Console.WriteLine(jsonString);
				File.WriteAllText(@"d:\Users\kevin\Desktop\bing\JsonEditor\sampleAB2.json", jsonString);
				*/
				//dataGridView1.Rows.Clear();

			}
			else
			{
				MessageBox.Show(
					"File not not found",
					"Error",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning // for Warning  
										   //MessageBoxIcon.Error // for Error 
										   //MessageBoxIcon.Information  // for Information
										   //MessageBoxIcon.Question // for Question
				);
			}
		}
		//Load the specified tables the user wants, used by the request form
		private async void loadUserTable()
        {
			Task t = clearGrid();
			await t;

			//We are going with dynamic instead as it's more flexible
			string fileName = Properties.Settings.Default.FileName;
			var jsonObject = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText(fileName));

			int rotation = jsonObject["Rotation"];
			int step1 = rowNum;
			int step2 = rowNum;
			int grid2Step = colName.Length;
			if(grid1Rotation == rotation)
            {
				step1 = jsonObject["Step"];
			}
			if(grid2Rotation == rotation)
            {
				step2 = jsonObject["Step"];
			}

			//Load 1st table
			loadGridValues(jsonObject, 1, grid1Rotation, step1);
			label1.Text = "Rotation " + grid1Rotation;

			//Load 2nd table
			loadGridValues(jsonObject, 2, grid2Rotation, step2);
			label2.Text = "Rotation " + grid2Rotation;

			//Load 3rd table
			loadBestGrid(jsonObject);
		}
		//Clear the grid and load the most recent and the 2nd most recent information
		private void loadButton_Click(object sender, EventArgs e)
		{
			loadTable();
		}
		//Loads the grid values, needs the jsonObject, grid 1 or 2, rotation number, a limiter(steps)
		private void loadGridValues(dynamic jsonObject, int gridNum, int rotation, int stepLimiter)
		{
			if (gridNum == 1) {
				for (int i = 0; i < stepLimiter; i++)
				{
					//Need to start at 1 b/c the top row is the name of the columns
					for (int j = 1; j < colNum+1; j++)
					{
                        try
                        {
							dataGridView1[j, i].Value = jsonObject[rowName[i]][colName[j - 1]][rotation];
                        }
                        catch
                        {

                        }
					}
				}
			}
			else if (gridNum == 2)
            {
				for (int i = 0; i < stepLimiter; i++)
				{
					for (int j = 1; j < colNum+1; j++)
					{
                        try
                        {
							dataGridView2[j, i].Value = jsonObject[rowName[i]][colName[j - 1]][rotation];
                        }
                        catch
                        {

                        }
					}
				}
			}
		}
		//Loads the grid to show the best times achieved
		private void loadBestGrid(dynamic jsonObject)
        {
			int colNum = colName.Length;

            for (int i = 0; i < colNum; i++)
            {
				dataGridView3[1, i].Value = getBestVals(jsonObject[rowName[i]]["My solo"]);
				dataGridView3[2, i].Value = getBestVals(jsonObject[rowName[i]]["Group time"]);
			}
        }
		//Calculates the fastest times
		private string getBestVals(dynamic list)
        {
			double bestTime = double.MaxValue;
			string fastestTime = "";

			foreach (string str in list)
			{
				double nextTime = 0;
				int counter = 0;

				foreach (char num in str)
                {
					//Convert time to number, so "12:34", will become 1.234, so not the exact conversion
					//But exact conversion is unneeded
					if (num != ':')
					{
						nextTime += Math.Pow(10, counter) * (num - '0');
						counter--;
					}
                }
				//Compare the times each iteration
                if (bestTime > nextTime && nextTime != 0)
                {
					bestTime = nextTime;
					fastestTime = str;
				}
			}
			return fastestTime;
        }
		//Add an entry
		private void addButton_Click(object sender, EventArgs e)
        {
			Form f2 = new AddEntryForm();
			var result = f2.ShowDialog();

            if (result == DialogResult.OK)
            {
				MessageBox.Show("File saved to: " + Properties.Settings.Default.FileName, "Success!");
				loadTable();
			}
		}
		//Load specific rotations
        private void requestButton_Click(object sender, EventArgs e)
        {
			Form f3 = new RequestForm();
			var result = f3.ShowDialog();
			if (result == DialogResult.OK)
			{
				MessageBox.Show("Rotation " + grid1Rotation + " and rotation "+ grid2Rotation + " loaded succesfully.", "Success!");
				loadUserTable();
			}
		}

		//When the directory button is pressed, open the file dialog
        private void button1_Click(object sender, EventArgs e)
        {
			changeDefaultDirectory();
		}
		//When enter is pressed in the directory textbox, check if the directory is valid
		private void directoryTextbox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				//Check if file path exists and if it's a json file
                if (directoryTextbox.Text.EndsWith(".json") && File.Exists(directoryTextbox.Text))
                {
					Properties.Settings.Default.FileName = directoryTextbox.Text;
					Properties.Settings.Default.Save();
					MessageBox.Show("File path saved", "Success");
				}
                else
                {
					MessageBox.Show("Invalid file path", "Error");
				}
			}
		}
		//Changes the default directory, called from the change directory button press event
		private void changeDefaultDirectory()
        {
			//Setting up the file explorer selector
			OpenFileDialog openFileDialog1 = new OpenFileDialog();
			openFileDialog1.CheckFileExists = true;
			openFileDialog1.CheckPathExists = true;
			openFileDialog1.RestoreDirectory = true;
			openFileDialog1.InitialDirectory = @"C:\";
			openFileDialog1.Title = "Choose a json file";
			openFileDialog1.Filter = "json files (*.json)|*.json|All files (*.*)|*.*";

			//File path valid
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				//Get the path of specified file
				directoryTextbox.Text = openFileDialog1.FileName;
				Properties.Settings.Default.FileName = openFileDialog1.FileName;
				Properties.Settings.Default.Save();
				MessageBox.Show("File path saved", "Success");
			}
            else
            {
				MessageBox.Show("Invalid file path", "Error");
			}
		}
		//Clears the blue highlight in the 3rd grid
		private void dataGridView3_SelectionChanged(object sender, EventArgs e)
		{
			this.dataGridView3.ClearSelection();
		}
		//When a cell in grid 1 is pressed on, highlight it in data grid 2
		private void dataGridView1_SelectionChanged(object sender, EventArgs e)
		{
			this.dataGridView2.ClearSelection();
			int rPos = dataGridView1.CurrentCell.ColumnIndex;
			int i = dataGridView1.CurrentRow.Index;
			dataGridView2[rPos, i].Selected = true;
		}
		//When a cell in grid 2 is pressed on
		private void dataGridView2_SelectionChanged(object sender, EventArgs e)
		{
			//this.dataGridView2.ClearSelection();
		}
		//Delete an entry from the json file, rowName index starts at 1, rotation index starts at 0 in our case
		private void DeleteButton_Click(object sender, EventArgs e)
		{
			Form f4 = new DeleteEntry();
			var result = f4.ShowDialog();
			if (result == DialogResult.OK)
			{
				string fileName = Properties.Settings.Default.FileName;
				var jsonObject = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText(fileName));

				int rotation = jsonObject["Rotation"];
				int step = jsonObject["Step"];

                foreach (string name in colName)
                {
                    try
                    {
						//Console.WriteLine(jsonObject[rowName[delRow - 1]][name][delRotation]);
						jsonObject[rowName[delRow - 1]][name].RemoveAt(delRotation);
                    }
					//out of bounds error, occurs when trying to delete an entry that does not exist
                    catch (Exception err)
                    {
						MessageBox.Show("" + err, "Error");

					}
                }

				var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
				var text = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);

				MessageBox.Show("Rotation: " + delRotation + ", Row Index: " + delRow + " has been deleted successfully", "Success!");

				File.WriteAllText(fileName, text);
				//Console.WriteLine(text);
			}
			//User selected to delete the last entry
			else if(delRotation == -1)
			{
				string fileName = Properties.Settings.Default.FileName;
				var jsonObject = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText(fileName));

				int rotation = jsonObject["Rotation"];
				int step = jsonObject["Step"];

				foreach (string name in Form1.colName)
				{
					try
					{
						//Console.WriteLine(jsonObject[rowName[delRow - 1]][name][delRotation]);
						jsonObject[Form1.rowName[step - 1]][name].RemoveAt(rotation);
					}
					//out of bounds error, occurs when trying to delete an entry that does not exist
					catch (Exception err)
					{
						MessageBox.Show("" + err, "Error");

					}
				}
				//If it's the first step we need to go back to the previous rotation and step
				if (step == 1 && rotation != 0)
				{
					jsonObject["Step"] = Form1.rowName.Length;
					jsonObject["Rotation"] = --rotation;
				}
				else
				{
					jsonObject["Step"] = --step;
				}
				var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
				var text = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
				File.WriteAllText(fileName, text);
				MessageBox.Show("Last entry has been deleted successfully", "Success!");
				
				//Reload the most recent rotations
				loadTable();
			}
		}

		//Array of the Key Values in your json file
		public static readonly string[] rowName = {"Step 1", "Step 2", "Step 3", "Step 4", "Step 5",
									"Step 6", "Step 7", "Step 8", "Step 9"};
		//Array of the values under the key in your json file
		public static readonly string[] colName = {"1st solo", "20th solo", "My solo", "Date",
									"Class used", "Group time", "1st group time", "Group 20 time", "Team"};
		public static readonly int colNum = colName.Length;
		public static readonly int rowNum = rowName.Length;
		//Information we will get from the RequestForm, used to load specific rotations
		public static int grid1Rotation;
		public static int grid2Rotation;
		//Information we will get from DeleteEntry form, used to delete specific entries
		public static int delRotation;
		public static int delRow;
	}
}
