using System.Diagnostics;
using wslyvh.Core.Mvp;

namespace wslyvh.Core.Samples.BootMvp.Models
{
    using System;

    using wslyvh.Core.Samples.BootMvp.Repositories;

    public class SampleModel : Model, ISampleModel
    {
        private readonly ISampleRepository repository;

        public SampleModel(ISampleRepository repository)
        {
            if (repository == null) throw new ArgumentNullException("repository");

            this.repository = repository;
            Trace.WriteLine("Instantiating SampleModel..");
            Trace.WriteLine("HashCode: " + GetHashCode());
        }

        public string GetMessage()
        {
            return repository.GetMessage();
        }

        public void PostMessage(string message)
        {
            repository.PostMessage(message);
        }
    }
}