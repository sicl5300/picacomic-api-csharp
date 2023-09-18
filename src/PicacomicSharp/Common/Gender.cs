namespace PicacomicSharp.Common;

public enum Gender
{
    Male,
    Female,

    /// <summary>
    ///     机器人，不知道有什么用
    /// </summary>
    Bot
}

public static class GenderExtensions
{
    public static string ToApiString(this Gender gender)
    {
        return gender switch
        {
            Gender.Male => "m",
            Gender.Female => "f",
            Gender.Bot => "bot",
            _ => throw new ArgumentOutOfRangeException(nameof(gender), gender, null)
        };
    }
}