namespace LetterStomach.Interfaces
{
    public interface IRecordService
    {
        void StartRecordMP3();
        string StopRecordMP3();
        void StartRecordWav();
        string StopRecordWav();
    }
}
