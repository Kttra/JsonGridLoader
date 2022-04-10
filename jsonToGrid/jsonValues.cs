using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

/*	Converts the jsonObject to a class
	
	Replace the properties names with your keys and replace
	the list types with your types.

	We don't use this class in this example b/c we want more flexibility
	from the dynamic deserialization. Instead we use lists located at the
	bottom of form1.
//*/
namespace jsonToGrid
{
    [Serializable]
    class JsonValues
    {
		[JsonProperty(PropertyName = "Step 1")]
		public Step Step1 { get; set; }
		[JsonProperty(PropertyName = "Step 2")]
		public Step Step2 { get; set; }
		[JsonProperty(PropertyName = "Step 3")]
		public Step Step3 { get; set; }
		[JsonProperty(PropertyName = "Step 4")]
		public Step Step4 { get; set; }
		[JsonProperty(PropertyName = "Step 5")]
		public Step Step5 { get; set; }
		[JsonProperty(PropertyName = "Step 6")]
		public Step Step6 { get; set; }
		[JsonProperty(PropertyName = "Step 7")]
		public Step Step7 { get; set; }
		[JsonProperty(PropertyName = "Step 8")]
		public Step Step8 { get; set; }
		[JsonProperty(PropertyName = "Step 9")]
		public Step Step9 { get; set; }
		public class Step
		{
			[JsonProperty(PropertyName = "1st solo")]
			public List<string> solo1 { get; set; }
			[JsonProperty(PropertyName = "20th solo")]
			public List<string> solo20 { get; set; }
			[JsonProperty(PropertyName = "My solo")]
			public List<string> mySolo { get; set; }
			[JsonProperty(PropertyName = "Solo rank")]
			public List<int> soloRank { get; set; }
			[JsonProperty(PropertyName = "Last rank")]
			public List<int> lastRank { get; set; }
			[JsonProperty(PropertyName = "Date")]
			public List<string> date { get; set; }
			[JsonProperty(PropertyName = "Class used")]

			public List<string> classUsed { get; set; }
			[JsonProperty(PropertyName = "Group time")]
			public List<string> groupTime { get; set; }
			[JsonProperty(PropertyName = "Group place")]
			public List<int> groupPlace { get; set; }
			[JsonProperty(PropertyName = "1st group time")]
			public List<string> group1Time { get; set; }
			[JsonProperty(PropertyName = "Group 20 time")]
			public List<string> group20Time { get; set; }
			[JsonProperty(PropertyName = "20th group place")]
			public List<int> group20Place { get; set; }
			[JsonProperty(PropertyName = "Team")]
			public List<int> team { get; set; }
		}
		//Function used for testing
		public dynamic getVal()
        {
			return this.Step1.date[0];
        }
	}
}
