using System;

namespace Pentaskilled.MEetAndYou.DomainModels
{
    public class ArchiveConfig
    {
        private const string DATE_TIME_FORMAT = "yyyy_MM_dd";
        private const int MAX_LOG_LIFETIME = 30;
        private const string BUFFER_EXTENSION = "_MEetAndYouarchive.csv";
        private const string ARCHIVE_EXTENSION = "_MEetAndYouarchive.zip";
        private const string LOG_CONNECTION_STRING = "_MEetAndYoulog.json";
        private const string BUFFER_LOCATION = @"C:\Users\Raymo\Music\senior-project-buffer";
        private const string ARCHIVE_LOCATION = @"C:\Users\Raymo\Music\senior-project-archives";
        private const double MIN_BYTES = 1024 * 25; // do we need this? don't know if it scales when deploy to cloud



        public string GetDateTimeFormat()
        {
            return DATE_TIME_FORMAT;
        }

        public int GetMaxLogLifetime()
        {
            return MAX_LOG_LIFETIME;
        }
        public string GetBufferExtension()
        {
            return BUFFER_EXTENSION;
        }

        public string GetArchiveExtension()
        {
            return ARCHIVE_EXTENSION;
        }

        public string GetLogConnectionString()
        {
            return LOG_CONNECTION_STRING;
        }
        public string GetBufferLocation()
        {
            return BUFFER_LOCATION;
        }
        public string GetArchiveLocation()
        {
            return ARCHIVE_LOCATION;
        }

        public double GetMinBytes()
        {
            return MIN_BYTES;
        }


    }
}
