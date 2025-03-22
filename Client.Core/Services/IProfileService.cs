namespace Client.Core.Services;

public interface IProfileService
{
    public bool IsFirstTime();
    public bool HasProfile();

}