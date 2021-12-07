using GibddApp.Db;
using GibddApp.Db.Models;
using System.ComponentModel;

namespace GibddApp.Forms
{
    internal class ViolationForm : FormBase
    {
        public ViolationForm(): base(            
            new[] 
            {
                "CodeVio",
                "NameVio",
                "Article",
                "Sanction"
            },
            new[]
            {
                "CodeVio"
            })
        {
            Text = "Violation";
        }

        protected override IBindingList LoadData()
        {
            using (var db = new GibddDbContext())
            {
                var violation = db.Violations.OrderBy(c => c.CodeVio).ToList();
                return new BindingList<Violation>(violation);
            }
        }
    }
}
