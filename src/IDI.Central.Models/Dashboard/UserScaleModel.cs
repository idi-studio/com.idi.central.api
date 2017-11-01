using IDI.Core.Infrastructure.Queries;
using Newtonsoft.Json;

namespace IDI.Central.Models.Dashboard
{
    public class UserScaleModel : IModel
    {
        [JsonProperty("custTotal")]
        public int CustomerTotal { get; set; } = 0;

        [JsonProperty("custMale")]
        public int MaleCustomerTotal { get; set; } = 0;

        [JsonProperty("custFemale")]
        public int FemaleCustomerTotal { get; set; } = 0;

        [JsonProperty("staffTotal")]
        public int StaffTotal { get; set; } = 0;

        [JsonProperty("staffMale")]
        public int MaleStaffTotal { get; set; } = 0;

        [JsonProperty("staffFemale")]
        public int FemaleStaffTotal { get; set; } = 0;
    }
}
