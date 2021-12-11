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
                ("CodeVio", "Код нарушения"),
                ("NameVio", "Название нарушения"),
                ("Article", "Статья нарушения"),
                ("Sanction", "Санкция")
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
