namespace EventPlanning.Domain.Models
{
    public class Creator : IBaseModel
    {
        public Guid Id { get; set; }

        public Account Account { get; set; }

        public string OrganizationName { get; set; }

        public string Website { get; set; }
    }
}
