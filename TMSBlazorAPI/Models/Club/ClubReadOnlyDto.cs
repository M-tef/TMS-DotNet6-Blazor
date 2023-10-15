using System.ComponentModel.DataAnnotations;

namespace TMSBlazorAPI.Models.Club
{
    public class ClubReadOnlyDto: BaseDto
    {
        public string ClubName { get; set; }

        public int UserID{ get; set; }

    }
}
