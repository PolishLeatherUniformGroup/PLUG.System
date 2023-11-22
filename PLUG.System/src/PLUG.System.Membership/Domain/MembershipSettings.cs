using PLUG.System.Common.Domain;

namespace PLUG.System.Membership.Domain;

public class MembershipSettings : Entity
{
    public string SettingName { get; private set; }
    public string SettingValue { get; private set; }
    public SettingType Type { get; private set; }
    

    private MembershipSettings()
    {
        
    }
    private MembershipSettings(string settingName, string settingValue, SettingType type)
    {
        this.SettingName = settingName;
        this.SettingValue = settingValue;
        this.Type = type;
    }

    public static MembershipSettings Text(string settingName, string settingValue) =>
        new(settingName, settingValue, SettingType.Text);
    public static MembershipSettings Number(string settingName, int settingValue) =>
        new(settingName, settingValue.ToString(), SettingType.Number);
    public static MembershipSettings Currency(string settingName, decimal settingValue) =>
        new(settingName, settingValue.ToString("C"), SettingType.Currency);
    public static MembershipSettings Boolean(string settingName, bool settingValue) =>
        new(settingName, settingValue.ToString(), SettingType.Boolean);
    
}