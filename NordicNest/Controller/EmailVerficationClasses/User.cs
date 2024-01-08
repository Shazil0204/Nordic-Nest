namespace NordicNest.Controller.EmailVerficationClasses
{
    public class User
    {
        public string Email { get; set; }
        public string VerificationToken { get; set; }
        public DateTime TokenCreated { get; set; } // Add this line
        public bool IsVerified { get; set; }
    }
}
