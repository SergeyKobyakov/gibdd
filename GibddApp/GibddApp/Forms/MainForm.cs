using GibddApp.Db;
using GibddApp.Db.Models;
using System.ComponentModel;

namespace GibddApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }        

        private IBindingList LoadData(int menuIndex)
        {
            using (var db = new GibddDbContext())
            {
                switch (menuIndex)
                {
                    case 0:
                        var cars = db.Cars.OrderBy(c => c.License).ToList();
                        return new BindingList<Car>(cars);
                    case 1:
                        var drivers = db.Drivers.OrderBy(c => c.License).ToList();
                        return new BindingList<Driver>(drivers);
                    case 2:
                        var protocols = db.Protocols.OrderBy(c => c.NoProtocol).ToList();
                        return new BindingList<Protocol>(protocols);
                    case 3:
                        var violations = db.Violations.OrderBy(c => c.CodeVio).ToList();
                        return new BindingList<Violation>(violations);

                    default:
                        return new BindingList<object>(new List<object>());
                }
            }
        }
    }
}