namespace MyClassroom.Application.Common
{
    public class APIProblem
    {
        public string Detail { get; set; }
        public Dictionary<string, string[]> Errors { get; set; }

        public APIProblem(
            string detail,
            Dictionary<string, string[]> errors)
        {
            Detail = detail;
            Errors = errors;
        }
    }
}
