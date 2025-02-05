namespace Core.Application.DTOs.DTOsRequests.Identity
{
    public class UpdatePasswordRequest
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
