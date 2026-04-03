using LetterStomach.Enums;
using LetterStomach.Models;

namespace LetterStomach.Controls
{
    public class SettingConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                if (values[0] != null && values[1] != null && values[2] != null && values[4] != null && values[5] != null && values.Length == 6)
                {
                    string update_database = values[0].ToString();
                    string sqlite_database = values[1].ToString();
                    string drop_database = values[2].ToString();
                    string select_table = "";
                    if (values[3] != null)
                    {
                        Hunks hunks = (Hunks)values[3];
                        select_table = hunks.Value.ToString();
                    }
                    else
                    {
                        Hunks hunks = new Hunks { Name = "Null", Value = (int)Hunk.Null };
                        select_table = hunks.Value.ToString();
                    }
                    string pitch_speak = values[4].ToString().Contains(".") ? values[4].ToString().Split(".")[0] : values[4].ToString();
                    string volume_speak = values[5].ToString().Contains(".")? values[5].ToString().Split(".")[0] : values[5].ToString();

                    return new Setting { UpdateDatabase = update_database, SQLiteDatabase = sqlite_database, DropDatabase = drop_database, SelectTable = select_table, PitchSpeak = pitch_speak, VolumeSpeak = volume_speak};
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
