# JsonGridLoader
Visual Studio C# program that loads grids using information from a json file. This is a rewritten and enhanced version of my [C++ project](https://github.com/Kttra/VS_GridProject).

The program expects the format of your json file to be a specific way. The way it is formmated
is that it assumes you are trying to load a table for a competition. This competition changes weekly
where each week is a different objective (this is called a step or a mission). One the competition goes through
all objectives, it cycles back to the beginning. This is called one cycle or one rotation.

In your json file, you should have the "rotation" and "step" keys so the program knows how far it should
look for information. Below is an example of the format expected from one key in the json file. They key has values that consists of a list where each value in the list is bound to each rotation in the competition chronologically.

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

**Packages Used**
-----------------------------------
In this project I used the newtonsoft json package; however, I have created a class file that can easily be altered to use microsoft's json package if you wish to do so.
