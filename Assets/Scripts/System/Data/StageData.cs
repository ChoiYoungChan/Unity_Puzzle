using System.Collections.Generic;
using Newtonsoft.Json;

public partial class StageData
{
    [JsonProperty("stageId")]
    public int StageId { get; set; }

    [JsonProperty("stageName")]
    public string StageName { get; set; }

    [JsonProperty("illust")]
    public string Illust { get; set; }

    [JsonProperty("question")]
    public string Question { get; set; }

    [JsonProperty("answer")]
    public string Answer { get; set; }

    [JsonProperty("section")]
    public List<Section> Section { get; set; }

    [JsonProperty("firstOpen")]
    public int FirstOpen { get; set; }
}

public partial class Section
{
    [JsonProperty("sectionId")]
    public int SectionID { get; set; }

    [JsonProperty("illust")]
    public string Illust { get; set; }

    [JsonProperty("question")]
    public string Question { get; set; }

    [JsonProperty("questionImage")]
    public string QuestionImage { get; set; }

    [JsonProperty("answerImage")]
    public string AnswerImage { get; set; }

    [JsonProperty("time")]
    public int Time { get; set; }
    
    [JsonProperty("sliceNum")]
    public int SliceNum { get; set; }
}