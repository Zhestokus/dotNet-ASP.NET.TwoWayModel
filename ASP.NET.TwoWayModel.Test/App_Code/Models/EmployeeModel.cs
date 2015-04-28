using System;

namespace Models
{
    public class EmployeeModel
    {
        public long IDNumber { get; set; }

        public String FirstName { get; set; }
        public String LastName { get; set; }

        public String Gender { get; set; }

        public int Rate { get; set; }

        public bool Active { get; set; }

        public PositionModel Position { get; set; }
    }
}