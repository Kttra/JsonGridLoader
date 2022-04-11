# JsonGridLoader
Visual Studio C# program that loads grids using information from a json file. This is a rewritten and enhanced version of my [C++ project](https://github.com/Kttra/VS_GridProject).

The program expects the format of your json file to be a specific way. The way it is formmated is that it assumes you are trying to load a table for a competition. This competition changes weekly/daily where each week/day is a different objective (this is called a step or a mission). One the competition goes through all objectives, it cycles back to the beginning. This is called one cycle or one rotation.

In your json file, you should have the "rotation" and "step" keys so the program knows how far it should look for information. Below is an example of the format expected from one key in the json file. The first rotation starts at index 0. While step is how far in we are in the rotation. They key has values that consists of a list where each value in the list is bound to each rotation in the competition chronologically.

```
"Step 1": {
  "1st solo": [ "4:10", "2:20", "2:12", "2:47", "1:42", "1:41", "1:38", "2:04", "2:07"],
  "20th solo": [ "6:16", "4:40", "4:18", "4:16", "3:52", "3:49", "3:36", "4:35", "4:17" ],
  "My solo": [ "4:48", "4:13", "3:59", "3:17", "2:43", "2:37", "2:24", "2:28", "2:24" ],
  "Solo rank": [ 4, 14, 15, 6, 5, 5, 4, 3, 2 ],
  "Last rank": [ 20, 20, 20, 20, 20, 20, 20, 20, 20 ],
  "Date": [ "5/25/2020", "6/29/2020", "9/28/2020", "11/30/2020", "2/1/2021", "4/5/2021", "6/7/2021", "8/9/2021", "10/11/2021" ],
  "Class used": [ "-", "-", "-", "-", "-", "-", "-", "-", "-" ],
  "Group time": [ "0", "0", "2:33", "1:44", "1:41", "1:38", "1:30", "1:30", "1:28" ],
  "Group place": [ 0, 0, 26, 13, 12, 9, 9, 5, 5 ],
  "1st group time": [ "0", "0", "1:15", "1:17", "1:08", "1:10", "1:02", "1:08", "1:08" ],
  "Group 20 time": [ "0", "0", "2:24", "3:08", "2:14", "1:47", "1:59", "2:52", "2:19" ],
  "20th group place": [ 0, 0, 20, 18, 19, 17, 19, 20, 15 ],
  "Team": [ 0, 0, 0, 13, 14, 8, 5, 5, 1 ]
```
**Program Layout**
-----------------------------------
When the load button is clicked, the grids are loaded in. The top grid shows the most recent rotation and the bottom grid shows the previous rotation in the json file. The grid on the top right calculates the best times ever achieved in any rotation. Below this grid is the file directory. You can change the directory or file by either typing in the textbox or opening the file dialog menu by pressing the button to the right of the textbox.

![image](https://user-images.githubusercontent.com/100814612/162642344-51799780-580b-485f-bdf8-bc98970f8ff8.png)

**Request Button**
-----------------------------------
The request button will open another form asking for what two rotations you would like to open onto the grid because load will only load the recent and previous rotations for comparison.

<p align="center">
<img src="https://user-images.githubusercontent.com/100814612/162650577-fdad1f07-3f2d-4457-859d-578cf5fa5c57.png"><img>
</p>

**Add Entry Button**
-----------------------------------
The "Add Entry" button will add another entry to the json file. It opens another form that will ask for user input. On the right of the menu, it shows the format expected (taken from rotation 2 of the json file for each value in a key). Input validation wise, the program does not check for specific formatting because such a method cannot be made to be adaptive for different json files. Instead, I have only set the program to check whether or not you have entered a integer or a string to be outputted to the json file.

<p align="center">
<img src="https://user-images.githubusercontent.com/100814612/162650562-8891b41c-dbbd-4517-aea0-3098aa808b90.png"><img>
</p>

**File Location & Default File Directory**
-----------------------------------
The default file directory can be changed at any time. It is located in the project settings. To change the directory, you can either type in the directory in the textbox and press enter or press on the button to bring up the file explorer menu. 

<p align="center">
<img src="https://user-images.githubusercontent.com/100814612/162650634-45f4e3a5-92e5-4032-ae56-d2aac5adbba9.png"><img>
</p>

**Json Files in C#**
-----------------------------------
There are multiple ways to access and write information from and to a json file. The below code goes over two different ways.
```
string fileName = @"d:\Users\Username\Desktop\sampleAB2.json";
if (File.Exists(fileName))
{
  var jsonObject = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText(fileName));
  int rotation = jsonObject["Rotation"];
  
  //Writing a file
  var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
  var text = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
  File.WriteAllText(@"d:\Users\Username\Desktop\sampleAB2.json", text);

  //If you want to assign the json to class instead, then you can call the value like this: jsonObject.Step1.classUsed[0]
  /*
  var jsonObjectEdit = JsonConvert.DeserializeObject<JsonValues>(File.ReadAllText(fileName));
  					    
  //System.Text.Json Serialize Method
  var options = new JsonSerializerOptions {WriteIndented = true};
  string jsonString = System.Text.Json.JsonSerializer.Serialize(jsonObjectEdit, options);
  Console.WriteLine(jsonString);
  File.WriteAllText(@"d:\Users\Username\Desktop\sampleAB2.json", jsonString);
  */
}
```

**Grid and Cell Values**
-----------------------------------
You can assign the cell values manually and also add in rows throughout the program.

```
//Add a row
dataGridView1.Rows.Add("Row Name");

//Assigning row 8, cell 0 a value
dataGridView1.Rows[8].Cells[0].Value = "Row Name";
dataGridView1[8, 0].Value = "Row Name";

//Clear the grid
dataGridView1.Rows.Clear();
```

**Form Controls, Labels, Textboxes**
------------------------------------
We can assign and change the form controls on the go without needing to create a list.

```
//Best method in our case
for (int i = 0; i < Form1.colNum; i++)
{
    this.Controls["label" + i].Text = Form1.colName[i];
}

//If you don't care about order
//var labels = Controls.OfType<Label>().Where(label => label.Name.StartsWith("label"));

//If you want to use exact names
//var labels = new List<Label> { label0, label1, label2, label3, label4, label5, label6, label7, label8};
```

**Step & Rotation**
-----------------------------------
Step starts at 1 to the number of column items or the number or max number of steps while rotation starts at 0 to the max number of cycles done. You can think of step as different courses/missions/objectives done while rotations is the almost of cycles done or the most recently in progress cycle.

**Packages Used**
-----------------------------------
In this project I used the newtonsoft json package; however, I have created a class file that can easily be altered to use microsoft's json package if you wish to do so. It may make it easier to conceptualize what is happening by using the class file, but for automation and editing wise it's better to work with the json file dynamically.

**Current Progress**
-----------------------------------
Currently, the program is at a state where I am satisfied. The only feature left that I have not finished is the delete entry feature.
