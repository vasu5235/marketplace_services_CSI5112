using System;
namespace marketplace_services_CSI5112.Models
{

    public class Category
    {
        public int Id { get; set; }
        public string ImageURL { get; set; }
        public string Name { get; set; }

        public Category(int Id, string Name, string ImageURL)
        {
            this.Id = Id;
            this.ImageURL = ImageURL;
            this.Name = Name;
        }
    }
}

