namespace MyClassroom.Contracts
{
    public class APISettings
    {
        public const string MyClassroom = "MyClassroom";
        public const string gRPCConfiguration = "gRPCConfiguration";

        public string SecretKey { get; set; } = string.Empty;
        public string ValidAudience { get; set; } = string.Empty;
        public string ValidIssuer { get; set; } = string.Empty;
    }
}
