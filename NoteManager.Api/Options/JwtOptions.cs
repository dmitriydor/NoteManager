namespace NoteManager.Api.Options
{
    public class JwtOptions
    {
        public string Secret { get; set; }
        public string Issure { get; set; }
        public string Audience { get; set; }
    }
}