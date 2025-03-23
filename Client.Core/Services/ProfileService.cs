using System.Text.Json.Serialization;
using Client.Core;
using Client.Core.Services;

namespace Client.Core.Services;

public class Profile
{
    [JsonPropertyName("user_uid")] public string? UserUID { get; set; } = string.Empty;
    [JsonPropertyName("extra")] public string? Additional { get; set; } = string.Empty;
}

public class ProfileService : LocalDataService, IProfileService
{
    private static string _profilePath => "profile.json";
    private Profile _profile = new Profile();

    public string? UID
    {
        get
        {
            if (Exists(_profilePath))
            {
                _profile = Parse<Profile>(_profilePath);
                return _profile.UserUID;
            }
            else
            {
                return string.Empty;
            }
        }
        set
        {
            _profile.UserUID = value;
            Save(_profilePath, _profile);
        }
    }

    public ProfileService() : base()
    {
        _profile = Parse<Profile>(_profilePath);
        if (_profile == null)
        {
            _profile = new Profile();
        }
    }

    public bool IsFirstTime()
    {
        return !HasProfile() || string.IsNullOrEmpty(UID);
    }

    public bool HasProfile()
    {
        return Exists(_profilePath);
    }
}