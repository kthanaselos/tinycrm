namespace TinyCrm.Core.Services.Options
{
    public class CustomerUpdateOptions
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool? IsActive { get; set; }
    }
}
