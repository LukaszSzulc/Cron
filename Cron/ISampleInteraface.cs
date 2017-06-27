using System.Diagnostics;

namespace Cron
{
    public interface ISampleInteraface
    {
        void DoSomethingUseful();
    }

    class SampleInteraface : ISampleInteraface
    {
        private readonly IDatabaseFake _databaseFake;

        public SampleInteraface(IDatabaseFake databaseFake)
        {
            _databaseFake = databaseFake;
        }
        public void DoSomethingUseful()
        {
            var fake = _databaseFake.GetSomeEntities();
            Debug.Write(fake.Count);
            Debug.Write("Konrad to pała");
        }
    }
}