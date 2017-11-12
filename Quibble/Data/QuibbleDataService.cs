using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class QuibbleDataService
    {
        private List<Quibble> Quibbles { get; set; }

        public QuibbleDataService()
        {
            if (File.Exists("./quibbles.dat"))
            {
                var formatter = new BinaryFormatter();
                using (var stream = new FileStream("./quibbles.dat", FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    this.Quibbles = (List<Quibble>)formatter.Deserialize(stream);
                }
            }
            else
            {
                this.Quibbles = new List<Quibble>();
                Init();
            }
        }

        private void Init()
        {
            var quibble = new Quibble
            {
                Id = 1,
                Text = "Old programmers never die, they just can't C as well.",
                Category = "Technology"
            };

            this.Quibbles.Add(quibble);
            Save();
        }

        private void Save()
        {
            var formatter = new BinaryFormatter();
            using (var stream = new FileStream("./quibbles.dat", FileMode.Create, FileAccess.Write))
            {
                formatter.Serialize(stream, this.Quibbles);
            }
        }

        public Quibble[] GetAll()
        {
            return this.Quibbles.ToArray();
        }

        public Quibble GetById(int id)
        {
            return this.Quibbles.SingleOrDefault(q => q.Id == id);
        }

        public Quibble Add(Quibble quibble)
        {
            var lastId = this.Quibbles.Max(q => q.Id);
            quibble.Id = lastId + 1;
            this.Quibbles.Add(quibble);
            Save();
            return quibble;
        }

        public void Update(Quibble quibble)
        {
            var found = this.Quibbles.SingleOrDefault(q => q.Id == quibble.Id);
            if (found != null)
            {
                this.Quibbles.Remove(found);
                this.Quibbles.Add(quibble);
                Save();
            }
        }

        public void Delete(int id)
        {
            var quibble = this.Quibbles.SingleOrDefault(q => q.Id == id);
            this.Quibbles.Remove(quibble);
            Save();
        }
    }
}
