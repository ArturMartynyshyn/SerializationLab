using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace lab1
{
    [Serializable]
    public class PCComponentCollection
    {
        private SortedList<string, PCComponent> _components;

        public PCComponentCollection()
        {
            _components = new();
        }

        public void Add(PCComponent component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            if (_components.ContainsKey(component.SerialNumber))
                throw new InvalidOperationException("Component with this serial number already exists.");

            _components.Add(component.SerialNumber, component);
        }

        public List<PCComponent> GetAll()
        {
            return _components.Values.ToList();
        }

        public List<PCComponent> FindByName(string name)
        {
            return _components.Values
                .Where(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<PCComponent> FindByCountry(string country)
        {
            return _components.Values
                .Where(c => c.Country.Equals(country, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public void Serialize(string filePath)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var list = _components.Values.ToList();
            var json = JsonSerializer.Serialize(list, options);
            File.WriteAllText(filePath, json);
        }

        public void Deserialize(string filePath)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var json = File.ReadAllText(filePath);
            var list = JsonSerializer.Deserialize<List<PCComponent>>(json, options) ?? new List<PCComponent>();

            _components = new();
            foreach (var c in list)
            {
                _components[c.SerialNumber] = c;
            }
        }
    }
}