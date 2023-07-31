namespace Smogon_MAUIapp.Entities
{
    public class Topics
    {
        #region Properties

        public int topic_id { get; set; }

        public string name { get; set; }

        public List<Forums> forums { get; set; }

        #endregion

        #region Constructor

        public Topics() { }

        public Topics(int topic_id, string name) 
        { 
            this.topic_id = topic_id;
            this.name = name;
        }

        public Topics(string name) 
        { 
            this.name = name;
        }

        #endregion
    }
}
