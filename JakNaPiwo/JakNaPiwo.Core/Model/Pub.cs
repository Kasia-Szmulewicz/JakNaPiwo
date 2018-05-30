using SQLite;

namespace JakNaPiwo.Core.Model
{
    public class Pub
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string Number { get; set; }

        public double PubLatitude { get; set; }

        public double PubLongitude { get; set; }
    }

}