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
                ("CodeVio", "Код нарушения", false, true),
                ("NameVio", "Название нарушения", false, false),
                ("Article", "Статья нарушения", false, false),
                ("Sanction", "Санкция", false, false)
            })
        {
            Text = "Нарушения";
        }

        protected override void LoadData()
        {
            var violations = Repository.GetViolations();
            Data = new BindingList<Violation>(violations);
        }
    }
}
