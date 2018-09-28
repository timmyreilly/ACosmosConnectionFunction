using System;

namespace Company.Function
{
    public class CheeseBurger
    {
        public CheeseBurger()
        {
            this.id = Guid.NewGuid().ToString();
        }
        public string ItemName { get; set; }
        public string id { get; set; }

        public override string  ToString() {
            return $"{ItemName + " " + id}"; 
        }
    }

}