using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace MopsBot.Module.Data.Session
{
    class TwitchResult
    {
        public Stream stream { get; set; }
    }

    class Stream{
        public ulong _id {get; set; }
        public int video_height {get; set; }
        public int average_fps {get; set; }
        public int delay {get; set; }
        public string created_at {get; set; }
        public bool is_playlist {get; set; }
        
        public string game {get; set; }
        public int viewers {get; set; }
        public Channel channel {get; set; }
        public Preview preview {get; set; }
    }

    class Channel{
        public string game {get; set; }
        public bool mature;
         public string status;
         public string broadcaster_language;
         public string display_name;
         public string language;
         public ulong _id;
         public string name;
         public string created_at {get; set; }
         public string updated_at {get; set; }
         public bool partner {get; set; }
         public string logo {get; set; }
         public string video_banner {get; set; }
         public string profile_banner {get; set; }
         public string profile_banner_background_color {get; set; }
         public string url {get; set; }
         public int views {get; set; }
         public int followers {get; set; }
      
    }
    public class Preview{
        public string small {get; set; }
        public string medium {get; set; }
        public string large {get; set; }
        public string template {get; set; }
      
    }
}
