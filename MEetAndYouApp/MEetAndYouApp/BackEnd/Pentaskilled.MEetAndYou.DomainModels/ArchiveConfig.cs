using System;

namespace Pentaskilled.MEetAndYou.DomainModels
{
    public class ArchiveConfig
    {

        static string strDirectory = Environment.CurrentDirectory.ToString();
        static string firstDir = System.IO.Directory.GetParent(strDirectory).ToString();

        static string secondDir = System.IO.Directory.GetParent(firstDir).ToString();

        static string thirdDir = System.IO.Directory.GetParent(secondDir).ToString();
        static string l = System.IO.Directory.GetParent(thirdDir).ToString();


        static string buffLocation = System.IO.Path.Combine(l, "senior-project-buffer");
        static string archiveLocation = System.IO.Path.Combine(l, "senior-project-archives");

    

        private const string DATE_TIME_FORMAT = "yyyy_MM_dd";
        private const string BUFFER_EXTENSION = "_MEetAndYouarchive.csv";
        private const string ARCHIVE_EXTENSION = "_MEetAndYouarchive.zip";
        private string BUFFER_LOCATION = buffLocation;
        private string ARCHIVE_LOCATION = archiveLocation;

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
