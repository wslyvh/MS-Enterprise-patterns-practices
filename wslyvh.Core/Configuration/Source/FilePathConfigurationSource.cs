using System.Configuration;
using system = System.Configuration;

namespace wslyvh.Core.Configuration.Source
{
    public class FilePathConfigurationSource : FileConfigurationSource
    {
        private readonly string _filePath;

        public FilePathConfigurationSource(string filePath)
        {
            Guard.ArgumentIsNotNullOrEmpty(filePath, "filePath");

            _filePath = filePath;
        }

        protected override system.Configuration OpenConfiguration()
        {
            var fileMap = new ExeConfigurationFileMap { ExeConfigFilename = _filePath };

            return ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
        }
    }
}
