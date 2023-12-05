using System.Runtime.CompilerServices;

namespace ONPA.Communication.Model;

public class TemplateParam
{
    public static TemplateParam Email = new("Email");
    public static TemplateParam FirstName = new("Imię");
    public static TemplateParam LastName = new("Nazwisko");
    public static TemplateParam CardNumber = new("Numer karty");
    public static TemplateParam EventName = new("Nazwa wydarzenia");
    public static TemplateParam EventDate = new("Data wydarzenia");
    public static TemplateParam FeeAmount = new("Kwota opłaty");
    public static TemplateParam AppealDeadline = new("Termin odwołania");
    
    

    public string Name { get; set; }
    public string Value { get; set; }

    public string DisplayName { get; set; }

    private TemplateParam(string displayName, [CallerMemberName] string name = "")
    {
        this.DisplayName = displayName;
        this.Name = name;
    }

    public TemplateParam AddValue(string value)
    {
        this.Value = value;
        return this;
    }
}