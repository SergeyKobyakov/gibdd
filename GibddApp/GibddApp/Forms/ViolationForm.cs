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
            var violations = Repository.GetViolations();
            return new BindingList<Violation>(violations);
        }
    }
}
