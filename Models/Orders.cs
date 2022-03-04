using System;
namespace marketplace_services_CSI5112.Models
{
    public class Orders
    {

        public int Id;
        public int UserId;
        public List<Product> Products; 

        public Orders(int Id, int UserId, List<Product> Products)
        {
            this.Id = Id;
            this.UserId = UserId;
            this.Products = Products; 
        }
    }
}

