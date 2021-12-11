using GibddApp.Db.Models;

namespace GibddApp.Db
{
    internal class Repository
    {
        public List<Car> GetCars()
        {
            using (var db = new GibddDbContext())
            {
                var cars = db.Cars.OrderBy(c => c.Gosno).ToList();
                return cars;
            }
        }

        public List<Driver> GetDrivers()
        {
            using (var db = new GibddDbContext())
            {
                var drivers = db.Drivers.OrderBy(c => c.License).ToList();
                return drivers.ToList();
            }
        }
        
        public List<Protocol> GetProtocols()
        {
            using (var db = new GibddDbContext())
            {
                var protocol = db.Protocols.OrderBy(c => c.NoProtocol).ToList();
                return protocol.ToList();
            }
        }
        
        public List<Violation> GetViolations()
        {
            using (var db = new GibddDbContext())
            {
                var violations = db.Violations.OrderBy(c => c.CodeVio).ToList();
                return violations.ToList();
            }
        }
    }
}
