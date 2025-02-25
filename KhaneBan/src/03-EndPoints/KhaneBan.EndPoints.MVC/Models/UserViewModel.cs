namespace KhaneBan.EndPoints.MVC.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PicturePath { get; set; }
        public string PhoneNumber { get; set; }
        public string Adress { get; set; }
        public bool IsDeleted { get; set; }
    }
}
