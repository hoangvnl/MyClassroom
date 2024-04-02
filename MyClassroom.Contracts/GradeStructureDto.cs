using System.Text.Json.Serialization;

namespace MyClassroom.Contracts
{
    public class GradeStructureDto
    {
        [JsonInclude]
        public string Description { get; private set; } = string.Empty;
        [JsonInclude]
        public decimal Percent { get; private set; }

        public GradeStructureDto()
        {
            
        }
    }
}
