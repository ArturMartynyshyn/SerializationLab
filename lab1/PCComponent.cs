using System;

namespace lab1
{
    [Serializable]
    public class PCComponent : IComparable<PCComponent>
    {
        private string _name;
        private string _serialNumber;
        private string _manufacturer;
        private string _country;
        private double _price;

        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name cannot be empty.");
                _name = value;
            }
        }

        public string SerialNumber
        {
            get { return _serialNumber; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("SerialNumber cannot be empty.");
                _serialNumber = value;
            }
        }

        public string Manufacturer
        {
            get { return _manufacturer; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Manufacturer cannot be empty.");
                _manufacturer = value;
            }
        }

        public string Country
        {
            get { return _country; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Country cannot be empty.");
                _country = value;
            }
        }

        public double Price
        {
            get { return _price; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Price cannot be negative.");
                _price = value;
            }
        }

        public PCComponent()
        {
            _name = "Unknown";
            _serialNumber = Guid.NewGuid().ToString();
            _manufacturer = "Unknown";
            _country = "Unknown";
            _price = 0.0;
        }

        public PCComponent(string name, string serialNumber, string manufacturer, string country, double price)
        {
            Name = name;
            SerialNumber = serialNumber;
            Manufacturer = manufacturer;
            Country = country;
            Price = price;
        }

        public int CompareTo(PCComponent other)
        {
            if (other == null) return 1;
            return string.Compare(this.SerialNumber, other.SerialNumber, StringComparison.OrdinalIgnoreCase);
        }

        public override string ToString()
        {
            return $"[{SerialNumber}] {Name} | {Manufacturer} | {Country} | {Price} грн";
        }
    }
}