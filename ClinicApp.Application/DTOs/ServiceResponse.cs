namespace ClinicApp.Application.DTOs
{
    public record ServiceResponse<T>(T? Data, bool Success, string Message);
    public record ServiceResponse(bool Success, string Message);
}
