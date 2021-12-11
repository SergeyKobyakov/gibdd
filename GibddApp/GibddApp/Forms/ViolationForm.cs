using GibddApp.Db;
using GibddApp.Db.Models;
using System.ComponentModel;

namespace GibddApp.Forms
{
    internal class ViolationForm : FormBase
    {
        public ViolationForm(): base(            
            Tables.Violation,
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
            Text = "Нарушения";
        }

        protected override IBindingList LoadData()
        {
            var violations = Repository.GetViolations();
            return new BindingList<Violation>(violations);
        }
    }
}
