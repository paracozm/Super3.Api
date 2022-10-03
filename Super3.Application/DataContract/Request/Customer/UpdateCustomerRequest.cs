namespace Super3.Application.DataContract.Request.Customer
{
    public sealed class UpdateCustomerRequest
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Document { get; set; }
        public string AddressNumber { get; set; }
        public string CEP { get; set; }
    }
}
