using System.Diagnostics;

namespace Cron
{
    public interface ISampleInteraface
    {
        void DoSomethingUseful();
    }

    class SampleInteraface : ISampleInteraface
    {
        public void DoSomethingUseful()
        {
            Debug.Write("Konrad to pała");
        }
    }
}