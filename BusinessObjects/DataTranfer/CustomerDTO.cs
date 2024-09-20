namespace BusinessObjects.DataTranfer
{
    public class CustomerDTO
    {
        public int Id { get; set;}
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string Gender { get; set; }
        public DateOnly Birthday { get; set; }
        public string Address { get; set; }
    }
}