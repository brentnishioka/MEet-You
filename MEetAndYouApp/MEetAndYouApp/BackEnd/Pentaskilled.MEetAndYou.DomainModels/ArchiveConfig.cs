using System;

namespace Pentaskilled.MEetAndYou.DomainModels
{
    public class ArchiveConfig
    {
        private const string DATE_TIME_FORMAT = "yyyy_MM_dd";
        private const string BUFFER_EXTENSION = "_MEetAndYouarchive.csv";
        private const string ARCHIVE_EXTENSION = "_MEetAndYouarchive.zip";
        private const string BUFFER_LOCATION = @"C:\Users\Raymo\Music\senior-project-buffer";
        private const string ARCHIVE_LOCATION = @"C:\Users\Raymo\Music\senior-project-archives";

        public string GetDateTimeFormat()
        {
            return DATE_TIME_FORMAT;
        }

        public string GetBufferExtension()
        {
            return BUFFER_EXTENSION;
        }

        public string GetArchiveExtension()
        {
            return ARCHIVE_EXTENSION;
        }

        public string GetBufferLocation()
        {
            return BUFFER_LOCATION;
        }
        public string GetArchiveLocation()
        {
            return ARCHIVE_LOCATION;
        }

    }
}
